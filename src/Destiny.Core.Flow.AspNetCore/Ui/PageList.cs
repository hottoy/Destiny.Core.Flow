﻿using Destiny.Core.Flow.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Flow.AspNetCore.Ui
{
    public class PageList<T> : ResultBase
    {

        public PageList() : this(new T[0], 0, "查询成功", true)
        {

        }
        public PageList(IEnumerable<T> data, int total, string message = "查询成功", bool success = true)
        {
            Data = data;
            Total = total;
            Success = success;
            this.Message = message;
        }


        public IEnumerable<T> Data { get; set; }

        public int Total { get; set; }


    }

    public class PageListDto : PageList<dynamic>
    {

    }
}
