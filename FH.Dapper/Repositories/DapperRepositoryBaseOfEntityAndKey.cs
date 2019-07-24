using Dapper;
using DapperExtensions;
using FH.Core.Domain.Entities;
using FH.Dapper.Extensions;
using FH.Dapper.Filters.Action;
using FH.Dapper.Filters.Query;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FH.Dapper.Repositories
{
    public class DapperRepositoryBase<TEntity, TPrimaryKey> : DapperRepositoryBaseAbs<TEntity, TPrimaryKey>
        where TEntity: class,
        IEntity<TPrimaryKey>
    {
        public DapperRepositoryBase(SessionServicePriver sessionServicePriver)
        {
            SessionServicePriver = sessionServicePriver;
            IDapperQueryFilterExecuter = DapperQueryFilterExecuter.Instance;
            IDapperActionFilterExecuter = DapperActionFilterExecuter.Instance;
        }

        public SessionServicePriver SessionServicePriver;

        /// <summary>
        /// 查询过滤 可扩展添加TenantId 多租户系统
        /// </summary>
        public IDapperQueryFilterExecuter IDapperQueryFilterExecuter { get; set; }

        /// <summary>
        /// 方法过滤
        /// </summary>
        public IDapperActionFilterExecuter IDapperActionFilterExecuter { get; set; }

        /// <summary>
        /// 事务
        /// </summary>
        public virtual DbTransaction Transaction
        {
            get { return SessionServicePriver.GetTransaction(); }
        }


        /// <summary>
        /// 数据库连接
        /// </summary>
        public virtual DbConnection Connection
        {
            get
            {
                return SessionServicePriver.GetConnection();
            }
        }

        public override TEntity Single(TPrimaryKey id)
        {
            return Single(CreateEqualityExpressionForId(id));
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
          
            IPredicate pg = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg).Single();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate pg = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg, transaction: Transaction).FirstOrDefault();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            if (item == null) { throw new EntityNotFoundException(typeof(TEntity), id); }

            return item;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            PredicateGroup predicateGroup = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>();
            return Connection.GetList<TEntity>(predicateGroup, transaction: Transaction);
        }

        public override IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            //return Connection.Query<TEntity>(query, parameters, Transaction);
            return null;
        }

        public override Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            //return Connection.QueryAsync<TEntity>(query, parameters, Transaction);
            return null;
        }

        public override IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            //return Connection.Query<TAny>(query, parameters, Transaction);
            return null;
        }

        public override Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            //return Connection.QueryAsync<TAny>(query, parameters, Transaction);
            return null;
        }

        public override int Execute(string query, object parameters = null)
        {
            //return Connection.Execute(query, parameters, Transaction);
            return 1;
        }

        public override Task<int> ExecuteAsync(string query, object parameters = null)
        {
            //return Connection.ExecuteAsync(query, parameters, Transaction);
            return null;
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            IPredicate filteredPredicate = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);

            return Connection.GetPage<TEntity>(
                filteredPredicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                pageNumber,
                itemsPerPage,
                Transaction);
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate filteredPredicate = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.Count<TEntity>(filteredPredicate, Transaction);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            IPredicate filteredPredicate = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetSet<TEntity>(
                filteredPredicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                firstResult,
                maxResults,
                Transaction
            );
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate filteredPredicate = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(filteredPredicate, transaction: Transaction);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate filteredPredicate = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetPage<TEntity>(filteredPredicate, sortingExpression.ToSortable(ascending), pageNumber, itemsPerPage, Transaction);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate filteredPredicate = IDapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetSet<TEntity>(filteredPredicate, sortingExpression.ToSortable(ascending), firstResult, maxResults, Transaction);
        }

        public override void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public override void Update(TEntity entity)
        {
            //EntityChangeEventHelper.TriggerEntityUpdatingEvent(entity);
            IDapperActionFilterExecuter.ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(entity);
            Connection.Update(entity, Transaction);
            //EntityChangeEventHelper.TriggerEntityUpdatedEventOnUowCompleted(entity);
        }

        public override void Delete(TEntity entity)
        {
            //EntityChangeEventHelper.TriggerEntityDeletingEvent(entity);
            if (entity is ISoftDelete)
            {
                IDapperActionFilterExecuter.ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(entity);
                Connection.Update(entity, Transaction);
            }
            else
            {
                Connection.Delete(entity, Transaction);
            }
            //EntityChangeEventHelper.TriggerEntityDeletedEventOnUowCompleted(entity);
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> items = GetAll(predicate);
            foreach (TEntity entity in items)
            {
                Delete(entity);
            }
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            //EntityChangeEventHelper.TriggerEntityCreatingEvent(entity);
            IDapperActionFilterExecuter.ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(entity);
            TPrimaryKey primaryKey = Connection.Insert(entity, Transaction);
            //EntityChangeEventHelper.TriggerEntityCreatedEventOnUowCompleted(entity);
            return primaryKey;
        }
    }
}
