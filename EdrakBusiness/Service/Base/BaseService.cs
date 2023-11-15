using AutoMapper;
using EdrakBusiness.Settings;
using Persistence.IUnitOfWork;
using Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakBusiness.Service.Base
{
    public class BaseService
    {
        protected IUnitOfWork UnitOfWork;
        protected AppSettings AppSettings;
        protected IMapper Mapper;
        public BaseService(AppSettings appSettings, IMapper mapper, IUnitOfWork unitOfWork)
        {
            AppSettings = appSettings;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
    }
}
