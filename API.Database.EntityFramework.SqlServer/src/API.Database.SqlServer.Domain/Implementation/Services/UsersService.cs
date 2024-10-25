using API.Database.SqlServer.Db.Models;
using API.Database.SqlServer.Db.Repository;
using API.Database.SqlServer.Domain.Implementation.Interfaces;
using API.Database.SqlServer.Models.Input;
using Microsoft.Extensions.Logging;

namespace API.Database.SqlServer.Domain.Implementation.Services
{
    public class UsersService : IUsersService
    {
        private readonly ILogger<UsersService> _logger;

        private readonly UsersRepository _usersRepository;

        public UsersService(
                ILogger<UsersService> logger,
                UsersRepository usersRepository
            )
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        public Task<Users?> GetByDocumentNumber(string documentNumber)
        {
            try
            {
                var Data = _usersRepository.GetByDocumentNumber(documentNumber);

                return Task.FromResult(Data)!;
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was a problem get the user: ", Ex.Message + Ex.StackTrace);

                return null;
            }
        }

        public Task<List<Users>?> GetAllActive()
        {
            try
            {
                var Data = _usersRepository.GetAllActive();

                return Task.FromResult(Data)!;
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was a problem get all users: ", Ex.Message + Ex.StackTrace);

                return null;
            }
        }

        public Task<bool> Register(RegisterUserInput registerUserInput)
        {
            try
            {
                _usersRepository.Save(new Users()
                {
                    Uid = Guid.NewGuid(),
                    Name = registerUserInput.Name,
                    DocumentNumber = registerUserInput.DocumentNumber,
                    Active = true,
                    CreatedAt = DateTime.Now
                });

                return Task.FromResult(true);
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was a problem registering the user: ", Ex.Message + Ex.StackTrace);

                return Task.FromResult(false);
            }
        }

        public Task<bool> Update(RegisterUserInput registerUserInput)
        {
            try
            {
                var Data = GetByDocumentNumber(registerUserInput.DocumentNumber);

                _usersRepository.ClearTracker();

                Data.Result!.Name = registerUserInput.Name;

                _usersRepository.Update(Data.Result);

                return Task.FromResult(true);
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was a problem update the user: ", Ex.Message + Ex.StackTrace);

                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteByDocumentNumber(string documentNumber)
        {
            try
            {
                var Data = GetByDocumentNumber(documentNumber);
                
                _usersRepository.ClearTracker();
                _usersRepository.Delete(Data.Result!);

                return Task.FromResult(true);
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was a problem delete the user: ", Ex.Message + Ex.StackTrace);

                return Task.FromResult(false);
            }
        }
    }
}