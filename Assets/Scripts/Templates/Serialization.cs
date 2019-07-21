using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Serialize {
    [Serializable]
    public class TableBase<TKey, TValue, Type> where Type : KeyAndValue<TKey, TValue> {
        [SerializeField]
        private List<Type> list;
        private Dictionary<TKey, TValue> table;
        public Dictionary<TKey, TValue> GetTable() {
            if(table == null) {
                table = ListToDictionary(list);
            }
            return table;
        }
        public List<Type> GetList() {
            if(list == null) {
                list = new List<Type>();
            }
            return list;
        }
        static Dictionary<TKey, TValue> ListToDictionary(List<Type> list) {
            var dic = new Dictionary<TKey, TValue>();
            foreach(KeyAndValue<TKey, TValue> pair in list) {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }
    }
    [Serializable]
    public class KeyAndValue<TKey, TValue> {
        public TKey Key;
        public TValue Value;
        public KeyAndValue(TKey key, TValue value) {
            Key = key;
            Value = value;
        }
        public KeyAndValue(KeyValuePair<TKey, TValue> pair) {
            Key = pair.Key;
            Value = pair.Value;
        }
    }

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
}