﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GoogleMapsComponents.Maps
{
    /// <summary>
    /// Animations that can be played on a marker. 
    /// Use the setAnimation method on Marker or the animation option to play an animation.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Animation
    {
        /// <summary>
        /// Marker bounces until animation is stopped.
        /// </summary>
        [EnumMember(Value = "google.maps.Animation.BOUNCE")]
        Bounce = 1,

        /// <summary>
        /// Marker falls from the top of the map ending with a small bounce.
        /// </summary>
        [EnumMember(Value = "google.maps.Animation.DROP")]
        Drop
    }
}
