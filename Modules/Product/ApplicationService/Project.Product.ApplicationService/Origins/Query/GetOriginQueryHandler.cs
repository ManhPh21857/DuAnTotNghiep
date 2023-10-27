﻿using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Origins;
using Project.Product.Integration.Origins.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Origins.Query
{
    public class GetOriginQueryHandler : QueryHandler<GetOriginQuery, GetOriginQueryResult>
    {
        private readonly IOriginRepository Origin;
        public GetOriginQueryHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<GetOriginQueryResult> Handle(GetOriginQuery request, CancellationToken cancellationToken)
        {
            var result = await Origin.GetOrigin(null);
            return new GetOriginQueryResult(result.ToList());
        }
    }
}
