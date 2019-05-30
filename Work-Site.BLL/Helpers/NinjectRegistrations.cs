using System.Collections.Generic;
using Ninject.Modules;
using Work_Site.BLL.Models;

namespace Work_Site.BLL.Helpers
{
    public class NinjectRegistrations:NinjectModule
    {
        public override void Load()
        {
            Bind<ICollection<VacationDto>>().To<List<VacationDto>>();
            Bind<ICollection<ResumeDto>>().To<List<ResumeDto>>();
            Bind<ICollection<UserDto>>().To<List<UserDto>>();
        }
    }
}