// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OpenSkyRestClient.Model;
using OpenSkyRestClient.Model.Response;
using System.Text.Json;

namespace OpenSkyRestClient.Parser
{
    public static class StateVectorResponseParser
    {
        public static StateVectorResponse Parse(string json)
        {
            if(json == null)
            {
                return null;
            }

            var jsonDocument = JsonDocument.Parse(json);
            
            if(jsonDocument == null)
            {
                return null;
            }

            var rootElement = jsonDocument.RootElement;

            return Parse(rootElement);
        }

        public static StateVectorResponse Parse(JsonElement element)
        {
            return new StateVectorResponse
            {
                Time = element.GetProperty("time").GetInt32(),
                States = GetStateVectors(element.GetProperty("states"))
            };
        }

        private static StateVector[] GetStateVectors(JsonElement element)
        {
            if(element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            StateVector[] result = new StateVector[element.GetArrayLength()];

            for(int arrayIdx = 0; arrayIdx < element.GetArrayLength(); arrayIdx++)
            {
                var jsonElement = element[arrayIdx];

                result[arrayIdx] = StateVectorParser.Parse(jsonElement);
            }

            return result;
        }
    }
}
