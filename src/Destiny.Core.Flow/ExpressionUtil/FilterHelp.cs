﻿using Destiny.Core.Flow.Extensions;
using Destiny.Core.Flow.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;


namespace Destiny.Core.Flow.ExpressionUtil
{
    /// <summary>
    /// 查询帮助
    /// </summary>
   public static class FilterHelp
    {
        /// <summary>
        /// 得到表达目录树
        /// </summary>
        /// <typeparam name="T">动态实体</typeparam>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetExpression<T>(FilterInfo[] filters)
        {
            if (filters == null || filters.Length == 0)
            {
                Expression<Func<T, bool>> expression = t=> true;
                return expression;
            }

             
            string expressionStr = GetFilterExpression(filters);

            var expression1 =  DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig.Default,true, expressionStr, filters.Select(o => o.Value).ToArray());
            return expression1;
        }


        /// <summary>
        /// 得到查询信息拼接好字符串
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        private static string GetFilterExpression(FilterInfo[] filters)
        {
            StringBuilder strWhere = new StringBuilder();
            int count = 0;
            foreach (FilterInfo filterInfo in filters)
            {
                var index = count + 1;
                //"City == @0 and Orders.Count >= @1"
                if (index != filters.Length)
                {

                    strWhere.Append($"{filterInfo.Key} {filterInfo.Operator.ToDescription<FilterCodeAttribute>()} @{count} {filterInfo.Connect.ToDescription<FilterCodeAttribute>()} ");
                }
                else
                {
                    strWhere.Append($"{filterInfo.Key} {filterInfo.Operator.ToDescription<FilterCodeAttribute>()} @{count}");
                }
                count++;
            }
            return strWhere?.ToString();
        }
    }
}
