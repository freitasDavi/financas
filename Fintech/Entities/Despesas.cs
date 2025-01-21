﻿using Fintech.Enums;

namespace Fintech.Entities;

public class Despesas
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public DateTime Data { get; set; }
    public string Origem { get; set; }
    public FormaDePagamento FormaDePagamento { get; set; }
    public decimal Valor { get; set; }
}