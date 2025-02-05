using Fintech.DTOs.DTO;
using Fintech.DTOs.Requests.Programadas;
using Fintech.DTOs.Responses;
using Fintech.Entities;
using Fintech.Enums;
using Fintech.Interfaces;
using Fintech.Utils;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Services;

public class DespesaProgramadaService : IDespesaProgramadaService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DespesaProgramadaService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AdicionarDespesaProgramada(NovaDespesaProgramadaRequest request)
    {
        var token = _httpContextAccessor.HttpContext!.Items["UserToken"] as TokenDTO;

        var dProgramada = new DespesaProgramada
        {
            DataInicial = request.DataInicial,
            DataFinal = request.DataFinal,
            Descricao = request.Descricao,
            Valor = request.Valor,
            CodigoUsuario = token.Id,
        };
        
        await _context.DespesaProgramada.AddAsync(dProgramada);
        await _context.SaveChangesAsync();
    }

    public async Task CheckForAvailableProgramadas()
    {
        var despesasProgramadasDoDia = await _context.DespesaProgramada.AsNoTracking()
            .Where(dp => dp.DataFinal >= DateTime.Now)
            .Where(dp => dp.DataInicial.Day == DateTime.Now.Day)
            .ToListAsync();

        if (despesasProgramadasDoDia.Count == 0)
        {
            return;
        }

        using var transaction = _context.Database.BeginTransactionAsync();
        
        var listDeProgramadas = despesasProgramadasDoDia.Select(dp => new Despesas
            {
                Data = DateTime.Now,
                CodigoUsuario = dp.CodigoUsuario,
                Valor = dp.Valor,
                Nome = dp.Descricao,
                CodigoDespesaParcelada = dp.Id,
                FormaDePagamento = FormaDePagamento.Credito,
                Origem = $"Despesa programada - {dp.Descricao} - {DateTime.Now.ToShortDateString()}",
            })
            .ToList();

        await _context.Despesas.AddRangeAsync(listDeProgramadas);
        await _context.SaveChangesAsync();
    }

    public async Task<GraficosBaseResponse> GetProximasProgramadas()
    {
        var token = _httpContextAccessor.HttpContext!.Items["UserToken"] as TokenDTO;

        var dProgramadasDoMes = await _context.DespesaProgramada
            .Where(dp => 
                dp.CodigoUsuario == token.Id
                && dp.DataInicial.Day >= DateTime.Now.Day
            )
            .AsNoTracking()
            .ToListAsync();

        if (dProgramadasDoMes.Any())
        {
            return new GraficosBaseResponse
            {
                Descriptions = dProgramadasDoMes.Select(d => d.Descricao).ToList(),
                Values = dProgramadasDoMes.Select(d => d.Valor).ToList(),
            };    
        }

        return null;
    }
}