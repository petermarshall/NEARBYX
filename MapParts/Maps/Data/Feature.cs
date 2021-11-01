﻿using OneOf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMapsComponents.Maps.Data
{
    /// <summary>
    /// A feature has a geometry, an id, and a set of properties
    /// </summary>
    public class Feature : IEnumerable<string>
    {
        /// <summary>
        /// Constructs a Feature with the given options.
        /// </summary>
        /// <param name="options"></param>
        public Feature(FeatureOptions options = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, object>> Properties { get; private set; }

        /// <summary>
        /// Repeatedly invokes the given function, passing a property value and name on each invocation. 
        /// The order of iteration through the properties is undefined.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the feature's geometry.
        /// </summary>
        /// <returns></returns>
        public Geometry GetGeometry()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the feature ID.
        /// </summary>
        /// <returns></returns>
        public OneOf<int, string> GetId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the value of the requested property, or undefined if the property does not exist.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetProperty(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the property with the given name.
        /// </summary>
        /// <param name="name"></param>
        public void RemoveProperty(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the feature's geometry.
        /// </summary>
        /// <param name="newGeometry"></param>
        public void SetGeometry(OneOf<Geometry, LatLngLiteral> newGeometry)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the value of the specified property. 
        /// If newValue is undefined this is equivalent to calling removeProperty.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newValue"></param>
        public void SetProperty(string name, object newValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exports the feature to a GeoJSON object.
        /// </summary>
        /// <returns></returns>
        public Task<object> ToGeoJson()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
