namespace Application.Controllers;
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

    public ExpensesController(
        IExpenseService expenseService,
        IUserService userService,
        IMapper mapper)
    {
        _expenseService = expenseService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetExpenses()
    {
        var expenses = _expenseService.GetAll();
        return expenses != null ? Ok(expenses) : NotFound();
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
        _userService.AddUserPaidMoney(model);
        _userService.UpdateUsersDept();

        return Ok(new { message = "Expense created" });
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateExpense([FromRoute] Guid id, UpdateExpenseRequest model)
    {
        var expense = _expenseService.GetById(id);

        var previousPaidMoney = expense.Total;
        var updatedPaidMoney = model.Total;
        int differencePaidMoney;

        if (previousPaidMoney > updatedPaidMoney)
        {
            differencePaidMoney = previousPaidMoney - updatedPaidMoney;
        }
        else
        {
            differencePaidMoney = updatedPaidMoney - previousPaidMoney;
        }

        model.Total = differencePaidMoney;

        _expenseService.Update(id, model);
        _userService.UpdateUserPaidMoney(model);
        _userService.UpdateUsersDept();
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