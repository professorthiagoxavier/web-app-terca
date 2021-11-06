using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IAtividadeService
    {
        Task<List<AtividadeModel>> BuscarAtividadesAsync();

        Task<AtividadeModel> SalvarAtividade(AtividadeModel model);
    }
}
