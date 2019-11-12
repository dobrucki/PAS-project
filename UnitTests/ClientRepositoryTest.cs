using NUnit.Framework;
using PAS_project.Models;

namespace UnitTests
{
    [TestFixture]
    public class ClientRepositoryTest
    {
        private static Client GenerateFakeClient()
        {
            var client = new Client(
                "Mateusz",
                "Wasilewski",
                true,
                "wasil_98@o2.pl",
                "530060645",
                "92-525"
            );
            return client;
        }

        [Test]
        public void Get_ClientById_ReturnsClientObject()
        {
            var repository = new ClientRepository();
            
        }
        
        [Test]
        public void Add_ClientObject_ObjectAddedToList()
        {
            // Arrange
            var repository = new ClientRepository();
            var client = GenerateFakeClient();
            // Act
            repository.Add(client);
            // Assert
            Assert.AreEqual(client, repository.Get(client.Id));
        }
    }
}