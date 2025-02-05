using Fintech.DTOs.Requests.Programadas;
using Fintech.DTOs.Responses;
using Fintech.Interfaces;
using Fintech.Utils.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DespesasProgramadasController : FinController
{
    private readonly IDespesaProgramadaService _despesaProgramadaService;

    public DespesasProgramadasController(IDespesaProgramadaService despesaProgramadaService)
    {
        _despesaProgramadaService = despesaProgramadaService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NovaDespesaProgramadaRequest request)
    {
        try
        {
            await _despesaProgramadaService.AdicionarDespesaProgramada(request);

            return Created("", request);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        } 
    }

    [HttpGet("proximas")]
    public async Task<ActionResult<GraficosBaseResponse>> GetProximasProgramadas()
    {
        try
        {
            var response = await _despesaProgramadaService.GetProximasProgramadas();
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}