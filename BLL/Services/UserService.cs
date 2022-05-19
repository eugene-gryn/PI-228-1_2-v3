using BLL.DTOs.User;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class UserService : AService
{
    public UserService(IUnitOfWork uow) : base(uow) { }
    
    //get user mainData
    //get user orders
    //get user cart
    
    //bool validate email/id-pass pair
    
    //create
    //add
    //delete

    
    

    public async Task<UserMainDataDTO?> GetMainData(int userID)
    {
        var user = await Database.Users.Read().FirstOrDefaultAsync(usr => usr.ID == userID);
        if (user == null)
        {
            //TODO log?
            return null;
        }
        return Mapper.Map<UserMainDataDTO>(user);

    }
    
    public async Task<UserMainDataDTO?> GetMainData(string userEmail)
    {
        var user = await Database.Users.Read().FirstOrDefaultAsync(usr => usr.Email.Equals(userEmail));
        if (user == null)
        {
            //TODO log?
            return null;
        }
        return Mapper.Map<UserMainDataDTO>(user);
    }

}