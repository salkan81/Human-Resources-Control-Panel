namespace HRMP.BLL.Abstract
{
    public interface IGenericService<T> where T : class
    {
        //tablo içindeki herşeyi, getirir
        IEnumerable<T> GetAll();

        //tablo içinde girilen id ye sahip herşeyi getirir
        T GetById(int id);

        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool ChangePassword(string password, string newPassword);
    }
}
