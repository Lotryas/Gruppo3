using Utility;

namespace Libreria.Models
{
    public class DAOLibro : IDAO
    {
        private static Database? _db;
        private static DAOLibro? _instance = null;

        public static DAOLibro GetInstance()
        {
            return _instance == null ? new() : _instance;
        }

        private DAOLibro()
        {
            _db = new(Config.ConnectionString.Value);
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Entity? Find(long id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Entity entity)
        {
            throw new NotImplementedException();
        }

        public List<Entity> ReadAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
