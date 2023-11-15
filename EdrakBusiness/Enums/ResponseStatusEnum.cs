using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakBusiness.Enums
{
    public enum ResponseStatusEnum
    {
        Success = 0,
        Error = 1,
        Exception = 2,
        Pending = 3,
        Rejected = 4,
        TimeOut = 5,
        FAIL = 6
    }
}
