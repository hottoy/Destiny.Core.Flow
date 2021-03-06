﻿
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Destiny.Core.Flow.EntityFrameworkCore;
using Destiny.Core.Flow.Reflection;
using Destiny.Core.Flow.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Destiny.Core.Flow
{
   /// <summary>
   /// 上下文基类
   /// </summary>
    public abstract class DbContextBase : DbContext
    {
        private readonly IServiceProvider _serviceProvider = null;
   

        protected DbContextBase(DbContextOptions options,IServiceProvider serviceProvider)
             : base(options)
        {
            _serviceProvider = serviceProvider;

            //_logger = serviceProvider?.GetLogger(GetType());
           
        }

        public IUnitOfWork UnitOfWork { get;  set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var typeFinder = _serviceProvider.GetService<ITypeFinder>();

            IEntityMappingConfiguration[] mappings = typeFinder.Find(o => o.IsDeriveClassFrom<IEntityMappingConfiguration>()).Select(o => Activator.CreateInstance(o) as IEntityMappingConfiguration).ToArray();
            foreach (var item in mappings)
            {
                item.Map(modelBuilder);

            }
        }


        /// <summary>
        /// 异步保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
    
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }


    }
}
