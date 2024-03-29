﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PowerUp.Databases
{
  public abstract class Entity
  {
    public int? Id { get; set; }
    public virtual string? GetBaseMatchIdentifier() => null;
    public virtual IEnumerable<Func<Entity, object>> UntypedIndexes 
      => Enumerable.Empty<Func<Entity, object>>();
  }

  public abstract class Entity<TEntity> : Entity where TEntity : Entity<TEntity>
  {
    public override IEnumerable<Func<Entity, object>> UntypedIndexes => Indexes.Cast<Func<Entity, object>>();
    public virtual IEnumerable<Func<TEntity, object>> Indexes 
      => Enumerable.Empty<Func<TEntity, object>>();
  }
}
