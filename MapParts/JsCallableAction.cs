﻿using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace GoogleMapsComponents
{
    public class JsCallableAction
    {
        private readonly Delegate _delegate;
        private readonly Type[] _argumentTypes;
        private readonly IJSRuntime _jsRuntime;

        public JsCallableAction(IJSRuntime jsRuntime, Delegate @delegate, params Type[] argumentTypes)
        {
            _jsRuntime = jsRuntime;
            _delegate = @delegate;
            _argumentTypes = argumentTypes;
        }

        [JSInvokable]
        public void Invoke(string args, string guid)
        {
            if (string.IsNullOrWhiteSpace(args) || !_argumentTypes.Any())
            {
                _delegate.DynamicInvoke();
                return;
            }

            var jArray = JArray.Parse(args);
            var arguments = _argumentTypes.Zip(jArray, (type, jToken) => new { jToken, type })
                .Select(x =>
                {
                    var obj = x.jToken.ToObject(x.type);

                    if (obj is IActionArgument actionArg)
                        actionArg.JsObjectRef = new JsObjectRef(_jsRuntime, new Guid(guid));

                    return obj;
                })
                .ToArray();

            //Debug.WriteLine(arguments.FirstOrDefault()?.GetType());

            _delegate.DynamicInvoke(arguments);
        }
    }
}
