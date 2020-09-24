// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OpenSkyRestClient.Extensions;
using OpenSkyRestClient.Model;
using System;
using System.Text.Json;

namespace OpenSkyRestClient.Parser
{
    public static class StateVectorParser
    {
        public static StateVector Parse(JsonElement element)
        {
            if(!TryParse(element, out StateVector result))
            {
                return null;
            }

            return result;
        }

        public static bool TryParse(JsonElement element, out StateVector result)
        {
            result = null;

            if (element.ValueKind != JsonValueKind.Array)
            {
                return false;
            }

            if (element.GetArrayLength() != 17)
            {
                return false;
            }

            result = new StateVector
            {
                Icao24 = element[0].GetNullableString(),
                CallSign = element[1].GetNullableString(),
                OriginCountry = element[2].GetNullableString(),
                TimePosition = element[3].GetNullableInt(),
                LastContact = element[4].GetNullableInt(),
                Longitude = element[5].GetNullableFloat(),
                Latitude = element[6].GetNullableFloat(),
                BarometricAltitude = element[7].GetNullableFloat(),
                OnGround = element[8].GetBoolean(),
                Velocity = element[9].GetNullableFloat(),
                TrueTrack = element[10].GetNullableFloat(),
                VerticalRate = element[11].GetNullableFloat(),
                Sensors = GetIntArray(element[12]),
                GeometricAltitudeInMeters = element[13].GetNullableFloat(),
                Squawk = element[14].GetNullableString(),
                Spi = element[15].GetBoolean(),
                PositionSource = GetPositionSource(element[16])
            };

            return true;
        }

        private static int[] GetIntArray(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            if (element.ValueKind != JsonValueKind.Array)
            {
                return null;
            }

            int[] result = new int[element.GetArrayLength()];

            for (int arrayIdx = 0; arrayIdx < element.GetArrayLength(); arrayIdx++)
            {
                result[arrayIdx] = element[arrayIdx].GetInt32();
            }

            return result;
        }

        private static PositionSourceEnum? GetPositionSource(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            var value = element.GetInt32();

            if (!Enum.IsDefined(typeof(PositionSourceEnum), value))
            {
                return null;
            }

            return (PositionSourceEnum)Enum.ToObject(typeof(PositionSourceEnum), value);
        }
    }
}
