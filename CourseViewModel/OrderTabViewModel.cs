﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class OrderTabViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}
