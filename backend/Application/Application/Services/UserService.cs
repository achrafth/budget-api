namespace Application.Services;

using Application.Entities;
using Application.Helpers;
using Application.Models.Users;
using AutoMapper;
using BCrypt.Net;

public interface IUserService
{
    IEnumerable<User> GetAll();

    IEnumerable<string> GetAllNames();
    User GetById(Guid id);

    User GetByName(string name);
    int GetUsersBudget();
    void Create(CreateUserRequest model);
    void Update(Guid id, UpdateUserRequest model);
    void Delete(Guid id);
}

public class UserService : IUserService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public UserService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public IEnumerable<string> GetAllNames()
    {
        return _context.Users.Select(x => x.FirstName).ToList();
    }

    public User GetById(Guid id)
    {
        return getUser(id);
    }

    public User GetByName(string name)
    {
        var user = _context.Users.FirstOrDefault(x => x.FirstName == name);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public int GetUsersBudget()
    {
        int budget = 0;
        var users = _context.Users;
        foreach (var user in users)
        {
            budget += user.PaidMoney;
        }
        return budget;
    }


    public void Create(CreateUserRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.Email == model.Email))
            throw new AppException("User with the email '" + model.Email + "' already exists");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // hash password
        user.PasswordHash = BCrypt.HashPassword(model.Password);

        // save user
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(Guid id, UpdateUserRequest model)
    {
        var user = getUser(id);

        // validate
        if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
            throw new AppException("User with the email '" + model.Email + "' already exists");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCrypt.HashPassword(model.Password);

        // copy model to user and save
        _mapper.Map(model, user);
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var user = getUser(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    // helper methods

    private User getUser(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}