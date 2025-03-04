using Fintech.DTOs.Requests.Parceladas;
using Fintech.Entities;
using Fintech.Interfaces;
using Fintech.Utils.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Controllers;

[Authorize]
[ApiController]
[Route("api/despesas-parceladas")]
public class DespesasParceladasController : FinController
{
    private readonly IDespesaParceladaService _despesaParceladaService;

    public DespesasParceladasController(IDespesaParceladaService despesaParceladaService)
    {
        _despesaParceladaService = despesaParceladaService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<DespesaParcelada>),  StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ICollection<DespesaParcelada>>> Get()
    {
        try
        {
            var despesasP = await _despesaParceladaService.GetAllParcelasParcelada();

            return Ok(despesasP);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}