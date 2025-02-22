using Microsoft.Extensions.Logging;
using PowerUp.Databases;
using System;
using System.Linq;
using System.Reflection;

namespace PowerUp.Migrations
{
  public interface IMigrationApi
  {
    public void MigrateDataFrom(string dataDirectory);
  }

  public class MigrationApi : IMigrationApi
  {
    private readonly ILogger<EntityDatabase> _entityDbLogger;

    public MigrationApi(ILogger<EntityDatabase> entityDbLogger)
    {
      _entityDbLogger = entityDbLogger;
    }

    public void MigrateDataFrom(string dataDirectory)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();
      using var existingDatabase = new EntityDatabase(_entityDbLogger, dataDirectory);
      var entityTypes = MigrationHelpers.GetAllEntityTypes();
      var importIdDictionary = new MigrationIdDictionary();

      // Fisrt time through save all types
      foreach (var entityType in entityTypes)
      {
        var allExistingSavedEntities = DatabaseConfig.Database.LoadAll(entityType).Where(e => e.GetBaseMatchIdentifier() != null).ToList();
        var allEntitiesOfType = existingDatabase.LoadAll(entityType);
        var lateMappedProperties = entityType
          .GetProperties()
          .Where(p => p.GetCustomAttribute<MigrationLateMapAttribute>() != null);
        var newEntity = entityType.InstantiateWithEmptyConstructor();

        // clear late-mapped values
        var entitiesOfTypeToImport = allEntitiesOfType.Where(e => e.GetBaseMatchIdentifier() == null).ToList();
        foreach (var entity in entitiesOfTypeToImport)
        {
          // For late mapped properties, set values to their defaults for a new entity
          foreach(var property in lateMappedProperties)
            property.SetValue(entity, property.GetValue(newEntity));
        }
        var savedIdsByImportId = DatabaseConfig.Database.ImportAll(entityType, entitiesOfTypeToImport);
        importIdDictionary.Add(entityType, savedIdsByImportId);

        // Add base entities for entity type if they exist
        foreach (var entity in allEntitiesOfType.Where(e => e.GetBaseMatchIdentifier() != null).ToList())
        {
          var matchingEntity = allExistingSavedEntities.SingleOrDefault(e => e.GetBaseMatchIdentifier() == entity.GetBaseMatchIdentifier());
          importIdDictionary.AddForType(entityType, entity.Id!.Value, matchingEntity!.Id!.Value);
        }
      }

      // Second time through perform late maps
      foreach (var entityTypeAndMap in importIdDictionary)
      {
        var entityType = entityTypeAndMap.Key;
        var savedEntityIdsByImportedId = entityTypeAndMap.Value;

        var lateMapperTypes = entityType
          .GetProperties()
          .Select(p => p.GetCustomAttribute<MigrationLateMapAttribute>()?.LateMapper)
          .Where(m => m != null)
          .Cast<Type>();

        if (!lateMapperTypes.Any())
          continue;

        foreach(var importIdAndSavedId in savedEntityIdsByImportedId)
        {
          var importedEntity = existingDatabase.Load(entityType, importIdAndSavedId.Key)!;
          var savedEntity = DatabaseConfig.Database.Load(entityType, importIdAndSavedId.Value)!;
          foreach (var lateMapperType in lateMapperTypes)
          {
            var lateMapper = (LateMapper)lateMapperType.InstantiateWithEmptyConstructor();
            lateMapper.Perform(importIdDictionary, importedEntity, savedEntity);
          }

          DatabaseConfig.Database.Save(entityType, savedEntity);
        }
      }

      tx.Commit();
    }
  }
}
