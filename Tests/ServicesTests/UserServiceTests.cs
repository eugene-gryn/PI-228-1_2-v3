using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class UserServiceTests : ABaseTest
    {
        private UserService _repos;

        public override void EndOperation()
        {
            base.EndOperation();
            _repos = new UserService(uow);
        }
    }
}
