using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PowerUp.Databases
{
  public abstract class Entity<TEntity> where TEntity : Entity<TEntity>
  {
    public int? Id { get; set; }
    public virtual IEnumerable<Expression<Func<TEntity, object>>> Indexes 
      => Enumerable.Empty<Expression<Func<TEntity, object>>>();
  }
}
