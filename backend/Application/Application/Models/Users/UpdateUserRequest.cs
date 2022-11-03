namespace Application.Models.Users;

using Application.Entities;
using System.ComponentModel.DataAnnotations;

public class UpdateUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EnumDataType(typeof(Role))]
    public string Role { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    // treat empty string as null for password fields to 
    // make them optional in front end apps
    private string _password;
    [MinLength(6)]
    public string Password
    {
        get => _password;
        set => _password = replaceEmptyWithNull(value);
    }

    public int PaidMoney { get; set; }
    public int Dept { get; set; }

    /*private int _paidMoney = 0;
    private int _dept = 0;
    public int PaidMoney
    {
        get => _paidMoney;
        set => _paidMoney = value;
    }
    public int Dept
    {
        get => _dept;
        set => _dept = value;
    }*/

    private string _confirmPassword;
    [Compare("Password")]
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => _confirmPassword = replaceEmptyWithNull(value);
    }

    // helpers

    private string replaceEmptyWithNull(string value)
    {
        // replace empty string with null to make field optional
        return string.IsNullOrEmpty(value) ? null : value;
    }
}