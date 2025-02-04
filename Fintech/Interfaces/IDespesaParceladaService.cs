using Fintech.DTOs.Requests.Parceladas;

namespace Fintech.Interfaces;

public interface IDespesaParceladaService
{
    Task Create(NovaDespesaParceladaRequest request);
}