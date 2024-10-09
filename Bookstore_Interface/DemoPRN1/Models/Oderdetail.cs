﻿using System;
using System.Collections.Generic;

namespace DemoPRN1.Models
{
    public partial class Oderdetail
    {
        public int OrderdetailsId { get; set; }
        public bool? Type { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Quantity { get; set; }
        public int? BookId { get; set; }

        public virtual Book? Book { get; set; }
    }
}
