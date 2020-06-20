using MyPiggyBank.Core.Protocol;
using MyPiggyBank.Core.Protocol.Account.DTO;

namespace MyPiggyBank.Core.Service {

    public interface IJwtService {
        AuthorizationToken GenerateToken(AccountInfo accountInfo);
    }
}
