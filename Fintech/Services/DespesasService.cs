﻿using System.Globalization;
using Fintech.DTOs.DTO;
using Fintech.DTOs.Requests;
using Fintech.DTOs.Requests.Despesas;
using Fintech.DTOs.Responses;
using Fintech.Entities;
using Fintech.Exceptions;
using Fintech.Interfaces;
using Fintech.Utils;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Services;

public class DespesasService : IDespesasService
{
    private IAuthService _authService;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public DespesasService(IAuthService authService, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _authService = authService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ICollection<Despesas>> GetAll(GetDespesasFiltroRequest filtros)
    {
        var query =  _context.Despesas.AsNoTracking();

        if (filtros.nome is not null)
            query = query.Where(desp => desp.Nome.Contains(filtros.nome));
        
        if (filtros.mes is not null)
            query = query.Where(desp => desp.Data.Month == filtros.mes);
        
        return await query.ToListAsync();
    }
    public async Task<Despesas?> GetById(long id)
    {
        var despesa = await _context.Despesas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return despesa;
    }
    public async Task Create(NovaDespesaRequest request)
    {
        var token = _httpContextAccessor.HttpContext!.Items["UserToken"] as TokenDTO;

        var despesa = new Despesas
        {
            Data = request.Data,
            Valor = request.Valor,
            Nome = request.Nome,
            FormaDePagamento = request.FormaDePagamento,
            Origem = request.Origem,
            CodigoUsuario = token.Id,
        };
        
        await _context.Despesas.AddAsync(despesa);
        await _context.SaveChangesAsync();
    }
    public async Task Update(long id, NovaDespesaRequest request)
    {
        var despesa = await GetByIdWithTracking(id);

        if (despesa == null)
            throw new NotFoundException("Expense");
        
        despesa.Data = request.Data;
        despesa.Valor = request.Valor;
        despesa.Nome = request.Nome;
        despesa.Origem = request.Origem;
        despesa.FormaDePagamento = request.FormaDePagamento;

        _context.Despesas.Update(despesa);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(long id)
    {
        var despesa = await GetByIdWithTracking(id);

        if (despesa == null)
            throw new NotFoundException("Expense not found!");

        _context.Despesas.Remove(despesa);
        await _context.SaveChangesAsync();
    }

    private async Task<Despesas?> GetByIdWithTracking(long id)
    {
        return await _context.Despesas.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<GraficosBaseResponse> GetValoresProximosMeses()
    {
        var token = _httpContextAccessor.HttpContext!.Items["UserToken"] as TokenDTO;
        var despesas = await _context.Despesas.AsNoTracking()
            .Where(d => d.CodigoUsuario == token.Id)
            .Where(d => 
                d.Data.Month >= DateTime.Now.Month && 
                d.Data.Year == DateTime.Now.Year && 
                d.Data.Month < DateTime.Now.AddMonths(6).Month)
            .ToListAsync();

        var despesasProximosMeses = new GraficosBaseResponse
        {
            Descriptions = despesas.Select(d => d.Data.ToString("MMMM"))
                .Distinct()
                .OrderBy(m => DateTime.ParseExact(m, "MMMM", new CultureInfo("pt-BR")).Month)
                .ToList(),
            Values = despesas
                .GroupBy(d => d.Data.Month)
                .OrderBy(g => g.Key)
                .Select(g => g.Sum(d => d.Valor))
                .ToList()
        };

        
        return despesasProximosMeses;
    }
    
}