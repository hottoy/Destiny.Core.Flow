﻿using Destiny.Core.Flow.Entity;
using Destiny.Core.Flow.EntityFrameworkCore;
using Destiny.Core.Flow.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Destiny.Core.Flow.Model.RepositoryBase
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : IEntity<TPrimaryKey>
    {
        #region 工作单元
        IUnitOfWork UnitOfWork { get; }
        #endregion

        #region 查询
        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        IQueryable<TEntity> Entities { get; }
        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        IQueryable<TEntity> TrackEntities { get; }
        /// <summary>
        /// 根据ID得到实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体</returns>
        TEntity GetById(TPrimaryKey primaryKey);
        /// <summary>
        /// 异步根据ID得到实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体</returns>
        Task<TEntity> GetByIdAsync(TPrimaryKey primaryKey);
        /// <summary>
        /// 根据ID得到Dto实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体并转成Dto</returns>
        TDto GetByIdToDto<TDto>(TPrimaryKey primaryKey) where TDto : class, new();
        /// <summary>
        /// 异步根据ID得到Dto实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体并转成Dto</returns>
        Task<TDto> GetByIdToDtoAsync<TDto>(TPrimaryKey primaryKey) where TDto : class, new();
        /// <summary>
        ///查询不跟踪数据源
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>返回查询后数据源</returns>
        IQueryable<TEntity> QueryAsNoTrack(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询不跟踪数据源
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns>返回查询后数据源</returns>
        IQueryable<TResult> QueryAsNoTrack<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
        /// <summary>
        ///查询跟踪数据源
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>返回查询后数据源</returns>
        IQueryable<TEntity> TrackQuery(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region 查询实体
        /// <summary>
        /// 无条件获取所有数据（非实体跟踪）
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAsNoTrackAll();
        /// <summary>
        /// 无条件获取所有数据（非实体跟踪）
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetTrackAll();
        /// <summary>
        /// 查询一个实体根据条件
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步等待执行获取所有实体集合
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();
        /// <summary>
        /// 根据查询select
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
        /// <summary>
        /// 根据条件获取所有实体集合
        /// </summary>
        /// <returns></returns>
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region 添加数据
        /// <summary>
        /// 以异步DTO插入实体
        /// </summary>
        /// <typeparam name="TInputDto">添加DTO类型</typeparam>
        /// <param name="dto">添加DTO</param>
        /// <param name="checkFunc">添加信息合法性检查委托</param>
        /// <param name="insertFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResponse> InsertAsync<TInputDto>(TInputDto dto, Func<TInputDto, Task> checkFunc = null, Func<TInputDto, TEntity, Task<TEntity>> insertFunc = null, Func<TEntity, TInputDto> completeFunc = null) where TInputDto : IInputDto<TPrimaryKey>;
        /// <summary>
        /// 以异步插入实体
        /// </summary>
        /// <param name="entity">要插入实体</param>
        /// <returns>影响的行数</returns>
        Task<int> InsertAsync(TEntity entity);
        /// <summary>
        /// 以异步批量插入实体
        /// </summary>
        /// <param name="entitys">要插入实体集合</param>
        /// <returns>影响的行数</returns>
        Task<int> InsertAsync(TEntity[] entitys);
        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entitys">要插入实体集合</param>
        /// <returns></returns>
        int Insert(params TEntity[] entitys);
        #endregion

        #region 修改实体
        /// <summary>
        /// 以异步DTO更新实体
        /// </summary>
        /// <typeparam name="TInputDto">更新DTO类型</typeparam>
        /// <param name="dto">更新DTO</param>
        /// <param name="checkFunc">添加信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResponse> UpdateAsync<TInputDto>(TInputDto dto, Func<TInputDto, TEntity, Task> checkFunc = null, Func<TInputDto, TEntity, Task<TEntity>> updateFunc = null) where TInputDto : class, IInputDto<TPrimaryKey>, new();
        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity">要更新实体</param>
        /// <returns>返回更新受影响条数</returns>
        Task<int> UpdateAsync(TEntity entity);
        /// <summary>
        /// 同步更新
        /// </summary>
        /// <param name="entity">要更新实体</param>
        /// <returns>返回更新受影响条数</returns>
        int Update(TEntity entity);
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<OperationResponse> DeleteAsync(TPrimaryKey primaryKey);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">要删除实体</param>
        /// <returns>返回删除受影响条数</returns>
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// 异步删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entitys">要删除实体集合</param>
        /// <returns>操作影响的行数</returns>
        int Delete(params TEntity[] entitys);
        #endregion
    }
}
