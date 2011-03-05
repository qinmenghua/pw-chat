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
using System.Globalization;

namespace CodeTitans.JSon.Objects
{
    /// <summary>
    /// Internal wrapper class that describes string and provides <see cref="IJSonObject"/> access interface.
    /// </summary>
    internal sealed class JSonStringObject : IJSonObject, IJSonWritable
    {
        private readonly string _data;

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonStringObject(string data)
        {
            _data = data;
        }

        #region IJSonObject Members

        public string StringValue
        {
            get { return _data; }
        }

        public int Int32Value
        {
            get { return Int32.Parse(_data, NumberStyles.Integer, CultureInfo.InvariantCulture); }
        }

        public uint UInt32Value
        {
            get { return UInt32.Parse(_data, NumberStyles.Integer, CultureInfo.InvariantCulture); }
        }

        public long Int64Value
        {
            get { return Int64.Parse(_data, NumberStyles.Integer, CultureInfo.InvariantCulture); }
        }

        public ulong UInt64Value
        {
            get { return UInt64.Parse(_data, NumberStyles.Integer, CultureInfo.InvariantCulture); }
        }

        public float SingleValue
        {
            get { return Single.Parse(_data, NumberStyles.Float, CultureInfo.InvariantCulture); }
        }

        public double DoubleValue
        {
            get { return Double.Parse(_data, NumberStyles.Float, CultureInfo.InvariantCulture); }
        }

        public DateTime DateTimeValue
        {
            get { return DateTime.Parse(_data, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal); }
        }

        public TimeSpan TimeSpanValue
        {
            get { return TimeSpan.Parse(_data); }
        }

        public bool BooleanValue
        {
            get { return Boolean.Parse(_data); }
        }

        public Guid GuidValue
        {
            get { return new Guid(_data); }
        }

        public bool IsNull
        {
            get { return _data == null; }
        }

        public bool IsTrue
        {
            get { return BooleanValue; }
        }

        public bool IsFalse
        {
            get { return !BooleanValue; }
        }

        public bool IsEnumerable
        {
            get { return false; }
        }

        public object ObjectValue
        {
            get { return _data; }
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
            if (typeof(T) == typeof(IJSonObject))
            {
                JSonReader reader = new JSonReader();
                return (T)reader.ReadAsJSonObject(StringValue);
            }

            return (T)JSonObjectConverter.ToObject(this, typeof(T));
        }

        public int Length
        {
            get { return _data.Length; }
        }

        public IJSonObject this[int index]
        {
            get { throw new InvalidOperationException(); }
        }

        public int Count
        {
            get { return _data.Length; }
        }

        public IJSonObject this[string name]
        {
            get { throw new InvalidOperationException(); }
        }


        public IJSonObject this[String name, IJSonObject defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, String defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, String defaultValue, Boolean asJSonSerializedObject]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, Int32 defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, Int64 defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, Single defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, Double defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, DateTime defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, TimeSpan defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, Guid defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, Boolean defaultValue]
        {
            get { throw new InvalidOperationException(); }
        }

        public bool Contains(string name)
        {
            return false;
        }

        public ICollection<string> Names
        {
            get { return null; }
        }

        #endregion

        #region IEnumerable<IJSonObject> Members

        public IEnumerator<IJSonObject> GetEnumerator()
        {
            throw new InvalidOperationException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new InvalidOperationException();
        }

        public IEnumerable<KeyValuePair<string, IJSonObject>> ObjectItems
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IJSonObject> ArrayItems
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public override string ToString()
        {
            return _data;
        }

        #region IJSonWritable Members

        public void Write(IJSonWriter output)
        {
            output.WriteValue(_data);
        }

        #endregion
    }
}
