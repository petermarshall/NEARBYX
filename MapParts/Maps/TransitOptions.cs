﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMapsComponents.Maps
{
    /// <summary>
    /// The TransitOptions object to be included in a DirectionsRequest when the travel mode is set to TRANSIT.
    /// </summary>
    public class TransitOptions
    {
        /// <summary>
        /// The desired arrival time for the route.
        /// If arrival time is specified, departure time is ignored.
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// The desired departure time for the route.
        /// If neither departure time nor arrival time is specified, the time is assumed to be "now".
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// One or more preferred modes of transit, such as bus or train. 
        /// If no preference is given, the API returns the default best route.
        /// </summary>
        public IEnumerable<TransitMode> Modes { get; set; }

        /// <summary>
        /// A preference that can bias the choice of transit route, such as less walking. 
        /// If no preference is given, the API returns the default best route.
        /// </summary>
        public TransitRoutePreference RoutingPreference { get; set; }
    }
}
