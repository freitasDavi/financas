using Fintech.DTOs.Requests;
using Fintech.DTOs.Requests.Despesas;
using Fintech.Entities;

namespace Fintech.Interfaces;

public interface IDespesasService
{
    Task<ICollection<Despesas>> GetAll(GetDespesasFiltroRequest filtros);
    Task<Despesas?> GetById(long id);
    Task Create (NovaDespesaRequest request);
    Task Update (long expenseId, NovaDespesaRequest request);
    Task Delete(long id);
}