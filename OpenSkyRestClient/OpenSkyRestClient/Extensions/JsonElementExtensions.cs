// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json;

namespace OpenSkyRestClient.Extensions
{
    public static class JsonElementExtensions
    {
        public static string GetNullableString(this JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return element.GetString();
        }

        public static int? GetNullableInt(this JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            if (!element.TryGetInt32(out int result))
            {
                return null;
            }

            return result;
        }

        public static float? GetNullableFloat(this JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            if (!element.TryGetSingle(out float result))
            {
                return null;
            }

            return result;
        }
    }
}
