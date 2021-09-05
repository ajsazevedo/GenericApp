using FluentValidation;
using GenericApp.Application.Services.Base;
using GenericApp.Application.Validators;
using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Dto.Request;
using GenericApp.Domain.Interfaces.Services;
using GenericApp.Domain.Interfaces.Services.Entity;
using GenericApp.Domain.Models;
using GenericApp.Infra.CC;
using GenericApp.Infra.CC.Localization.Resources;
using GenericApp.Infra.Common.Exceptions;
using GenericApp.Infra.Common.Utils;
using GenericApp.Infra.Data.Interfaces;
using System;
using Wis.Common.Objects;

namespace GenericApp.Application.Services.Entity
{
    public class UserService : BaseDbService<UserDto, User>, IUserService
    {
        private readonly IMailService _mailService;

        public UserService(
            IMailService mailService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mailService = mailService;

        }

        public override UserDto Add(UserDto obj)
        {
            ValidateUserCreation(obj);
            var addedUser = base.Add(obj);
            return addedUser;
        }

        public override UserDto Update(long id, UserDto obj)
        {
            var oldUser = FindById(id);
            if (oldUser.Email != obj.Email)
                CheckUniqueEmail(obj.Email);
            obj.Password = oldUser.Password;
            obj.Id = oldUser.Id;
            oldUser = obj;
            var updatedUser = ToDto(_repository.Update(ToEntity(oldUser)));
            return updatedUser;
        }

        private int GetMonthsToExpirePassword()
        {
            return Convert.ToInt32(Global.Instance.GetConfigurationFile().GetSection("Security")["PasswordExpirationMonths"]);
        }

        private void ValidateUserCreation(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Password))
                SetDefaultPassword(userDto);
            userDto.PasswordValidDate = DateTime.Now;
            ServiceValidateuser(userDto);

            CheckUniqueEmail(userDto.Email);
        }

        void CheckUniqueEmail(string email)
        {
            var user = FindOne(x => x.Email == email && x.Active);

            if (user != null)
                throw new ServiceException(string.Format(SharedResource.AlreadyRegistered, SharedResource.User));
        }

        private void ServiceValidateuser(UserDto user)
        {
            var validator = Activator.CreateInstance<UserValidator>();
            validator.ValidateAndThrow(user);
        }

        private string EncryptPassword(string password)
        {
            return Encryption.ToSHA256LowerCase(password);
        }

        private void SetDefaultPassword(UserDto user)
        {
            var password = Password.Generate(8, 1);
            _mailService.SendNewPassword(password, user.Email, user.Name);
            user.Password = EncryptPassword(password);
        }

        public Result SetDefaultPassword(string username)
        {
            var user = FindByUsername(username);
            SetDefaultPassword(user);
            base.Update(user.Id, user);
            return Result.Successfull(SharedResource.PasswordReseted);
        }

        public UserDto FindByUsername(string username)
        {
            var user = FindOne(x => x.Email == username);
            if (user == null)
                throw new ServiceException(SharedResource.UserNotRegistered);

            return user;
        }

        public UserDto FindById(long id)
        {
            var user = Get(id);
            if (user == null)
                throw new ServiceException(SharedResource.UserNotRegistered);

            return user;
        }

        public UserDto FindByLogin(CredencialsDto userLogin)
        {
            var user = FindByUsername(userLogin.Login);

            if (user.Password != EncryptPassword(userLogin.Password))
                throw new ServiceException(SharedResource.InvalidPassword);

            if (!user.Active)
                throw new ServiceException(string.Format(SharedResource.Inactive, SharedResource.User));

            return user;
        }

        public Result ChangePassword(ChangeCredencialsDto credencials)
        {
            var user = FindByLogin(credencials);
            user.Password = EncryptPassword(credencials.NewPassword);
            user.PasswordValidDate = DateTime.Now.AddMonths(GetMonthsToExpirePassword());
            base.Update(user.Id, user);
            return Result.Successfull(SharedResource.PasswordUpdated);
        }
    }
}
