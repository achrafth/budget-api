namespace Application.Models.Users;

using Application.Entities;
using System.ComponentModel.DataAnnotations;

public class CreateUserRequest
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EnumDataType(typeof(Role))]
    public string Role { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    private int _paidMoney = 0;
    private int _dept = 0;
    public int PaidMoney {
        get => _paidMoney;
        set =>  _paidMoney = 0;
    }
    public int Dept
    {
        get => _dept;
        set => _dept = 0;
    }
}