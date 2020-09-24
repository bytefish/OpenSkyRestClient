// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using OpenSkyRestClient.Parser;
using System;
using System.IO;

namespace OpenSkyRestClient.Test
{
    public class JsonParsersTests
    {
        public string GetFileContent(string filename)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", filename);

            var text = File.ReadAllText(filePath);

            return text;
        }

        [Test]
        public void ParseStateVectorResponseTest()
        {
            var json = GetFileContent("all_state_vectors_response.json");

            var result = StateVectorResponseParser.Parse(json);

            Assert.IsNotNull(result);
        }
    }
}