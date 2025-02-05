using Fintech.DTOs.Requests.Programadas;
using Fintech.DTOs.Responses;

namespace Fintech.Interfaces;

public interface IDespesaProgramadaService
{
    Task AdicionarDespesaProgramada(NovaDespesaProgramadaRequest request);
    Task CheckForAvailableProgramadas();
    Task<GraficosBaseResponse> GetProximasProgramadas();
}