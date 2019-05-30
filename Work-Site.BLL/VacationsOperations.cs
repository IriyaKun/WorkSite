using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject;
using Work_Site.BLL.Exceptions;
using Work_Site.BLL.Helpers;
using Work_Site.BLL.Models;
using Work_Site.DAL;
using Work_Site.DAL.Models;

namespace Work_Site.BLL
{
    public class VacationsOperations
    {
        private WorkSiteUow _uow;

        private readonly MapperConfiguration _businessDbVacationConfig;
        private readonly MapperConfiguration _dbBusinessVacationConfig;
        private readonly MapperConfiguration _dbBusinessResumeConfig;
        private IMapper _mapper;
        private readonly IKernel _kernel;


        public VacationsOperations(IMapper mapper)
        {
            _mapper = mapper;
            _uow = new WorkSiteUow();
            _businessDbVacationConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VacationDto, Vacation>();
            });
            _dbBusinessVacationConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vacation, VacationDto>();
            });
            _dbBusinessResumeConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Resume, ResumeDto>();
            });

            _kernel = new StandardKernel(new NinjectRegistrations());
        }

        public void AddVacation(VacationDto vacation, UserDto user)
        {
            vacation.Guid = System.Guid.NewGuid().ToString();
            vacation.User = user;

            var mapper = _businessDbVacationConfig.CreateMapper();

            var dest = mapper.Map<VacationDto, Vacation>(vacation);

            try
            {
                _uow.Vacations.Create(dest);
                _uow.Save();
            }
            catch(Exception)
            {
                throw new InvalidVacationException();
            }
        }

        public ICollection<ResumeDto> LookAtAllResumesForVacation(VacationDto vacation)
        {
            ICollection<ResumeDto> result = _kernel.Get<ICollection<ResumeDto>>();


            var vac = _uow.Vacations.Read(vacation.Guid);
            var mapper = _dbBusinessResumeConfig.CreateMapper();
            foreach (var user in vac.Users)
            {
                var resume = user.Resume;
                var res = mapper.Map<Resume, ResumeDto>(resume);
                result.Add(res);
            }

            return result;
        }

        public ICollection<VacationDto> FetchFromNewestVacations()
        {
            ICollection<VacationDto> result = _kernel.Get<ICollection<VacationDto>>();

            var mapper = _dbBusinessVacationConfig.CreateMapper();
            var vac = _uow.Vacations.GetAll().OrderByDescending(v => v.DatePosted);

            foreach (var v in vac)
            {
                result.Add(mapper.Map<Vacation, VacationDto>(v));
            }

            return result;
        }

        public ICollection<VacationDto> FetchAllVacationsFromSearch(string search)
        {
            ICollection<VacationDto> result = _kernel.Get<ICollection<VacationDto>>();

            var vac = _uow.Vacations.GetAll();
            var mapper = _dbBusinessVacationConfig.CreateMapper();

            var queryResults = vac.Where(vacation => vacation.Name.Contains(search));
            foreach (var resume in queryResults)
            {
                var r = mapper.Map<Vacation, VacationDto>(resume);
                result.Add(r);
            }

            return result;
        }
    }
}
