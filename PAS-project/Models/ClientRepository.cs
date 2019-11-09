using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class ClientRepository : IDataRepository<Client>
    {
        private readonly List<Client> _clients = new List<Client>();
        
        public void Add(Client item)
        {
            if (item is null) throw new Exception("Given client is null");
            if (_clients.Any(client => client.Id == item.Id)) throw new Exception("Given client already exists!");
            _clients.Add(item);
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

        public void Update(Client updatedClient)
        {
            if (updatedClient is null) throw new Exception("Given client is null!");
            var actualClient = _clients.FirstOrDefault(c => c.Id == updatedClient.Id);
            if (actualClient is null) throw new Exception("Given id does not match any client!");
            actualClient.Email = updatedClient.Email;
            actualClient.Sex = updatedClient.Sex;
            actualClient.FirstName = updatedClient.FirstName;
            actualClient.LastName = updatedClient.LastName;
            actualClient.PhoneNumber = updatedClient.PhoneNumber;
            actualClient.ZipCode = updatedClient.PhoneNumber;
        }

        public Client Delete(int id)
        {
            
        }
    }
}