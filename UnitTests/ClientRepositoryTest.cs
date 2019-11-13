using NUnit.Framework;
using PAS_project.Models;

namespace UnitTests
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private static User GenerateFakeUser()
        {
            var user = new User(
                "Mateusz",
                "Wasilewski",
                true,
                "wasil_98@o2.pl",
                "530060645",
                "92-525"
            );
            return user;
        }

        [Test]
        public void Get_UserById_ReturnsUserObject()
        {
            var repository = new UserRepository();
            
        }
        
        [Test]
        public void Add_UserObject_ObjectAddedToList()
        {
            // Arrange
            var repository = new UserRepository();
            var user = GenerateFakeUser();
            // Act
            repository.Add(user);
            // Assert
            Assert.AreEqual(user, repository.Get(user.Id));
        }
    }
}