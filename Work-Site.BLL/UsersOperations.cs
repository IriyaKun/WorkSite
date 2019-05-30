using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Newtonsoft.Json;
using Ninject;
using Work_Site.BLL.Exceptions;
using Work_Site.BLL.Helpers;
using Work_Site.BLL.Models;
using Work_Site.DAL;
using Work_Site.DAL.Models;

namespace Work_Site.BLL
{
    public class UsersOperations
    {
        private WorkSiteUow _uow;
        private MapperConfiguration _dbBusinessUser;
        private MapperConfiguration _businessDbUser;


        private Dictionary<string, string> apiKeys;
        
        //Ninject kernel
        private IKernel _kernel;

        public UsersOperations()
        {
            _uow = new WorkSiteUow();

            _dbBusinessUser = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
            _businessDbUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>());
        }

        public void CreateUser(UserDto u, string password, string role)
        {
            u.Guid = Guid.NewGuid().ToString();
            u.Role = role;
            u.HashedPassword = SecurePasswordHasher.Hash(password);

            var mapper = _businessDbUser.CreateMapper();

            var destination = mapper.Map<UserDto, User>(u);

            try
            {
                _uow.Users.Create(destination);
                _uow.Save();
            }
            catch (Exception)
            {
                throw new InvalidUserException();
            }
        }

        public ICollection<UserDto> ShowUsers()
        {
            ICollection<UserDto> res = _kernel.Get<ICollection<UserDto>>();

            var users = _uow.Users.GetAll();
            var mapper = _dbBusinessUser.CreateMapper();

            foreach (var u in users)
            {
                res.Add(mapper.Map<User, UserDto>(u));
            }

            return res;
        }

        public void CreateResume(UserDto user, ResumeDto resume)
        {
            var dbUser = new User();
            var users = _uow.Users.GetAll();

            foreach (var u in users)
            {
                if (u.Email != user.Email) continue;
                dbUser = u;
                break;
            }

            var mapcfg = new MapperConfiguration(cfg => cfg.CreateMap<ResumeDto, Resume>());
            var mapper = mapcfg.CreateMapper();

            var res = mapper.Map<ResumeDto, Resume>(resume);

            res.User = dbUser;
            try
            {
                dbUser.Resume = res;
                _uow.Users.Update(dbUser);

                _uow.Resumes.Create(res);

                _uow.Save();
            }
            catch(Exception)
            {
                throw new InvalidResumeException();
            }

        }

    }
}