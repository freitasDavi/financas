using Fintech.DTOs.Requests;
using Fintech.DTOs.Requests.Despesas;
using Fintech.DTOs.Responses;
using Fintech.Entities;
using Fintech.Enums;
using Fintech.Exceptions;
using Fintech.Interfaces;
using Fintech.Utils;
using Fintech.Utils.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DespesasController : FinController
{
    private readonly DataContext _context;
    private readonly IDespesasService _despesasService;
    
    public DespesasController(DataContext context, IDespesasService despesasService)
    {
        _context = context;
        _despesasService = despesasService;
    }
    
    [HttpGet("proximas")]
    public async Task<ActionResult<GraficosBaseResponse>> GetProximas()
    {
        try
        {
            var response = await _despesasService.GetValoresProximosMeses();
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Despesas>>> Get([FromQuery] GetDespesasFiltroRequest filtros)
    {
        try
        {
            var despesas = await _despesasService.GetAll(filtros);

            return Ok(despesas);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Despesas>> Get([FromRoute] int id)
    {
        try
        {
            var despesa = await _despesasService.GetById(id);

            if (despesa is null)
                return BadRequest("Expense not found!");
            ;
            return Ok(despesa);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] NovaDespesaRequest request)
    {
        try
        {
            await _despesasService.Create(request);
        
            return Created();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] NovaDespesaRequest request)
    {
        try
        {
            await _despesasService.Update(id, request);
        
            return NoContent();    
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _despesasService.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }


}