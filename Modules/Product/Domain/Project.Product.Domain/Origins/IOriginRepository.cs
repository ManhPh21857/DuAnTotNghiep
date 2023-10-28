﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Origins
{
    public interface IOriginRepository
    {
        Task<IEnumerable<OriginInfo>> GetOrigin();
        Task CreateOrigin(OriginInfo origin);
        Task UpdateOrigin(OriginInfo origin);
        Task DeleteOrigin(OriginInfo origin);
    }
}
