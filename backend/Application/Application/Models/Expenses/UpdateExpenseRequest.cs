﻿namespace Application.Models.Expenses;

using Application.Entities;
using System.ComponentModel.DataAnnotations;

public class UpdateExpenseRequest
{
    [Required]
    [EnumDataType(typeof(TypeExpense))]
    public string TypeExpense { get; set; }

    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    public int Total { get; set; }

    [Required]
    public string PaidBy { get; set; }
}