using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCommon.Models
{
    public class QuerryPrameters
    {
        const int maxPageSize = 100;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
