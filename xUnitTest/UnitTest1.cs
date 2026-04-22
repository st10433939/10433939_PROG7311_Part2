using _10433939_PROG7311_Part2.Data;
using _10433939_PROG7311_Part2.Models;
using System.Text;

namespace xUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1_AddClient_Successful()
        {
            //Create new
            var initialCount = ClientData.GetAllClients().Count();

            var newClient = new Client
            {
                name = "Ben Sharp",
                region = "South Africa"
            };
            //Action
            ClientData.AddClient(newClient);

            //New count
            var newCount = ClientData.GetAllClients().Count();
            Assert.Equal(initialCount + 1, newCount);

            Assert.True(newClient.clientId > 0, "Client should have assigned Id.");

            //Verify retrieval
            var retrievedClient = ClientData.GetClientById(newClient.clientId);
            Assert.NotNull(retrievedClient);
            Assert.Equal("Ben Sharp", retrievedClient.name);
        }

    }
}
