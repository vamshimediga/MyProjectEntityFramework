﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ILoginLogRepository
    {
        Task LogLoginAsync(LoginLog log);
    }
}
