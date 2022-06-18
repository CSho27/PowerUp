using System;
using System.Collections.Generic;

namespace PowerUp.Generators
{
  public abstract class GenerationAlgorithm<TOutput, TDatasetEnum, TDatasetCollection> 
  {
    protected IList<PropertySetter<TOutput, TDatasetCollection>> propertySetters = new List<PropertySetter<TOutput, TDatasetCollection>>();
    public IEnumerable<PropertySetter<TOutput, TDatasetCollection>> PropertySetters => propertySetters;

    public abstract HashSet<TDatasetEnum> DatasetDependencies { get; }

    protected void SetProperty(string propertyKey, Action<TOutput, TDatasetCollection> setProperty)
      => SetProperty(new InlinePropertySetter<TOutput, TDatasetCollection>(propertyKey, (obj, data) => { setProperty(obj, data); return true; }));

    protected void SetProperty(PropertySetter<TOutput, TDatasetCollection> propertySetter)
    {
      propertySetters.Add(propertySetter);
    }
  }

  public abstract class PropertySetter<TOutput, TDatasetCollection>
  {
    public abstract string PropertyKey { get; }
    public abstract bool SetProperty(TOutput obj, TDatasetCollection datasetCollection);
  }

  public class InlinePropertySetter<TOutput, TDatasetCollection> : PropertySetter<TOutput, TDatasetCollection>
  {
    private readonly Func<TOutput, TDatasetCollection, bool> _setProperty;

    public override string PropertyKey { get; }
    public override bool SetProperty(TOutput obj, TDatasetCollection datasetCollection) => _setProperty(obj, datasetCollection);


    public InlinePropertySetter(string propertyKey, Func<TOutput, TDatasetCollection, bool> setProperty)
    {
      PropertyKey = propertyKey;
      _setProperty = setProperty;
    }
  }
}
