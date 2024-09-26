﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.ProductSpecification
{
    public class ProductSpecifications
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1; 
        private const int MAX_PAGE_SIZE = 50;

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }
        private string? _search;

        public string? Search
        {
            get => _search;
            set => _search = value?.Trim().ToLower();
        }

    }
}
