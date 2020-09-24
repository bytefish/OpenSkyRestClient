// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace OpenSkyRestClient.Model
{
    public class BoundingBox
    {
        [JsonPropertyName("lamin")]
        public float LaMin { get; set; }

        [JsonPropertyName("lomin")]
        public float LoMin { get; set; }

        [JsonPropertyName("lamax")]
        public float LaMax { get; set; }

        [JsonPropertyName("lomax")]
        public float LoMax { get; set; }
    }
}
