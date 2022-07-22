using HRMP.BLL.Abstract;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.BLL.Concrete
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IRepository<T> repository;
        public GenericService(IRepository<T> _repository)
        {
            this.repository = _repository;
        }
        public IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }

        public T GetById(int id)
        {
            return repository.GetById(id);
        }

        public bool Add(T entity)
        {
            return repository.Add(entity);
        }

        public bool Delete(T entity)
        {
            return repository.Delete(entity);
        }


        public bool Update(T entity)
        {
            return repository.Update(entity);
        }

        public bool ChangePassword(string password, string newPassword)
        {
            return repository.ChangePassword(password, newPassword);
        }
    }
}
