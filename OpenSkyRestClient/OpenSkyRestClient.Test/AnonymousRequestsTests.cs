// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace OpenSkyRestClient.Test
{
    public class AnonymousRequestsTest
    {
        [Test]
        public async Task GetAllFlightsTest()
        {
            var client = new OpenSkyClient();
            
            var begin = new DateTime(2020, 1, 1, 13, 10, 0);
            var end = new DateTime(2020, 1, 1, 15, 10, 0);

            var response = await client.GetAllFlightsBetweenAsync(begin, end);

            Assert.IsNotNull(response);
            Assert.AreEqual(2727, response.Flights.Length);
        }
    }
}