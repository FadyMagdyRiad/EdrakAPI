using Domain.Entities;
using Persistence.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IRepositories
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
    }
}
