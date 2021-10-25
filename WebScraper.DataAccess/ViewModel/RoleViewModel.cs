﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.DataAccess.ViewModel
{
    public class RoleViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
        public bool RowStatus { get; set; }
    }
}
