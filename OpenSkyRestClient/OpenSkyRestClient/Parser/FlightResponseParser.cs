// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OpenSkyRestClient.Model;
using OpenSkyRestClient.Model.Response;
using System.Text.Json;

namespace OpenSkyRestClient.Parser
{
    public static class FlightResponseParser
    {
        public static FlightResponse Parse(string json)
        {
            if (json == null)
            {
                return null;
            }

            var jsonDocument = JsonDocument.Parse(json);

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
