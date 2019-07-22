using FH.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FH.Dapper.Filters.Action
{
    public class NullDapperActionFilterExecuter : IDapperActionFilterExecuter
    {
        public static readonly NullDapperActionFilterExecuter Instance = new NullDapperActionFilterExecuter();

        public void ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
        }

        public void ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
        }

        public void ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
        }
    }
}
