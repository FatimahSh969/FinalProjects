namespace SmartAirPort.Repositories.Base
{
    public interface IRepository <T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
