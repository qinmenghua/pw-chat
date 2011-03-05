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
    /// Internal wrapper class that describes numeric type and provides <see cref="IJSonObject"/> access interface.
    /// </summary>
    internal class JSonDecimalObject<S> : IJSonObject, IJSonWritable where S : struct, IConvertible
    {
        private readonly S _data;
        private readonly string _stringRepresentation;

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonDecimalObject(S data)
        {
            _data = data;
        }

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonDecimalObject(S data, string stringRepresentation)
        {
            _data = data;
            _stringRepresentation = stringRepresentation;
        }

        #region IJSonObject Members

        public string StringValue
        {
            get
            {
                if (_stringRepresentation != null)
                    return _stringRepresentation;

                return _data.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int Int32Value
        {
            get { return _data.ToInt32(CultureInfo.InvariantCulture); }
        }

        public uint UInt32Value
        {
            get { return _data.ToUInt32(CultureInfo.InvariantCulture); }
        }

        public long Int64Value
        {
            get { return _data.ToInt64(CultureInfo.InvariantCulture); }
        }

        public ulong UInt64Value
        {
            get { return _data.ToUInt64(CultureInfo.InvariantCulture); }
        }

        public float SingleValue
        {
            get { return _data.ToSingle(CultureInfo.InvariantCulture); }
        }

        public double DoubleValue
        {
            get { return _data.ToDouble(CultureInfo.InvariantCulture); }
        }

        public DateTime DateTimeValue
        {
            get { return new DateTime(Int64Value, DateTimeKind.Utc); }
        }

        public TimeSpan TimeSpanValue
        {
            get { return new TimeSpan(Int64Value); }
        }

        public bool BooleanValue
        {
            get { return _data.ToBoolean(null); }
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

        public virtual object ObjectValue
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
            return (T)JSonObjectConverter.ToObject(this, typeof(T));
        }

        public int Length
        {
            get { throw new InvalidOperationException(); }
        }

        public IJSonObject this[int index]
        {
            get { throw new InvalidOperationException(); }
        }

        public int Count
        {
            get { throw new InvalidOperationException(); }
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
            if (_stringRepresentation != null)
                return _stringRepresentation;

            return _data.ToString(CultureInfo.InvariantCulture);
        }

        #region IJSonWritable Members

        public virtual void Write(IJSonWriter output)
        {
            if (_stringRepresentation != null)
                output.WriteValue(_stringRepresentation);

            Type t = _data.GetType();

            if (t == typeof(double))
            {
                output.WriteValue(DoubleValue);
            }
            else
            {
                output.WriteValue(Int64Value);
            }
        }

        #endregion
    }
}
