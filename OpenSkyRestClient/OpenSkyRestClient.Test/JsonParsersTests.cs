// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using OpenSkyRestClient.Parser;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OpenSkyRestClient.Test
{
    public class JsonParsersTests
    {
        [Test]
        public async Task ParseStateVectorResponseTest()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "all_state_vectors_response.json");

            using(var stream = File.OpenRead(filePath))
            {
                var result = await StateVectorResponseParser.ParseAsync(stream);

                Assert.IsNotNull(result);

                Assert.AreEqual(1600950860, result.Time);

                Assert.IsNotNull(result.States);
                Assert.AreEqual(1750, result.States.Length);

            }
        }
    }
}