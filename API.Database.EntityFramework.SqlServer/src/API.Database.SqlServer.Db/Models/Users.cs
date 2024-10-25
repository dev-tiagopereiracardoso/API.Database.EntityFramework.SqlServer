using System.ComponentModel.DataAnnotations;

namespace API.Database.SqlServer.Db.Models
{
    public class Users
    {
        [Key]
        public Guid Uid { get; set; }

        public string Name { set; get; }

        public string DocumentNumber { set; get; }

        public bool Active { set; get; }

        public DateTime CreatedAt { set; get; }
    }
}