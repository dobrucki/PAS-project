using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class ClientManager
    {
        private readonly IDataRepository<Client> _repository;

        public ClientManager(IDataRepository<Client> repository)
        {
            _repository = repository;
        }

        public Client AddClient(Client client)
        {
            if(client is null) throw new Exception("Given client is null");
            
            _repository.Add(client);
            return client;
        }

        public Client GetClient(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _repository.GetAll();
        }

        public Client UpdateClient(Client updatedClient)
        {
            if (updatedClient is null) throw new Exception("Given client is null!");

            return _repository.Update(updatedClient);
            
        }
        public Client DeleteClient(int id)
        {
            return _repository.Delete(id);
        }

        public void ActiveClient(Client client)
        {
            if(client.Activity) throw new Exception("Given client is already activated!");
            client.Activity = true;
        }

        public void DeactiveClient(Client client)
        {
            if(client.Activity == false) throw new Exception("Given client is already deactivated!");
            client.Activity = false;
        }

        public Client FilterByEmails(string email)
        {
            var result = _repository.GetAll().First(cl => cl.Email == email);
            if(result == null) throw new Exception("Given email does not match any client!");
            return result;
        }

    }
}