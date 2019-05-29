using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Work_Site.BLL.Helpers;
using Work_Site.BLL.Models;
using Work_Site.DAL;
using Work_Site.DAL.Models;

namespace Work_Site.BLL
{
    public class VacationsOperations
    {
        private WorkSiteUow _uow;

        private MapperConfiguration _businessDbConfig;
        private MapperConfiguration _dbBusinessConfig;
        private IMapper _mapper;


        public VacationsOperations()
        {
            _uow = new WorkSiteUow();
            _businessDbConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VacationDetails, Vacation>();
            });
            _dbBusinessConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vacation, VacationDetails>();
            });
        }

        public OperationResult AddVacation(VacationDetails vacation, UserDetails user)
        {
            vacation.Guid = System.Guid.NewGuid().ToString();
            vacation.User = user;

            var mapper = _businessDbConfig.CreateMapper();

            var dest = mapper.Map<VacationDetails, Vacation>(vacation);

            return OperationResult.OK;
        }
    }
}
