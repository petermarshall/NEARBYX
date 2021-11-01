﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapsComponents.Maps.Extension
{
    /// <summary>
    /// A class able to manage a lot of Circle objects and get / set their
    /// properties at the same time, eventually with different values
    /// Main concept is that each Circle to can be distinguished by other ones need
    /// to have a "unique key" with a "external world mean", so not necessary it's GUID
    ///
    /// All properties should be called With a Dictionary<string, {property type}> indicating for each Circle(related to that key) the corresponding related property value
    /// </summary>
    public class CircleList : ListableEntityListBase<Circle, CircleOptions>
    {
        public Dictionary<string, Circle> Circles => base.BaseListableEntities;

        /// <summary>
        /// Create circles list
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="opts">Dictionary of desired Circle keys and CircleOptions values. Key as any type unique key. Not nessary Guid</param>
        /// <returns>new instance of CircleList class will be returned with its Circles dictionary member populated with the corresponding results</returns>
        public static async Task<CircleList> CreateAsync(IJSRuntime jsRuntime, Dictionary<string, CircleOptions> opts)
        {
            JsObjectRef jsObjectRef = new JsObjectRef(jsRuntime, Guid.NewGuid());

            CircleList obj;
                Dictionary<string, JsObjectRef> jsObjectRefs = await JsObjectRef.CreateMultipleAsync(
                    jsRuntime,
                    "google.maps.Circle",
                    opts.ToDictionary(e => e.Key, e => (object)e.Value));
                Dictionary<string, Circle> objs = jsObjectRefs.ToDictionary(e => e.Key, e => new Circle(e.Value));
                obj = new CircleList(jsObjectRef, objs);

            return obj;
        }

        /// <summary>
        /// Sync list over lifetime: Create and remove list depending on entity count; 
        /// entities will be removed, added or changed to mirror the given set.
        /// </summary>
        /// <param name="list">
        /// The list to manage. May be null.
        /// </param>
        /// <param name="jsRuntime"></param>
        /// <param name="opts"></param>
        /// <returns>
        /// The managed list. Assign to the variable you used as parameter.
        /// </returns>
        public static async Task<CircleList> SyncAsync(CircleList list,IJSRuntime jsRuntime, Dictionary<string, CircleOptions> opts,Action<MouseEvent,string,Circle> clickCallback=null)
        {
          if (opts.Count==0) {
            if (list!=null) {
              await list.SetMultipleAsync(opts);
              list=null;
            }
          } else {
            if (list==null) {
              list = await CircleList.CreateAsync(jsRuntime,new Dictionary<string, CircleOptions>());
              if (clickCallback!=null) {
                list.EntityClicked+=(sender,e)=>{
                  clickCallback(e.MouseEvent,e.Key,e.Entity);
                };
              }
            }
              await list.SetMultipleAsync(opts);
          }
          return list;
        }

        private CircleList(JsObjectRef jsObjectRef, Dictionary<string, Circle> circles)
            : base(jsObjectRef, circles)
        {
        }

        /// <summary>
        /// Set the set of entities; entities will be removed, added or changed to mirror the given set.
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        public async Task SetMultipleAsync(Dictionary<string, CircleOptions> opts)
        {
          await base.SetMultipleAsync(opts, "google.maps.Circle");
        }

        /// <summary>
        /// Only keys not matching with existent Circle keys will be created
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        public async Task AddMultipleAsync(Dictionary<string, CircleOptions> opts)
        {
            await base.AddMultipleAsync(opts, "google.maps.Circle");
        }

        public Task<Dictionary<string, LatLngBoundsLiteral>> GetBounds(List<string> filterKeys = null)
        {
            List<string> matchingKeys = ComputeMathingKeys(filterKeys);

            if (matchingKeys.Any())
            {
                Dictionary<Guid, string> internalMapping = ComputeInternalMapping(matchingKeys);
                Dictionary<Guid, object> dictArgs = ComputeDictArgs(matchingKeys);

                return _jsObjectRef.InvokeMultipleAsync<LatLngBoundsLiteral>(
                    "getBounds",
                    dictArgs).ContinueWith(e => e.Result.ToDictionary(r => internalMapping[new Guid(r.Key)], r => r.Value));
            }
            else
            {
                return ComputeEmptyResult<LatLngBoundsLiteral>();
            }
        }

        public Task<Dictionary<string, LatLngLiteral>> GetCenters(List<string> filterKeys = null)
        {
            List<string> matchingKeys = ComputeMathingKeys(filterKeys);

            if (matchingKeys.Any())
            {
                Dictionary<Guid, string> internalMapping = ComputeInternalMapping(matchingKeys);
                Dictionary<Guid, object> dictArgs = ComputeDictArgs(matchingKeys);

                return _jsObjectRef.InvokeMultipleAsync<LatLngLiteral>(
                    "getCenter",
                    dictArgs).ContinueWith(e => e.Result.ToDictionary(r => internalMapping[new Guid(r.Key)], r => r.Value));
            }
            else
            {
                return ComputeEmptyResult<LatLngLiteral>();
            }
        }

        public Task<Dictionary<string, bool>> GetEditables(List<string> filterKeys = null)
        {
            List<string> matchingKeys = ComputeMathingKeys(filterKeys);

            if (matchingKeys.Any())
            {
                Dictionary<Guid, string> internalMapping = ComputeInternalMapping(matchingKeys);
                Dictionary<Guid, object> dictArgs = ComputeDictArgs(matchingKeys);

                return _jsObjectRef.InvokeMultipleAsync<bool>(
                    "getEditable",
                    dictArgs).ContinueWith(e => e.Result.ToDictionary(r => internalMapping[new Guid(r.Key)], r => r.Value));
            }
            else
            {
                return ComputeEmptyResult<bool>();
            }
        }

        public Task<Dictionary<string, double>> GetRadiuses(List<string> filterKeys = null)
        {
            List<string> matchingKeys = ComputeMathingKeys(filterKeys);

            if (matchingKeys.Any())
            {
                Dictionary<Guid, string> internalMapping = ComputeInternalMapping(matchingKeys);
                Dictionary<Guid, object> dictArgs = ComputeDictArgs(matchingKeys);

                return _jsObjectRef.InvokeMultipleAsync<double>(
                    "getRadius",
                    dictArgs).ContinueWith(e => e.Result.ToDictionary(r => internalMapping[new Guid(r.Key)], r => r.Value));
            }
            else
            {
                return ComputeEmptyResult<double>();
            }
        }

        public Task SetCenters(Dictionary<string, LatLngLiteral> centers)
        {
            Dictionary<Guid, object> dictArgs = centers.ToDictionary(e => Circles[e.Key].Guid, e => (object)e.Value);
            return _jsObjectRef.InvokeMultipleAsync(
                "setCenter",
                dictArgs);
        }

        public Task SetEditables(Dictionary<string, bool> editables)
        {
            Dictionary<Guid, object> dictArgs = editables.ToDictionary(e => Circles[e.Key].Guid, e => (object)e.Value);
            return _jsObjectRef.InvokeMultipleAsync(
                "setEditable",
                dictArgs);
        }

        public Task SetRadiuses(Dictionary<string, double> radiuses)
        {
            Dictionary<Guid, object> dictArgs = radiuses.ToDictionary(e => Circles[e.Key].Guid, e => (object)e.Value);
            return _jsObjectRef.InvokeMultipleAsync(
                "setRadius",
                dictArgs);
        }
    }
}
