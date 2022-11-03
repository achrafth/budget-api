namespace Application.Services;

using Application.Entities;
using Application.Helpers;
using Application.Models.Expenses;
using AutoMapper;

public interface IExpenseService
{
    IEnumerable<Expense> GetAll();
    Expense GetById(Guid id);
    void Create(CreateExpenseRequest model);
    void Update(Guid id, UpdateExpenseRequest model);
    void Delete(Guid id);
}

public class ExpenseService : IExpenseService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public ExpenseService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Expense> GetAll()
    {
        return _context.Expenses;
    }

    public Expense GetById(Guid id)
    {
        return GetExpense(id);
    }

    public void Create(CreateExpenseRequest model)
    {
        // validate
        if (_context.Expenses.Any(x => x.DateTime == model.DateTime))
            throw new AppException("Expense on the same time '" + model.DateTime + "' already exists");

        // map model to new exoense object
        var expense = _mapper.Map<Expense>(model);

        // save expense
        _context.Expenses.Add(expense);
        _context.SaveChanges();
    }

    public void Update(Guid id, UpdateExpenseRequest model)
    {
        var expense = GetExpense(id);

        // validate
        if (model.DateTime != expense.DateTime && _context.Expenses.Any(x => x.DateTime == model.DateTime))
            throw new AppException("Expense on the same time '" + model.DateTime + "' already exists");

        if (string.IsNullOrEmpty(model.PaidBy))
            throw new AppException("The person who paid for this expense is missing");

        // copy model to expense and save
        _mapper.Map(model, expense);
        _context.Expenses.Update(expense);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var expense = GetExpense(id);
        _context.Expenses.Remove(expense);
        _context.SaveChanges();
    }

    // helper methods

    private Expense GetExpense(Guid id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense == null) throw new KeyNotFoundException("Expense not found");
        return expense;
    }
}