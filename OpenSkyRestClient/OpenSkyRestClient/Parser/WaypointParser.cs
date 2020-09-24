// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OpenSkyRestClient.Extensions;
using OpenSkyRestClient.Model;
using System.Text.Json;

namespace OpenSkyRestClient.Parser
{
    public static class WaypointParser
    {
        public static Waypoint Parse(JsonElement element)
        {
            if (!TryParse(element, out Waypoint result))
            {
                return null;
            }

            return result;
        }

        public static bool TryParse(JsonElement element, out Waypoint result)
        {
            result = null;

            if (element.ValueKind != JsonValueKind.Array)
            {
                return false;
            }

            if (element.GetArrayLength() != 6)
            {
                return false;
            }

            result = new Waypoint
            {
                Time = element[0].GetInt32(),
                Latitude = element[1].GetNullableFloat(),
                Longitude = element[2].GetNullableFloat(),
                BarometricAltitudeInMeters = element[3].GetNullableFloat(),
                TrueTrack = element[4].GetNullableFloat(),
                OnGround = element[5].GetBoolean()
            };

            return true;
        }
    }
}