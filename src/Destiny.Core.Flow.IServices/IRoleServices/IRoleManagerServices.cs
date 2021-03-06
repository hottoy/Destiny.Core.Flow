﻿using Destiny.Core.Flow.Dtos.RoleDtos;
using Destiny.Core.Flow.Ui;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Destiny.Core.Flow.IServices.IRoleServices
{
    public interface IRoleManagerServices
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        Task<OperationResponse> AddRoleAsync(RoleInputDto dto);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        Task<OperationResponse> UpdateRoleAsync(RoleInputDto dto);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> DeleteAsync(Guid id);
    }
}
