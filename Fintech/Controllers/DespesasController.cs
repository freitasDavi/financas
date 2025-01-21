using Fintech.DTOs.Requests;
using Fintech.Entities;
using Fintech.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Controllers;

[ApiController]
[Route("[controller]")]
public class DespesasController : ControllerBase
{
    private static List<Despesas> despesas = new List<Despesas>
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

    [HttpGet]
    public ActionResult<List<Despesas>> Get()
    {
        return Ok(despesas);
    }

    [HttpGet("{id}")]
    public ActionResult<Despesas> Get([FromRoute] int id)
    {
        var despesa = despesas.Find(x => x.Id == id);

        if (despesa == null)
            return BadRequest("Expense not found!");
        
        return Ok(despesa);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] NovaDespesaRequest request)
    {
        var despesa = new Despesas
        {
            Id = despesas.Count + 1,
            Data = request.Data,
            Valor = request.Valor,
            Nome = request.Nome,
            FormaDePagamento = request.FormaDePagamento,
            Origem = request.Origem,
        };

        despesas.Add(despesa);
        
        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] NovaDespesaRequest request)
    {
        var despesa = despesas.Find(d => d.Id == id);

        if (despesa == null)
            return BadRequest("Despesa não encontrada.");
        
        despesa.Data = request.Data;
        despesa.Valor = request.Valor;
        despesa.Nome = request.Nome;
        despesa.Origem = request.Origem;
        despesa.FormaDePagamento = request.FormaDePagamento;

        despesas = despesas.Select(d =>
        {
            if (d.Id == id)
                d = despesa;

            return d;
        }).ToList();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var despesa = despesas.Find(d => d.Id == id);

        if (despesa == null)
            return BadRequest("Expense not found!");

        despesas.Remove(despesa);  
        
        return NoContent();
    }
}