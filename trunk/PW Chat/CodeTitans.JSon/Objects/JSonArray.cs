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
    /// Internal wrapper class that describes array of IJSonObjects and provides <see cref="IJSonObject"/> access interface.
    /// </summary>
    internal sealed class JSonArray : IJSonObject, IJSonWritable
    {
        private readonly IJSonObject[] _data;

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonArray(List<object> data)
        {
            _data = new IJSonObject[data.Count];

            // convert and copy data as an array:
            int i = 0;
            foreach (object d in data)
                _data[i++] = (IJSonObject)d;
        }

        #region IEnumerable<IJSonObject> Members

        public IEnumerator<IJSonObject> GetEnumerator()
        {
            return ((IEnumerable<IJSonObject>)_data).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        #endregion

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
                object[] result = new object[_data.Length];

                for (int i = 0; i < _data.Length; i++)
                    result[i] = _data[i].ObjectValue;

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
            get { return _data.Length; }
        }

        public IJSonObject this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
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

        public IJSonObject this[String name, String defaultValue, Boolean asJSonSerializedObject]
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[String name, String defaultValue]
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

        public IEnumerable<KeyValuePair<string, IJSonObject>> ObjectItems
        {
            get { return null; }
        }

        public IEnumerable<IJSonObject> ArrayItems
        {
            get { return _data; }
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
