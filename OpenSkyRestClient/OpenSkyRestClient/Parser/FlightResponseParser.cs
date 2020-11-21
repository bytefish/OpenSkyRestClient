// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OpenSkyRestClient.Model;
using OpenSkyRestClient.Model.Response;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenSkyRestClient.Parser
{
    public static class FlightResponseParser
    {
        public static async Task<FlightResponse> ParseAsync(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            var jsonDocument = await JsonDocument.ParseAsync(stream);

            if (jsonDocument == null)
            {
                return null;
            }

            var rootElement = jsonDocument.RootElement;
            
            if(rootElement.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            Flight[] items = new Flight[rootElement.GetArrayLength()];

            for(int arrayIdx = 0; arrayIdx < rootElement.GetArrayLength(); arrayIdx++)
            {
                items[arrayIdx] = FlightParser.Parse(rootElement[arrayIdx]);
            }

            return new FlightResponse
            {
                Flights = items
            };
        }
    }
}
