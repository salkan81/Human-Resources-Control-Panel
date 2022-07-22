using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;


namespace HRMP.BLL.Services.Concrete
{
    public class MesajService : GenericService<Mesaj>, IMesajService
    {
        private readonly IMesajRepository mesajRepository;
        public MesajService(IMesajRepository _mesajRepository) : base(_mesajRepository)
        {
            mesajRepository = _mesajRepository;
        }
    }
}
