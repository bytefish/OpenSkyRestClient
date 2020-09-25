// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace OpenSkyBackend.Contracts
{
    public class StateVectorResponseDto
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("states")]
        public StateVectorDto[] States { get; set; }
    }
}
