﻿using System;

using Newtonsoft.Json;

namespace GoogleMapsComponents.Maps
{
    /// <summary>
    /// Object literals are accepted in place of LatLng objects, as a convenience, in many places. 
    /// These are converted to LatLng objects when the Maps API encounters them.
    /// </summary>
    public class LatLngLiteral
    {
        /// <summary>
        /// Latitude in degrees. Values will be clamped to the range [-90, 90]. 
        /// This means that if the value specified is less than -90, it will be set to -90. 
        /// And if the value is greater than 90, it will be set to 90.
        /// </summary>
        [JsonProperty("lat")]
        public double Lat { get; set; }

        /// <summary>
        /// Longitude in degrees. Values outside the range [-180, 180] will be wrapped so that they fall within the range. 
        /// For example, a value of -190 will be converted to 170. A value of 190 will be converted to -170. 
        /// This reflects the fact that longitudes wrap around the globe.
        /// </summary>
        [JsonProperty("lng")]
        public double Lng { get; set; }

        public LatLngLiteral()
        {
        }

        public LatLngLiteral(double lng, double lat)
        {
            Lng = lng;
            Lat = lat;
        }

        public LatLngLiteral(decimal lng, decimal lat) : this(Convert.ToDouble(lng), Convert.ToDouble(lat))
        {
        }

        public override string ToString()
        {
            return $"{Lat}, {Lng}";
        }
    }
}
