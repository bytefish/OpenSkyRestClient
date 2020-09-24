// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using OpenSkyRestClient.Options;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenSkyRestClient.Test
{
    public class AuthenticatedRequestsTest
    {
        private readonly string credentialsJsonFile = @"D:\credentials.json";

        private class CredentialsDto
        {
            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("password")]
            public string Password { get; set; }
        }

        public string GetFileContent(string filename)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", filename);

            var text = File.ReadAllText(filePath);

            return text;
        }

        [Test]
        public async Task GetAllFlightsTest()
        {
            var client = new OpenSkyClient();
            var credentials = GetCredentialsFromFile(credentialsJsonFile);
            
            var begin = new DateTime(2020, 1, 1, 13, 10, 0);
            var end = new DateTime(2020, 1, 1, 15, 10, 0);

            var response = await client.GetAllFlightsBetweenAsync(begin, end, credentials);

            Assert.IsNotNull(response);
            Assert.AreEqual(2727, response.Flights.Length);
        }

        private Credentials GetCredentialsFromFile(string filename)
        {
            var content = File.ReadAllText(filename);
            var dto = JsonSerializer.Deserialize<CredentialsDto>(content);

            return new Credentials
            {
                Username = dto.Username,
                Password = dto.Password
            };
        }
    }
}