// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OpenSkyRestClient.Extensions;
using OpenSkyRestClient.Model;
using System.Text.Json;

namespace OpenSkyRestClient.Parser
{
    public class FlightParser
    {
        public static Flight Parse(JsonElement element)
        {
            if(element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return new Flight
            {
                Icao24 = element.GetProperty("icao24").GetString(),
                FirstSeen = element.GetProperty("firstSeen").GetInt32(),
                EstDepartureAirport = element.GetProperty("estDepartureAirport").GetNullableString(),
                LastSeen = element.GetProperty("lastSeen").GetInt32(),
                EstArrivalAirport = element.GetProperty("estArrivalAirport").GetNullableString(),
                CallSign = element.GetProperty("callsign").GetString(),
                EstDepartureAirportHorizDistance = element.GetProperty("estDepartureAirportHorizDistance").GetInt32(),
                EstDepartureAirportVertDistance = element.GetProperty("estDepartureAirportVertDistance").GetInt32(),
                EstArrivalAirportHorizDistance = element.GetProperty("estArrivalAirportHorizDistance").GetInt32(),
                EstArrivalAirportVertDistance = element.GetProperty("estArrivalAirportVertDistance").GetInt32(),
                DepartureAirportCandidatesCount = element.GetProperty("departureAirportCandidatesCount").GetInt32(),
                ArrivalAirportCandidatesCount = element.GetProperty("arrivalAirportCandidatesCount").GetInt32(),
            };
        }

    }
}
