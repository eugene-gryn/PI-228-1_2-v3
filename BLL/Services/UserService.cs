using BLL.DTOs.User;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class UserService : AService
{
    public UserService(IUnitOfWork uow) : base(uow) { }
    
    //get user mainData by id/email
    //get user orders
    //get user cart
    
    //bool validate email/id-pass pair
    
    //create / register
    //delete


    public async Task<UserMainDataDTO?> Create(UserRegisterDTO registerDto)
    {
        var user = Mapper.Map<User>(registerDto);

        Database.Users.Create(user);
        Database.Save();

        return await GetMainData(user.Email);
    }



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