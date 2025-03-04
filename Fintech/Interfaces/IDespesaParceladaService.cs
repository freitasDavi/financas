using Fintech.DTOs.Requests.Parceladas;
using Fintech.Entities;

namespace Fintech.Interfaces;

public interface IDespesaParceladaService
{
    Task Create(NovaDespesaParceladaRequest request);
    Task<List<DespesaParcelada>> GetAllParcelasParcelada();
}