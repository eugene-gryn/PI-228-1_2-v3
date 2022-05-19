using BLL.DTOs.User;
using DAL.UOW;

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

    
    

    public UserMainDataDTO? GetMainData(int userID)
    {
        var user = Database.Users.Read().FirstOrDefault(usr => usr.ID == userID);
        if (user == null)
        {
            //TODO log?
            return null;
        }
        return Mapper.Map<UserMainDataDTO>(user);

    }
    
    public UserMainDataDTO? GetMainData(string userEmail)
    {
        var user = Database.Users.Read().FirstOrDefault(usr => usr.Email.Equals(userEmail));
        if (user == null)
        {
            //TODO log?
            return null;
        }
        return Mapper.Map<UserMainDataDTO>(user);
    }

}