using API.Database.SqlServer.Db.Models;
using API.Database.SqlServer.Models.Input;

namespace API.Database.SqlServer.Domain.Implementation.Interfaces
{
    public interface IUsersService
    {
        Task<bool> Register(RegisterUserInput registerUserInput);

        Task<bool> Update(RegisterUserInput registerUserInput);

        Task<List<Users>?> GetAllActive();

        Task<Users?> GetByDocumentNumber(string documentNumber);

        Task<bool> DeleteByDocumentNumber(string documentNumber);
    }
}