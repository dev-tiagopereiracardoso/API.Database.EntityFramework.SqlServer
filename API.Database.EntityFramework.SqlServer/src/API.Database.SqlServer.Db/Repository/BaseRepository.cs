using Microsoft.Extensions.Logging;

namespace API.Database.SqlServer.Db.Repository
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;

        private readonly ILogger<BaseRepository<T>> _logger;

        protected BaseRepository(
                ApplicationDbContext db, 
                ILogger<BaseRepository<T>> logger
            )
        {
            _db = db;
            _logger = logger;
        }

        public List<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _db.Set<TEntity>().ToList();
        }

        public virtual T? GetById(params object?[]? keyValues)
        {
            return _db.Find<T>(keyValues);
        }

        public virtual void Save(T entity)
        {
            try
            {
                _db.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Houve um problema na gravação: ", e.Message + e.StackTrace);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                _db.Update(entity);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Houve um problema na atualização: ", e.Message + e.StackTrace);
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                _db.Remove(entity);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Houve um problema na exclusão: ", e.Message + e.StackTrace);
            }
        }

        public void SaveRange(IEnumerable<T> entities)
        {
            try
            {
                _db.AddRange(entities);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Houve um problema na gravação do range: ", e.Message + e.StackTrace);
            }
        }

        public void ClearTracker()
        {
            _db.ChangeTracker.Clear();
        }
    }
}