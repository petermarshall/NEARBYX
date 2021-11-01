﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMapsComponents.Maps.Drawing
{
    /// <summary>
    /// Allows users to draw markers, polygons, polylines, rectangles, and circles on the map. 
    /// The DrawingManager's drawing mode defines the type of overlay that will be created by the user. 
    /// Adds a control to the map, allowing the user to switch drawing mode.
    /// </summary>
    public class DrawingManager : IDisposable
    {
        private readonly JsObjectRef _jsObjectRef;
        private Map _map;

        /// <summary>
        /// Creates a DrawingManager that allows users to draw overlays on the map, and switch between the type of overlay to be drawn with a drawing control.
        /// </summary>
        public async static Task<DrawingManager> CreateAsync(IJSRuntime jsRuntime, DrawingManagerOptions opts = null)
        {
            var jsObjectRef = await JsObjectRef.CreateAsync(jsRuntime, "google.maps.drawing.DrawingManager", opts);

            var obj = new DrawingManager(jsObjectRef, opts);

            return obj;
        }

        /// <summary>
        /// Creates a DrawingManager that allows users to draw overlays on the map, and switch between the type of overlay to be drawn with a drawing control.
        /// </summary>
        private DrawingManager(JsObjectRef jsObjectRef, DrawingManagerOptions opt = null)
        {
            _jsObjectRef = jsObjectRef;

            if (opt?.Map != null)
                _map = opt.Map;
        }

        public void Dispose()
        {
            _jsObjectRef.Dispose();
        }

        /// <summary>
        /// Returns the DrawingManager's drawing mode.
        /// </summary>
        /// <returns></returns>
        public async Task<OverlayType> GetDrawingMode()
        {
            var result = await _jsObjectRef.InvokeAsync<string>("getDrawingMode");

            return Helper.ToEnum<OverlayType>(result);
        }

        /// <summary>
        /// Returns the Map to which the DrawingManager is attached, which is the Map on which the overlays created will be placed.
        /// </summary>
        /// <returns></returns>
        public Map GetMap()
        {
            return _map;
        }

        /// <summary>
        /// Changes the DrawingManager's drawing mode, which defines the type of overlay to be added on the map. 
        /// Accepted values are 'marker', 'polygon', 'polyline', 'rectangle', 'circle', or null. 
        /// A drawing mode of null means that the user can interact with the map as normal, and clicks do not draw anything.
        /// </summary>
        /// <returns></returns>
        public Task SetDrawingMode(OverlayType? drawingMode)
        {
            return _jsObjectRef.InvokeAsync(
                "setDrawingMode",
                drawingMode);
        }

        /// <summary>
        /// Attaches the DrawingManager object to the specified Map.
        /// </summary>
        /// <param name="map"></param>
        public async Task SetMap(Map map)
        {
            await _jsObjectRef.InvokeAsync(
                   "googleMapDrawingManagerJsFunctions.setMap",
                   map);

            _map = map;
        }

        /// <summary>
        /// Sets the DrawingManager's options.
        /// </summary>
        /// <param name="options"></param>
        public Task SetOptions(DrawingManagerOptions options)
        {
            return _jsObjectRef.InvokeAsync(
                   "setOptions",
                   options);
        }

        public async Task<MapEventListener> AddListener(string eventName, Action handler)
        {
            var listenerRef = await _jsObjectRef.InvokeWithReturnedObjectRefAsync(
                "addListener", eventName, handler);

            return new MapEventListener(listenerRef);
        }

        public async Task AddOverlayCompleteListener(Action<OverlayCompleteEvent> action)
        {
            void Act(OverlaycompleteArgs args)
            {
                var completeEvent = new OverlayCompleteEvent();
                var reference = new JsObjectRef(_jsObjectRef.JSRuntime, args.uuid);
                switch (args.type)
                {
                    case "polygon":
                        completeEvent.Polygon = new Polygon(reference);
                        completeEvent.Type = OverlayType.Polygon;
                        break;
                    case "marker":
                        completeEvent.Marker = new Marker(reference);
                        completeEvent.Type = OverlayType.Marker;
                        break;
                    case "polyline":
                        completeEvent.Polyline = new Polyline(reference);
                        completeEvent.Type = OverlayType.Polyline;
                        break;
                    case "rectangle":
                        completeEvent.Rectangle = new Rectangle(reference);
                        completeEvent.Type = OverlayType.Rectangle;
                        break;
                    case "circle":
                        completeEvent.Circle = new Circle(reference);
                        completeEvent.Type = OverlayType.Circle;
                        break;
                }

                action.Invoke(completeEvent);
            }

            await _jsObjectRef.JSRuntime.MyInvokeAsync("googleMapsObjectManager.drawingManagerOverlaycomplete",
                new object[] { this._jsObjectRef.Guid.ToString(), (Action<OverlaycompleteArgs>)Act });

            return;
        }

        public async Task<MapEventListener> AddListener<T>(string eventName, Action<T> handler)
        {
            var listenerRef = await _jsObjectRef.InvokeWithReturnedObjectRefAsync(
                "addListener", eventName, handler);

            return new MapEventListener(listenerRef);
        }

        /// <summary>
        /// Object is created in drawingManagerOverlaycomplete function in objectManager.js
        /// </summary>
        private class OverlaycompleteArgs
        {
            // ReSharper disable once InconsistentNaming
            public Guid uuid { get; set; }
            // ReSharper disable once InconsistentNaming
            public string type { get; set; }
        }

    }


}
