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
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            var flight = new Flight();
            
            flight.Icao24 = element.GetProperty("icao24").GetString();
            flight.FirstSeen = element.GetProperty("firstSeen").GetNullableInt32();
            flight.EstDepartureAirport = element.GetProperty("estDepartureAirport").GetNullableString();
            flight.LastSeen = element.GetProperty("lastSeen").GetNullableInt32();
            flight.EstArrivalAirport = element.GetProperty("estArrivalAirport").GetNullableString();
            flight.CallSign = element.GetProperty("callsign").GetNullableString();
            flight.EstDepartureAirportHorizDistance = element.GetProperty("estDepartureAirportHorizDistance").GetNullableInt32();
            flight.EstDepartureAirportVertDistance = element.GetProperty("estDepartureAirportVertDistance").GetNullableInt32();
            flight.EstArrivalAirportHorizDistance = element.GetProperty("estArrivalAirportHorizDistance").GetNullableInt32();
            flight.EstArrivalAirportVertDistance = element.GetProperty("estArrivalAirportVertDistance").GetNullableInt32();
            flight.DepartureAirportCandidatesCount = element.GetProperty("departureAirportCandidatesCount").GetNullableInt32();
            flight.ArrivalAirportCandidatesCount = element.GetProperty("arrivalAirportCandidatesCount").GetNullableInt32();

            return flight;

        }

    }
}
