namespace Utility
{
    public interface IDAO
    {
        public bool Delete(long id);
        public Entity? Find(long id);
        public bool Insert(Entity entity);
        public List<Entity> ReadAll();
        public bool Update(Entity entity);
    }
}