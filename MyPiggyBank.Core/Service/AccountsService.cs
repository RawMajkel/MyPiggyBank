using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Core.Protocol;

namespace MyPiggyBank.Core.Service {
    public class AccountsService : IAccountsService
    {
        private readonly IUsersRepository _repository;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IMapper _mapper;

        public AccountsService(IUsersRepository repository, IPasswordHasher<User> hasher, IMapper mapper)
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

        public async Task DeleteAccount(Guid userId)   
        {
            var userEntity = await _repository.Get(userId) ??
                throw new ArgumentException(ValidationResources.AccountService_User_NotFound);

            await _repository.Delete(userEntity);
        }

        public async Task<AuthenticateResult> Authenticate(LoginRequest loginInput)
        {
            var userEntity = await _repository.GetByEmail(loginInput.Email) ??
                throw new ArgumentException(ValidationResources.AccountService_User_NotFound);

            var passResult = _hasher.VerifyHashedPassword(userEntity, userEntity.PasswordHash, loginInput.Password);

            if(passResult != PasswordVerificationResult.Success)
                throw new ArgumentException(ValidationResources.AccountService_Authenticate_Password_Incorrect);

            return _mapper.Map<AuthenticateResult>(userEntity);
        }


        private async Task Validate(RegisterRequest register)
        {
            if (await _repository.IsAny(u => u.Email.ToLower() == register.Email.ToLower()))
                throw new ArgumentException(ValidationResources.AccountService_Register_Email_Exists_Error);

            if (await _repository.IsAny(u => u.Username == register.Username))
                throw new ArgumentException(ValidationResources.AccountService_Register_Username_Exists_Error);
        }
    }
}
