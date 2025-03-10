﻿using Fintech.DTOs.DTO;
using Fintech.DTOs.Requests.Parceladas;
using Fintech.Entities;
using Fintech.Enums;
using Fintech.Interfaces;
using Fintech.Utils;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Services;

public class DespesaParceladaService : IDespesaParceladaService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DespesaParceladaService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<DespesaParcelada>> GetAllParcelasParcelada()
    {
        var token = _httpContextAccessor.HttpContext!.Items["UserToken"] as TokenDTO;

        var despesas = await _context.DespesaParcelada.Where(dp => dp.CodigoUsuario == token.Id).ToListAsync();
        
        return despesas;
    }
    
    public async Task Create(NovaDespesaParceladaRequest request)
    {
        var token = _httpContextAccessor.HttpContext!.Items["UserToken"] as TokenDTO;

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var dParcelada = new DespesaParcelada
                {
                    Data = request.Data,
                    CodigoUsuario = token.Id,
                    Descricao = request.Descricao,
                    NumeroParcelas = request.NumeroParcelas,
                    TotalParcela = request.TotalParcela,
                    ValorParcela = request.ValorParcela
                };

                await _context.DespesaParcelada.AddAsync(dParcelada);
                await _context.SaveChangesAsync();

                var parcelasToCreate = new List<Despesas>();

                for (var i = 0; i < request.NumeroParcelas; i++)
                {
                    var parcela = new Despesas()
                    {
                        Data = dParcelada.Data.AddMonths(i),
                        FormaDePagamento = FormaDePagamento.Credito,
                        Nome = request.Descricao,
                        Valor = request.ValorParcela,
                        Origem = $"Compra parcelada dia {request.Data.ToShortDateString()} - Parcela {i + 1}",
                        CodigoDespesaParcelada = dParcelada.Id,
                        CodigoUsuario = token.Id,
                    };

                    parcelasToCreate.Add(parcela);
                }

                await _context.Despesas.AddRangeAsync(parcelasToCreate);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}