﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Destiny.Core.Flow.Dtos.Identitys
{
    /// <summary>
    /// 登录Dto
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 用户名登录名
        /// </summary>

        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
