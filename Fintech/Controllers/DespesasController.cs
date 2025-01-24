using Fintech.DTOs.Requests;
using Fintech.DTOs.Responses;
using Fintech.Entities;
using Fintech.Enums;
using Fintech.Exceptions;
using Fintech.Interfaces;
using Fintech.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DespesasController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IDespesasService _despesasService;
    
    public DespesasController(DataContext context, IDespesasService despesasService)
    {
        _context = context;
        _despesasService = despesasService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Despesas>>> Get()
    {
        try
        {
            var despesas = await _despesasService.GetAll();

            return Ok(despesas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
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
        catch (BadRequestException ex)
        {
            return BadRequest(new BadRequestResponse
            {
                Message = ex.Message,
                Data = ex.Data,
                Details = ex.Details
            });
        }
    }
}