using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyPiggyBank.Core.Communication.Account;
using MyPiggyBank.Core.Communication.Account.DTO;
using MyPiggyBank.Core.Communication.Account.Requests;
using MyPiggyBank.Core.Services.Account.Interface;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repositories.Interfaces;

namespace MyPiggyBank.Core.Services.Account.Model
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IMapper _mapper;

        public AccountService(IUserRepository repository, IPasswordHasher<User> hasher, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task SaveAccount(RegisterRequest register)
        {
           await Validate(register);

           var user = _mapper.Map<User>(register);
           user.PasswordHash = _hasher.HashPassword(user, register.Password);

           await _repository.Add(user);
        }

        public async Task<AccountInfo> Authenticate(LoginRequest loginInput)
        {
            var userEntity = await _repository.GetByEmail(loginInput.Email) ??
                throw new ArgumentException(AccountResources.AccountService_Authenticate_User_NotFound);

            var passResult = _hasher.VerifyHashedPassword(userEntity, userEntity.PasswordHash, loginInput.Password);

            if(passResult != PasswordVerificationResult.Success)
                throw new ArgumentException(AccountResources.AccountService_Authenticate_Password_Incorrect);

            return _mapper.Map<AccountInfo>(userEntity);
        }


        private async Task Validate(RegisterRequest register)
        {
            if (await _repository.IsAny(u => u.Email.ToLower() == register.Email.ToLower()))
                throw new ArgumentException(AccountResources.AccountService_Register_Email_Exists_Error);

            if (await _repository.IsAny(u => u.Username == register.UserName))
                throw new ArgumentException(AccountResources.AccountService_Register_Username_Exists_Error);
        }
    }
}
