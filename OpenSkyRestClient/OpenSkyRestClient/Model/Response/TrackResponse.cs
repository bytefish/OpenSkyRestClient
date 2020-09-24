// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace OpenSkyRestClient.Model.Response
{
    public class TrackResponse
    {
        public string Icao24 { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public string CallSign { get; set; }

        public Waypoint[] Path { get; set; }
    }
}
