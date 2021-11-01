﻿
// ReSharper disable InconsistentNaming

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoogleMapsComponents.Maps
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CollisionBehavior
    {
        [EnumMember(Value = "google.maps.CollisionBehavior.REQUIRED")]
        REQUIRED,
        [EnumMember(Value = "google.maps.CollisionBehavior.REQUIRED_AND_HIDES_OPTIONAL")]
        REQUIRED_AND_HIDES_OPTIONAL,
        [EnumMember(Value = "google.maps.CollisionBehavior.OPTIONAL_AND_HIDES_LOWER_PRIORITY")]
        OPTIONAL_AND_HIDES_LOWER_PRIORITY
    }
}
