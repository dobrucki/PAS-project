using System;
using NUnit.Framework;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace UnitTests.Models.Repositories
{
    [TestFixture]
    public class MockDataRepositoryTests
    {

        [Test]
        public void Add_ValidArgument_ArgumentAddedToList()
        {
            // Arrange
            var repository = new MockDataRepository<User>();
            const int id = 1;
            var entity = new User {};

            // Act
            repository.Add(entity);
            
            // Assert
            Assert.AreSame(entity, repository.GetById(id));
        }

        [Test]
        public void Add_NullArgument_ExceptionThrown()
        {
            // Arrange
            var repository = new MockDataRepository<User>();
            
            // Act
            void Action() => repository.Add(null);

            // Assert
            Assert.Throws<NullReferenceException>(Action);
        }

        [Test]
        public void Add_SameId_ExceptionNotThrown()
        {
            // Arrange
            var repository = new MockDataRepository<User>();
            var user1 = new User {Id = 100};
            var user2 = new User {Id = 100};
            repository.Add(user1);

            // Act
            void Action() => repository.Add(user2);

            // Assert
            Assert.DoesNotThrow(Action);
        }

        [Test]
        public void GetById_NotValidId_NullObjectReturned()
        {
            // Arrange
            var repository = new MockDataRepository<User>();
            var user = new User {Id = 100};
            repository.Add(user);
            
            // Act
            User User() => repository.GetById(int.MaxValue);
            
            // Assert
            Assert.IsNull(User());
        }

        [Test]
        public void Update_ValidArgument_NoExceptionsThrown()
        {
            // Arrange
            var repository = new MockDataRepository<User>();
            var user = new User {Email = "john@example.com"};
            const string updatedEmail = "jedrzej@example.com";
            var updatedUser = new User {Id = 1, Email = updatedEmail};
            repository.Add(user);
            
            // Act
            repository.Update(updatedUser);
            
            // Assert
            Assert.AreEqual(updatedEmail, repository.GetById(1).Email);
        }
    }
}