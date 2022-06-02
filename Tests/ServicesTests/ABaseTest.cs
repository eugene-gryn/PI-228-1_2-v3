using DAL.EF;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests
{
   
    public abstract class ABaseTest 
    {
        protected MainContext _context;
        protected IUnitOfWork uow;
        protected DbContextOptions<MainContext> _configuration;
        
        public ABaseTest()
        {
            _configuration = new DbContextOptionsBuilder<MainContext>()
            .UseInMemoryDatabase(databaseName: "DbTest")
            .Options;
            _context = new MainContext(_configuration);
            uow = new EFUnitOfWork(_context);
        }
        public virtual void EndOperation()
        {
            uow.Dispose();
            _context = new MainContext(_configuration);
            uow = new EFUnitOfWork(_context);
        }

        

        [OneTimeSetUp]
        public void SetUp()
        {
            _context.Database.EnsureCreated();

            TestFillDB.SeedDatabase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        
    }
}
