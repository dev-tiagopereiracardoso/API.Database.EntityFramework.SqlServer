using API.Database.SqlServer.Db.Models;
using Microsoft.Extensions.Logging;

namespace API.Database.SqlServer.Db.Repository
{
    public class UsersRepository : BaseRepository<Users>
    {
        public UsersRepository(
                ILogger<BaseRepository<Users>> logger,
                ApplicationDbContext db
            )
            : base(db, logger) { }

        public Users GetByDocumentNumber(string DocumentNumber)
        {
            return _db.Users.FirstOrDefault(x => x.DocumentNumber.Equals(DocumentNumber))!;
        }

        public List<Users> GetAllActive()
        {
            return _db.Users.Where(x => x.Active.Equals(true)).ToList();
        }
    }
}