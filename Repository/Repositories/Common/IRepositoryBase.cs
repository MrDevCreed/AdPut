namespace Data.Repositories.Common
{
    public interface IRepositoryBase<T>
    {
        void SaveChanges();

        void Add(T Entity);

        void Delete(T Entity);

        void Edit(T Entity);

        T Find(T Entity);

        T FindById(int id);
    }
}
