using BLL.DTOs.User;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace BLL.Services;

public class UserService : AService
{
    public UserService(IUnitOfWork uow) : base(uow)
    {
    }

    //get user mainData by id/email
    //get user orders
    //get user cart

    //bool validate email/id-pass pair

    //create / register
    //delete

    public async Task<UserMainDataDTO?> Create(UserMainDataDTO mainDto)
    {
        var user = Mapper.Map<User>(mainDto);

        await Database.Users.Create(user);
        Database.Save();

        return await GetMainData(user.Email);
    }

    public async Task<UserMainDataDTO> GetMainData(int userID)
    {
        var user = await Database.Users.Read().AsNoTracking().FirstOrDefaultAsync(usr => usr.ID == userID);

        if (user == null) return null;
        return Mapper.Map<UserMainDataDTO>(user);
    }

    public async Task<UserMainDataDTO?> GetMainData(string userEmail)
    {
        var user = await Database.Users.Read().AsNoTracking().FirstOrDefaultAsync(usr => usr.Email.Equals(userEmail));

        if (user == null) return null;
        return Mapper.Map<UserMainDataDTO>(user);
    }


    public async Task Update(UserMainDataDTO mainDataDto)
    {
        //TODO validate DTO?
        //TODO check if works correct
        var user = Mapper.Map<User>(mainDataDto);
        await Database.Users.Update(user);
        Database.Save();
    }
    public async Task Remove(int id)
    {
        //TODO check if works correct
        //TODO check if removes correlated info
        var result = await Database.Users.Delete(id);
        if (result) Database.Save();
        else throw new Exception("Something went wrong!"); // TODO Make new Exception
    }


}