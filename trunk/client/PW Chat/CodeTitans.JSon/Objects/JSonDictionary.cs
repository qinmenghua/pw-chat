#region License
/*
    Copyright (c) 2010, Paweł Hofman (CodeTitans)
    All Rights Reserved.

    Licensed under the Apache License version 2.0.
    For more information please visit:

    http://codetitans.codeplex.com/license
        or
    http://www.apache.org/licenses/


    For latest source code, documentation, samples
    and more information please visit:

    http://codetitans.codeplex.com/
*/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeTitans.JSon.Objects
{
    /// <summary>
    /// Internal wrapper class that describes JSON object and provides <see cref="IJSonObject"/> access interface.
    /// </summary>
    internal sealed class JSonDictionary : IJSonObject, IJSonWritable
    {
        private readonly Dictionary<string, IJSonObject> _data;

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonDictionary(Dictionary<string, object> data)
        {
            _data = new Dictionary<string, IJSonObject>();

            foreach (KeyValuePair<string, object> d in data)
                _data.Add(d.Key, (IJSonObject)d.Value);
        }

        #region IJSonObject Members

        public string StringValue
        {
            get { throw new InvalidOperationException(); }
        }

        public int Int32Value
        {
            get { throw new InvalidOperationException(); }
        }

        public uint UInt32Value
        {
            get { throw new InvalidOperationException(); }
        }

        public long Int64Value
        {
            get { throw new InvalidOperationException(); }
        }

        public ulong UInt64Value
        {
            get { throw new InvalidOperationException(); }
        }

        public float SingleValue
        {
            get { throw new InvalidOperationException(); }
        }

        public double DoubleValue
        {
            get { throw new InvalidOperationException(); }
        }

        public DateTime DateTimeValue
        {
            get { throw new InvalidOperationException(); }
        }

        public TimeSpan TimeSpanValue
        {
            get { throw new InvalidOperationException(); }
        }

        public bool BooleanValue
        {
            get { throw new InvalidOperationException(); }
        }

        public Guid GuidValue
        {
            get { throw new InvalidOperationException(); }
        }

        public bool IsNull
        {
            get { return false; }
        }

        public bool IsTrue
        {
            get { return false; }
        }

        public bool IsFalse
        {
            get { return true; }
        }

        public bool IsEnumerable
        {
            get { return true; }
        }

        public object ObjectValue
        {
            get
            {
                Dictionary<string, object> result = new Dictionary<string, object>();

                foreach (KeyValuePair<string, IJSonObject> v in _data)
                {
                    result.Add(v.Key, v.Value.ObjectValue);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the value of given JSON object.
        /// </summary>
        public object ToValue(Type t)
        {
            return JSonObjectConverter.ToObject(this, t);
        }

        /// <summary>
        /// Get the value of given JSON object.
        /// </summary>
        public T ToObjectValue<T>()
        {
            return (T)JSonObjectConverter.ToObject(this, typeof(T));
        }

        public int Length
        {
            get { return _data.Count; }
        }

        public IJSonObject this[int index]
        {
            get { throw new InvalidOperationException(); }
        }

        public int Count
        {
            get { return _data.Count; }
        }

        public IJSonObject this[string name]
        {
            get { return _data[name]; }
        }

        public IJSonObject this[String name, IJSonObject defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                if (defaultValue != null)
                    return defaultValue;

                return new JSonStringObject(null);
            }
        }

        public IJSonObject this[String name, String defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonStringObject(defaultValue);
            }
        }

        public IJSonObject this[String name, String defaultValue, Boolean asJSonSerializedObject]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                if (!asJSonSerializedObject)
                    return new JSonStringObject(defaultValue);

                JSonReader reader = new JSonReader();
                return reader.ReadAsJSonObject(defaultValue);
            }
        }

        public IJSonObject this[String name, Int32 defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonDecimalObject<Int32>(defaultValue);
            }
        }

        public IJSonObject this[String name, Int64 defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonDecimalObject<Int64>(defaultValue);
            }
        }

        public IJSonObject this[String name, Single defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonDecimalObject<Single>(defaultValue);
            }
        }

        public IJSonObject this[String name, Double defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonDecimalObject<Double>(defaultValue);
            }
        }

        public IJSonObject this[String name, DateTime defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonDecimalObject<Int64>(defaultValue.ToUniversalTime().Ticks, JSonWriter.ToString(defaultValue));
            }
        }

        public IJSonObject this[String name, TimeSpan defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonDecimalObject<Int64>(defaultValue.Ticks, JSonWriter.ToString(defaultValue));
            }
        }

        public IJSonObject this[String name, Guid defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonStringObject(JSonWriter.ToString(defaultValue));
            }
        }

        public IJSonObject this[String name, Boolean defaultValue]
        {
            get
            {
                IJSonObject result;

                if (_data.TryGetValue(name, out result))
                    return result;

                return new JSonBooleanObject(defaultValue);
            }
        }

        public bool Contains(string name)
        {
            return _data.ContainsKey(name);
        }

        public ICollection<string> Names
        {
            get { return _data.Keys; }
        }

        public IEnumerable<KeyValuePair<string, IJSonObject>> ObjectItems
        {
            get { return _data; }
        }

        public IEnumerable<IJSonObject> ArrayItems
        {
            get { return _data.Values; }
        }

        #endregion

        #region IEnumerable<IJSonObject> Members

        public IEnumerator<IJSonObject> GetEnumerator()
        {
            return _data.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.Values.GetEnumerator();
        }

        #endregion

        public override string ToString()
        {
            IJSonWriter writer = new JSonWriter(true);

            Write(writer);
            return writer.ToString();
        }

        #region IJSonWritable Members

        public void Write(IJSonWriter output)
        {
            output.Write(_data);
        }

        #endregion
    }
}
