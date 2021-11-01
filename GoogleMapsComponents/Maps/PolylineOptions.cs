﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMapsComponents.Maps
{
    /// <summary>
    /// PolylineOptions object used to define the properties that can be set on a Polyline.
    /// </summary>
    public class PolylineOptions : ListableEntityOptionsBase
    {
        /// <summary>
        /// Indicates whether this Polyline handles mouse events. 
        /// Defaults to true.
        /// </summary>
        public bool? Clickable { get; set; }

        /// <summary>
        /// If set to true, the user can drag this shape over the map. 
        /// The geodesic property defines the mode of dragging. Defaults to false.
        /// </summary>
        public bool? Draggable { get; set; }

        /// <summary>
        /// If set to true, the user can edit this shape by dragging the control points shown at the vertices and on each segment. 
        /// Defaults to false.
        /// </summary>
        public bool? Editable { get; set; }

        /// <summary>
        /// When true, edges of the polygon are interpreted as geodesic and will follow the curvature of the Earth. When false, edges of the polygon are rendered as straight lines in screen space. 
        /// Note that the shape of a geodesic polygon may appear to change when dragged, as the dimensions are maintained relative to the surface of the earth. 
        /// Defaults to false.
        /// </summary>
        public bool? Geodesic { get; set; }

        /// <summary>
        /// The icons to be rendered along the polyline.
        /// </summary>
        public IEnumerable<IconSequence> Icons { get; set; }

        /// <summary>
        /// Map on which to display Polyline.
        /// </summary>
        [JsonConverter(typeof(JsObjectRefConverter<Map>))]
        public Map Map { get; set; }

        /// <summary>
        /// The ordered sequence of coordinates of the Polyline.
        /// </summary>
        public IEnumerable<LatLngLiteral> Path { get; set; }

        /// <summary>
        /// The stroke color. All CSS3 colors are supported except for extended named colors.
        /// </summary>
        public string StrokeColor { get; set; }

        /// <summary>
        /// The stroke opacity between 0.0 and 1.0.
        /// </summary>
        public float? StrokeOpacity { get; set; }

        /// <summary>
        /// The stroke width in pixels.
        /// </summary>
        public int? StrokeWeight { get; set; }

        /// <summary>
        /// Whether this polyline is visible on the map. Defaults to true.
        /// </summary>
        public bool? Visible { get; set; }

        /// <summary>
        /// The zIndex compared to other polys.
        /// </summary>
        public int? ZIndex { get; set; }
    }
}
