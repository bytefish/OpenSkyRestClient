// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace OpenSkyRestClient.Model
{
    public class Waypoint
    {
        /// <summary>
        /// Time which the given waypoint is associated with in 
        /// seconds since epoch (Unix time).
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// WGS-84 latitude in decimal degrees. Can be null.
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// WGS-84 longitude in decimal degrees. Can be null.
        /// </summary>
        public float? Longitude { get; set; }

        /// <summary>
        /// Barometric altitude in meters. Can be null.
        /// </summary>
        public float? BarometricAltitudeInMeters { get; set; }

        /// <summary>
        /// True track in decimal degrees clockwise from north (north=0°). Can be null.
        /// </summary>
        public float? TrueTrack { get; set; }

        /// <summary>
        /// Boolean value which indicates if the position was retrieved 
        /// from a surface position report.
        /// </summary>
        public bool OnGround { get; set; }
    }
}
