namespace Application.Controllers;

using Application.Entities;
using Application.Helpers;
using Application.Models.Expenses;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private IExpenseService _expenseService;
    private IUserService _userService;
    private IMapper _mapper;
    private DataContext _context;

    public ExpensesController(
        IExpenseService expenseService,
        IUserService userService,
        IMapper mapper,
              DataContext context)
    {
        _expenseService = expenseService;
        _userService = userService;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetExpenses()
    {
        var expenses = _expenseService.GetAll();
        return Ok(expenses);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetExpense([FromRoute] Guid id)
    {
        var user = _expenseService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult AddExpense(CreateExpenseRequest model)
    {
        _expenseService.Create(model);
        UpdateUserPaidMoney(model);
        UpdateUsersDept();
        _context.SaveChangesAsync();
        return Ok(new { message = "Expense created" });
    }

    [HttpPost]
    [Route("{user-budget}")]
    public IActionResult UpdateUserPaidMoney(CreateExpenseRequest model)
    {
        var user = _userService.GetByName(model.PaidBy);
        if (user == null)
        {
            return NotFound();
        }
        user.PaidMoney += model.Total;
        _context.Users.Update(user);
        _context.SaveChanges();
        return Ok(user);
    }

    [HttpPost]
    [Route("{user-dept}")]
    public IActionResult UpdateUsersDept()
    {
        int rollingPaidMoney = _userService.GetUsersBudget();
        var users = _userService.GetAll();

        if (users == null)
        {
            return NotFound();
        }

        foreach (var user in users)
        {
            user.Dept = user.PaidMoney - (rollingPaidMoney / users.ToArray().Length);
            _context.Users.Update(user);
        }

        _context.SaveChanges();

        return Ok(users);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateExpense([FromRoute] Guid id, UpdateExpenseRequest model)
    {
        _expenseService.Update(id, model);
        return Ok(new { message = "Expense updated" });
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteExpense([FromRoute] Guid id)
    {
        _expenseService.Delete(id);
        return Ok(new { message = "Expense deleted" });
    }
}