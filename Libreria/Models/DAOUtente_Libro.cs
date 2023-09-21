using Utility;

namespace Libreria.Models
{
    public class DAOUtente_Libro : IDAO
    {
        private static Database? _db;
        private static DAOUtente_Libro? _instance = null;

        public static DAOUtente_Libro GetInstance()
        {
            return _instance == null ? new() : _instance;
        }

        private DAOUtente_Libro()
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
