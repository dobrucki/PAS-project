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

        public void Update(Client item, int id)
        {
            if (item is null) throw new Exception("Given client is null!");
            try
            {
                var index = _clients.FindIndex(client => client.Id == id);
                _clients[index] = item;
            }
            catch (ArgumentNullException)
            {
                throw new Exception("Given id does not match any client!");
            }
        }

        public void Remove(int id)
        {
            _clients.Remove(Get(id));
        }

        public void Remove(Client item)
        {
            _clients.Remove(item);
        }
    }
}