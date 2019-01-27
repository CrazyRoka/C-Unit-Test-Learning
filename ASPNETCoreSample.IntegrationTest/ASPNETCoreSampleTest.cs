using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ASPNETCoreSample.IntegrationTest
{
    public class ASPNETCoreSampleTest : IDisposable
    {
        private TestServer _testServer;

        public ASPNETCoreSampleTest()
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            var requestBuilder = _testServer.CreateRequest("/");
            var response = await requestBuilder.GetAsync();
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello World!", responseString);
        }

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }
}
