using Fintech.DTOs.Requests.Parceladas;
using Fintech.Interfaces;
using Fintech.Utils.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DespesasParceladasController : FinController
{
    private readonly IDespesaParceladaService _despesaParceladaService;

    public DespesasParceladasController(IDespesaParceladaService despesaParceladaService)
    {
        _despesaParceladaService = despesaParceladaService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NovaDespesaParceladaRequest request)
    {
        try
        {
            await _despesaParceladaService.Create(request);

            return Created("", request);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}