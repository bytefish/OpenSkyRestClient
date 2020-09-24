// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OpenSkyRestClient.Extensions;
using OpenSkyRestClient.Model;
using OpenSkyRestClient.Model.Response;
using System.Text.Json;

namespace OpenSkyRestClient.Parser
{
    public static class TrackResponseParser
    {
        public static TrackResponse Parse(string json)
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

            return Parse(rootElement);
        }

        public static TrackResponse Parse(JsonElement element)
        {
            return new TrackResponse
            {
                Icao24 = element.GetProperty("icao24").GetString(),
                StartTime = element.GetProperty("startTime").GetInt32(),
                EndTime = element.GetProperty("endTime").GetInt32(),
                CallSign = element.GetProperty("callsign").GetNullableString(),
                Path = GetWaypoints(element.GetProperty("path"))
            };
        }

        private static Waypoint[] GetWaypoints(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            Waypoint[] result = new Waypoint[element.GetArrayLength()];

            for (int arrayIdx = 0; arrayIdx < element.GetArrayLength(); arrayIdx++)
            {
                var jsonElement = element[arrayIdx];

                result[arrayIdx] = WaypointParser.Parse(jsonElement);
            }

            return result;
        }
    }
}
