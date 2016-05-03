using System;
using System.Collections.Generic;

namespace AutoServiceManager.Mediator
{
    static public class Mediator
    {
        static readonly IDictionary<string, List<Action<object>>> CallDictionary = new Dictionary<string, List<Action<object>>>();

        static public void Register(string token, Action<object> callback)
        {
            if (!CallDictionary.ContainsKey(token))
            {
                var list = new List<Action<object>> {callback};
                CallDictionary.Add(token, list);
            }
            else
            {
                var found = false;
                foreach (var item in CallDictionary[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    CallDictionary[token].Add(callback);
            }
        }

        static public void Unregister(string token, Action<object> callback)
        {
            if (CallDictionary.ContainsKey(token))
                CallDictionary[token].Remove(callback);
        }

        static public void NotifyColleagues(string token, object args)
        {
            if (CallDictionary.ContainsKey(token))
                foreach (var callback in CallDictionary[token])
                    callback(args);
        }
    }

}
