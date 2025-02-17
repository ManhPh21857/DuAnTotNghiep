﻿using Project.Core.ApplicationService.Queries;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductQuery : IQuery<GetProductQueryResult>
    {
        public int PageNo { get; set; }
        public int IsDeleted { get; set; }

        public GetProductQuery(int pageNo, int isDeleted)
        {
            this.PageNo = pageNo;
            this.IsDeleted = isDeleted;
        }
    }
}
