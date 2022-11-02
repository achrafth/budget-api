namespace Application.Entities;

public class Expense
{
    public Guid ExpenseId { get; set; }
    public TypeExpense TypeExpense { get; set; }
    public DateTime DateTime { get; set; }
    public int Total { get; set; }
    public string PaidBy { get; set; }
}
