using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Serialization<T> {
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }
    public Serialization(List<T> t) {
        this.target = t;
    }
}

[Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver {
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;
    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }
    public Serialization(Dictionary<TKey, TValue> t) {
        this.target = t;
    }
    public void OnBeforeSerialize() {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }
    public void OnAfterDeserialize() {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for(var i = 0; i < count; i++) {
            var key = keys[i];
            var value = values[i];
            target.Add(key, value);
        }
    }
}