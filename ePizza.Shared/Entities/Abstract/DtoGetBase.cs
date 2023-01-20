﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Shared.Utilities.Results.ComplexType;

namespace ePizza.Services.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
    }
}
