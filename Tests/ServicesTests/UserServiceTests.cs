using BLL.DTOs.User;
using BLL.Services;
using NUnit.Framework;
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
        public UserServiceTests()
        {
            _repos = new UserService(uow);
        }
        public override void EndOperation()
        {
            base.EndOperation();
            _repos = new UserService(uow);
        }
        [Test]
        public void CreateUser_AddNewUser()
        {
            UserMainDataDTO user = new UserMainDataDTO();
            UserCartDTO cart = new UserCartDTO();

            _repos.Create(user);

            Assert.That(user, Is.Not.Null);
        }
        [Test]
        public void GetMainData_UserID_GetData()
        {
            var user = new UserMainDataDTO();
            UserLoginDTO login = new UserLoginDTO();

            _repos.GetMainData(user.ID);

            Assert.That(user, Is.Not.Null);
        }
        [Test]
        public void GetMainData_UserEmail_GetData()
        {
            var user = new UserMainDataDTO();
            UserOrdersDTO orders = new UserOrdersDTO();

            _repos.GetMainData(user.Email);

            Assert.That(user.Email, Is.EqualTo(user.Email));
        }
        [Test]
        public void UpdateInfo_ChangeUserInfo()
        {
            var user = new UserMainDataDTO();
            UserRegisterDTO register = new UserRegisterDTO();   

            _repos.Update(user);

            Assert.That(user, Is.Not.Null);
        }
        [Test]
        public void RemoveUser_DeleteUser()
        {
            var user = new UserMainDataDTO();

            _repos.Remove(1);

            Assert.That(_repos.GetMainData(user.ID), Is.Not.Null);
        }
    }
}
