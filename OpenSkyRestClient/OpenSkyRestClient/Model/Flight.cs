// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace OpenSkyRestClient.Model
{
    public class Flight
    {
        /// <summary>
        /// Unique ICAO 24-bit address of the transponder in hex 
        /// string representation. All letters are lower case.
        /// </summary>
        public string Icao24 { get; set; }

        /// <summary>
        /// Estimated time of departure for the flight as Unix 
        /// time (seconds since epoch).
        /// </summary>
        public int FirstSeen { get; set; }

        /// <summary>
        /// ICAO code of the estimated departure airport. Can be null 
        /// if the airport could not be identified.
        /// </summary>
        public string EstDepartureAirport { get; set; }

        /// <summary>
        /// Estimated time of arrival for the flight as Unix 
        /// time (seconds since epoch)
        /// </summary>
        public int LastSeen { get; set; }

        /// <summary>
        /// ICAO code of the estimated arrival airport. Can be null 
        /// if the airport could not be identified.
        /// </summary>
        public string EstArrivalAirport { get; set; }

        /// <summary>
        /// Callsign of the vehicle (8 chars). Can be null if no callsign 
        /// has been received. If the vehicle transmits multiple callsigns 
        /// during the flight, we take the one seen most frequently
        /// </summary>
        public string CallSign { get; set; }

        /// <summary>
        /// Horizontal distance of the last received airborne position to the 
        /// estimated departure airport in meters
        /// </summary>
        public int EstDepartureAirportHorizDistance { get; set; }

        /// <summary>
        /// Vertical distance of the last received airborne position to the 
        /// estimated departure airport in meters
        /// </summary>
        public int EstDepartureAirportVertDistance { get; set; }

        /// <summary>
        /// Horizontal distance of the last received airborne position to the 
        /// estimated arrival airport in meters
        /// </summary>
        public int EstArrivalAirportHorizDistance { get; set; }

        /// <summary>
        /// Vertical distance of the last received airborne position to the 
        /// estimated arrival airport in meters
        /// </summary>
        public int EstArrivalAirportVertDistance { get; set; }

        /// <summary>
        /// Number of other possible departure airports. These are airports in 
        /// short distance to estDepartureAirport.
        /// </summary>
        public int DepartureAirportCandidatesCount { get; set; }

        /// <summary>
        /// Number of other possible departure airports. These are airports in 
        /// short distance to estArrivalAirport.
        /// </summary>
        public int ArrivalAirportCandidatesCount { get; set; }
    }
}
