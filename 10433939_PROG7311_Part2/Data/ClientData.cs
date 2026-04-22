using _10433939_PROG7311_Part2.Models;

namespace _10433939_PROG7311_Part2.Data
{
    public class ClientData
    {
        private static List<Client> _client = new List<Client>() { };

        private static int _nextId = 4;
        private static int _nextReviewId = 1;

        public static List<Client> GetAllClients() => _client.ToList();

        public static Client? GetClientById(int id) =>
            _client.FirstOrDefault(b => b.clientId == id);

        public static void AddClient(Client client)
        {
            client.clientId = _nextId;
            _nextId++;
            _client.Add(client);
        }
    }
}
