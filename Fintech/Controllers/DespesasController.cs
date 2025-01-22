using Fintech.DTOs.Requests;
using Fintech.Entities;
using Fintech.Enums;
using Fintech.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Controllers;

[ApiController]
[Route("[controller]")]
public class DespesasController : ControllerBase
{
    private static List<Despesas> despesas3 = new List<Despesas>
    {
        new Despesas()
        {
            Id = 1,
            Data = DateTime.Now,
            Nome = "Lanche",
            Origem = "Restaurante",
            Valor =  37.50m,
            FormaDePagamento = FormaDePagamento.Dinheiro
        },
        new Despesas()
        {
            Id = 2,
            Data = DateTime.Now - TimeSpan.FromDays(7),
            Nome = "Conta de Energia",
            Origem = "Celesc",
            Valor =  124.03m,
            FormaDePagamento = FormaDePagamento.Boleto
        },
        new Despesas()
        {
            Id = 3,
            Data = DateTime.Now,
            Nome = "Roupas",
            Origem = "Insider",
            Valor =  79.99m,
            FormaDePagamento = FormaDePagamento.Credito
        },
    };

    private readonly DataContext _context;
    public DespesasController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Despesas>>> Get()
    {
        var despesas = await _context.Despesas.ToListAsync();
        
        return Ok(despesas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Despesas>> Get([FromRoute] int id)
    {
        var despesa = await _context.Despesas.FirstOrDefaultAsync(x => x.Id == id);

        if (despesa == null)
            return BadRequest("Expense not found!");
        
        return Ok(despesa);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] NovaDespesaRequest request)
    {
        var despesa = new Despesas
        {
            Data = request.Data,
            Valor = request.Valor,
            Nome = request.Nome,
            FormaDePagamento = request.FormaDePagamento,
            Origem = request.Origem,
        };

        await _context.Despesas.AddAsync(despesa);
        await _context.SaveChangesAsync();
        
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] NovaDespesaRequest request)
    {
        var despesa = await _context.Despesas.FirstOrDefaultAsync(d => d.Id == id);

        if (despesa == null)
            return BadRequest("Despesa não encontrada.");
        
        despesa.Data = request.Data;
        despesa.Valor = request.Valor;
        despesa.Nome = request.Nome;
        despesa.Origem = request.Origem;
        despesa.FormaDePagamento = request.FormaDePagamento;

        _context.Despesas.Update(despesa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var despesa = await _context.Despesas.FirstOrDefaultAsync(d => d.Id == id);

        if (despesa == null)
            return BadRequest("Expense not found!");

        _context.Despesas.Remove(despesa);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}