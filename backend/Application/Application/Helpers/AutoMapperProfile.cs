namespace Application.Helpers;

using Application.Entities;
using Application.Models.Expenses;
using Application.Models.Users;
using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> User
        CreateMap<CreateUserRequest, User>();
        CreateMap<CreateExpenseRequest, Expense>();

        // UpdateRequest -> User
        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    // ignore null role
                    if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                    return true;
                }
            ));

        CreateMap<UpdateExpenseRequest, Expense>()
         .ForAllMembers(x => x.Condition(
             (src, dest, prop) =>
             {
                 // ignore both null & empty string properties
                 if (prop == null) return false;
                 if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                 // ignore null role
                 if (x.DestinationMember.Name == "TypeExpense" && src.PaidBy == null) return false;

                 return true;
             }
         ));
    }
}