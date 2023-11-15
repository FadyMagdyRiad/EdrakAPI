using EdrakBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakBusiness.Dto.Common
{
    public class Response<T>
    {
        public Response()
        {
            Code = ResponseStatusEnum.Error;
            Message = "! Error";
        }
        public ResponseStatusEnum Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
