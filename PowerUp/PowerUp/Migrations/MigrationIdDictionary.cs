using System;
using System.Collections.Generic;

namespace PowerUp.Migrations
{
  public class MigrationIdDictionary : Dictionary<Type, IDictionary<int, int>>
  {
    public void AddForType(Type type, int importedId, int savedId)
    {
      TryAdd(type, new Dictionary<int, int>());
      this[type][importedId] = savedId;
    }

    public int RetrieveForTypeId(Type type, int importedId)
    {
      var entityDictionary = this[type];
      return entityDictionary[importedId];
    }
  }
}
