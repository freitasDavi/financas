using Fintech.DTOs.Requests;
using Fintech.Entities;

namespace Fintech.Interfaces;

public interface IDespesasService
{
    Task<ICollection<Despesas>> GetAll();
    Task<Despesas?> GetById(long id);
    Task Create (NovaDespesaRequest request);
    Task Update (long expenseId, NovaDespesaRequest request);
    Task Delete(long id);
}