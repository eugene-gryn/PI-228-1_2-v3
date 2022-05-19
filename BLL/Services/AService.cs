using AutoMapper;
using BLL.Mapper;
using DAL.UOW;

namespace BLL.Services;

public abstract class AService : IDisposable
{
    protected IUnitOfWork Database;
    protected AutoMapper.Mapper Mapper;

    protected AService(IUnitOfWork uow)
    {
        Database = uow;
        
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MainProfile>(); });
        Mapper = new AutoMapper.Mapper(configuration);
    }
    
    

    public void Dispose()
    {
        Database.Dispose();
    }

}