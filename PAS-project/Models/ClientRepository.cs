using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class ClientRepository : IDataRepository<Client>
    {
        private readonly List<Client> _clients = new List<Client>();
        
        public Client Add(Client client)
        {
            if (_clients.Any(m => m.Id == client.Id)) throw new Exception("Given movie already exists!");
            var id = _clients.Count == 0 ? 1 : _clients.Last().Id;
            client.Id = id;
            _clients.Add(client);
            return client;
        }

        public Client Get(int id)
        {
            var result = _clients.First(client => client.Id == id);
            if (result is null) throw new Exception("Given id does not match any client!");
            return result;
        }

        public IEnumerable<Client> GetAll()
        {
            return _clients;
        }

        public Client Update(Client updatedClient)
        {
            
            var actualClient = _clients.FirstOrDefault(c => c.Id == updatedClient.Id);
            if (actualClient is null) throw new Exception("Given id does not match any client!");
            actualClient.Email = updatedClient.Email;
            actualClient.Sex = updatedClient.Sex;
            actualClient.FirstName = updatedClient.FirstName;
            actualClient.LastName = updatedClient.LastName;
            actualClient.PhoneNumber = updatedClient.PhoneNumber;
            actualClient.ZipCode = updatedClient.PhoneNumber;
            return actualClient;
        }

        public Client Delete(int id)
        {
            var foundClient = _clients.FirstOrDefault(c => c.Id == id);
            if(foundClient is null) throw new Exception("Given id does not match any client!");
            _clients.Remove(foundClient);
            return foundClient;
        }
    }
}