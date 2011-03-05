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
using System.IO;
using System.Reflection;
using System.Text;

namespace CodeTitans.JSon
{
    /// <summary>
    /// Class that allows to construct the JSON objects and writes them into given <see cref="TextWriter"/> output.
    /// </summary>
    public sealed class JSonWriter : IDisposable, IJSonWriter
    {
        private readonly TextWriter _output;
        private readonly Stack<JSonWriterTokenInfo> _tokens;
        private Int32 _level;
        private readonly bool _closeOutput;

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonWriter(TextWriter output)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            _output = output;
            _tokens = new Stack<JSonWriterTokenInfo>();
            _level = 1;
        }

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonWriter(StringBuilder output)
            : this(output, false)
        {
        }

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonWriter(StringBuilder output, bool indent)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            _output = new StringWriter(output, CultureInfo.InvariantCulture);
            _tokens = new Stack<JSonWriterTokenInfo>();
            _level = 1;
            _closeOutput = true;
            Indent = indent;
        }

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonWriter(bool indent)
        {
            Indent = indent;
            _output = new StringWriter(CultureInfo.InvariantCulture);
            _tokens = new Stack<JSonWriterTokenInfo>();
            _level = 1;
            _closeOutput = true;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JSonWriter()
        {
            _output = new StringWriter(CultureInfo.InvariantCulture);
            _tokens = new Stack<JSonWriterTokenInfo>();
            _level = 1;
            _closeOutput = true;
        }

        /// <summary>
        /// Get the JSON string representation stored inside.
        /// Before accessing it, use <seealso cref="IsValid"/> property to validate
        /// if content has a JSON valid structure.
        /// </summary>
        /// <returns>String representing JSON object.</returns>
        public override string ToString()
        {
            if (_output != null)
                return _output.ToString();

            return null;
        }

        /// <summary>
        /// Gets or sets the indication if generated content should be indented.
        /// The setting take effect from the next writing, so the previously
        /// written elements are no affected.
        /// </summary>
        public bool Indent
        { get; set; }

        /// <summary>
        /// Checks if given instance of JSON writer contains a valid content.
        /// </summary>
        public bool IsValid
        {
            get { return _tokens.Count == 0 && _output != null; }
        }

        #region Verification Methods

        private JSonWriterTokenInfo VerifyTopTokenEqualToAndDequeue(JSonWriterTokenType expected)
        {
            JSonWriterTokenType currentToken = JSonWriterTokenType.Nothing;
            JSonWriterTokenInfo t = null;

            if (_tokens.Count > 0)
            {
                t = _tokens.Pop();
                currentToken = t.TokenType;
            }

            if (currentToken != expected)
                throw new JSonWriterException(expected, currentToken);

            return t;
        }

        private JSonWriterTokenInfo VerifyTopTokenEqualTo(JSonWriterTokenType expected)
        {
            if (_tokens.Count == 0)
                throw new JSonWriterException(expected, JSonWriterTokenType.Nothing);

            JSonWriterTokenType currentToken = _tokens.Peek().TokenType;

            if (currentToken != expected)
                throw new JSonWriterException(expected, currentToken);

            if (_tokens.Count > 0)
                return _tokens.Peek();

            return null;
        }

        private JSonWriterTokenInfo VerifyTopTokenEqualTo(JSonWriterTokenType expected1, JSonWriterTokenType expected2)
        {
            JSonWriterTokenType currentToken = JSonWriterTokenType.Nothing;

            if (_tokens.Count > 0)
                currentToken = _tokens.Peek().TokenType;

            if (currentToken != expected1 && currentToken != expected2)
            {
                if (currentToken != expected1)
                    throw new JSonWriterException(expected1, currentToken);
                throw new JSonWriterException(expected2, currentToken);
            }

            if (_tokens.Count > 0)
                return _tokens.Peek();

            return null;
        }

        private JSonWriterTokenInfo VerifyTopTokenDifferentThan(JSonWriterTokenType expected)
        {
            if (_tokens.Count > 0)
            {
                JSonWriterTokenType currentToken = _tokens.Peek().TokenType;

                if (currentToken == expected)
                    throw new JSonWriterException(expected, currentToken);

                return _tokens.Peek();
            }

            return null;
        }

        private static string GetSecureString(string value, bool quoted)
        {
            StringBuilder result = new StringBuilder();

            if (quoted)
                result.Append('"');

            foreach(char c in value)
            {
                if (c == '\\')
                    result.Append("\\\\");
                else
                    if (c == '"')
                        result.Append("\\\"");
                    else
                        if (c == '\b')
                            result.Append ("\\b");
                        else
                            if (c == '\t')
                                result.Append("\\t");
                            else
                                if (c == '\r')
                                    result.Append ("\\r");
                                else
                                    if (c == '\n')
                                        result.Append("\\n");
                                    else
                                        if (c == '\f')
                                            result.Append("\\f");
                                        else
                                        {
                                            result.Append(c);

                                            /*
                                            int ansiChar = (int)c;
                                            if (ansiChar >= 32 && ansiChar <= 128)
                                                result.Append(c);
                                            else
                                                result.Append("\\u").Append(Convert.ToString(ansiChar, 16).PadLeft(4, '0'));
                                             */
                                        }
            }

            if (quoted)
                result.Append('"');

            return result.ToString();
        }

        #endregion

        #region Object Serialization Methods

        /// <summary>
        /// Writes an object start.
        /// </summary>
        public void WriteObjectBegin()
        {
            // object can't be directly embedded inside another object:
            JSonWriterTokenInfo currentToken = VerifyTopTokenDifferentThan(JSonWriterTokenType.Object);
            _tokens.Push(new JSonWriterTokenInfo(JSonWriterTokenType.Object, _level++));

            if (currentToken != null)
                currentToken.AddItem(_output, currentToken.TokenType != JSonWriterTokenType.MemberValue ? Indent : false);

            _output.Write('{');
        }

        /// <summary>
        /// Writes an object end.
        /// </summary>
        public void WriteObjectEnd()
        {
            JSonWriterTokenInfo currentToken = VerifyTopTokenEqualToAndDequeue(JSonWriterTokenType.Object);

            // if this object was added as a value in
            if (_tokens.Count > 0 && _tokens.Peek().TokenType == JSonWriterTokenType.MemberValue)
            {
                _tokens.Pop();
            }

            if (currentToken != null)
                currentToken.RemoveItem(_output, Indent);

            _level--;
            _output.Write('}');
        }

        /// <summary>
        /// Writes an array start.
        /// </summary>
        public void WriteArrayBegin()
        {
            JSonWriterTokenInfo currentToken = _tokens.Count > 0 ? _tokens.Peek() : null;

            _tokens.Push(new JSonWriterTokenInfo(JSonWriterTokenType.Array, _level++));

            if (currentToken != null)
                currentToken.AddItem(_output, currentToken.TokenType != JSonWriterTokenType.MemberValue ? Indent : false);

            _output.Write('[');
        }

        /// <summary>
        /// Writes an array end.
        /// </summary>
        public void WriteArrayEnd()
        {
            JSonWriterTokenInfo currentToken = VerifyTopTokenEqualToAndDequeue(JSonWriterTokenType.Array);

            // if this array was added as a value in
            if (_tokens.Count > 0 && _tokens.Peek().TokenType == JSonWriterTokenType.MemberValue)
            {
                _tokens.Pop();
            }

            if (currentToken != null)
                currentToken.RemoveItem(_output, Indent);

            _output.Write(']');
        }

        private void WriteMemberInternal(string name, string value)
        {
            JSonWriterTokenInfo currentToken = VerifyTopTokenEqualTo(JSonWriterTokenType.Object);

            if (currentToken != null)
                currentToken.AddItem(_output, Indent);

            _output.Write("\"{0}\": {1}", GetSecureString(name, false), value);
        }

        /// <summary>
        /// Writes a 'null' text as a value.
        /// It requires to be put inside an object.
        /// </summary>
        public void WriteMember(string name, DBNull value)
        {
            WriteMemberInternal(name, JSonReader.NullString);
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// If 'null' value is passed it will emit the JSON 'null' for given member.
        /// </summary>
        public void WriteMember(string name, String value)
        {
            if (value == null)
                WriteMemberInternal(name, JSonReader.NullString);
            else
                WriteMemberInternal(name, GetSecureString(value, true));
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        public void WriteMember(string name, Int32 value)
        {
            WriteMemberInternal(name, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        public void WriteMember(string name, Int64 value)
        {
            WriteMemberInternal(name, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        public void WriteMember(string name, Single value)
        {
            WriteMemberInternal(name, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        public void WriteMember(string name, Double value)
        {
            WriteMemberInternal(name, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// Before writing, the date is converted into universal time representation and written as string.
        /// </summary>
        public void WriteMember(string name, DateTime value)
        {
            // write as universal (GMT/Zulu) time:
            WriteMemberInternal(name, GetSecureString(ToString(value), true));
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        public void WriteMember(string name, TimeSpan value)
        {
            WriteMemberInternal(name, GetSecureString(ToString(value), true));
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        public void WriteMember(string name, Boolean value)
        {
            WriteMemberInternal(name, ToString(value));
        }

        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        public void WriteMember(string name, Guid value)
        {
            WriteMemberInternal(name, GetSecureString(ToString(value), true));
        }

        /// <summary>
        /// Writes 'null' JSON value.
        /// </summary>
        public void WriteMemberNull(string name)
        {
            WriteMemberInternal(name, JSonReader.NullString);
        }

        /// <summary>
        /// Writes a member only to an object.
        /// It requires to be called before writing an object (or array) inside current object.
        /// Writing values after call to this function is also allowed, however it is not recommented
        /// due to number of checks performed. The better performance can be achieved
        /// if other overloaded functions are used.
        /// </summary>
        public void WriteMember(string name)
        {
            JSonWriterTokenInfo currentToken = VerifyTopTokenEqualTo(JSonWriterTokenType.Object);

            currentToken.AddItem(_output, Indent);

            _tokens.Push(new JSonWriterTokenInfo(JSonWriterTokenType.MemberValue, _level));
            _output.Write("\"{0}\": ", name);
        }

        private void WriteValueInternal(string value)
        {
            // allow adding value item, when it's just a simple writing without wrapping
            // in array or object:
            if (_tokens.Count == 0)
            {
                // push dummy item on tokens stack, so that on the next try, verification will fail,
                // only one value item can be added!
                _tokens.Push(new JSonWriterTokenInfo(JSonWriterTokenType.Nothing, -1));
                _output.Write(value);
                return;
            }

            JSonWriterTokenInfo currentToken = VerifyTopTokenEqualTo(JSonWriterTokenType.Array, JSonWriterTokenType.MemberValue);

            currentToken.AddItem(_output, currentToken.TokenType != JSonWriterTokenType.MemberValue ? Indent : false);

            if (currentToken.TokenType == JSonWriterTokenType.MemberValue)
            {
                _tokens.Pop();
            }

            _output.Write(value);
        }

        /// <summary>
        /// Writes a 'null' value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValue(DBNull value)
        {
            WriteValueInternal(JSonReader.NullString);
        }

        /// <summary>
        /// Writes string value.
        /// It can be used only as an array element or value for object member.
        /// If 'null' value is passed it will emit the JSON 'null' for given member.
        /// </summary>
        public void WriteValue(String value)
        {
            if (value == null)
                WriteValueInternal(JSonReader.NullString);
            else
                WriteValueInternal(GetSecureString(value, true));
        }

        /// <summary>
        /// Writes integer value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValue(Int32 value)
        {
            WriteValueInternal(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes integer value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValue(Int64 value)
        {
            WriteValueInternal(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes decimal value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValue(Single value)
        {
            WriteValueInternal(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes decimal value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValue(Double value)
        {
            WriteValueInternal(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes DateTime value.
        /// It can be used only as an array element or value for object member.
        /// Before writing, the date is converted into universal time representation and written as string.
        /// </summary>
        public void WriteValue(DateTime value)
        {
            WriteValueInternal(GetSecureString(ToString(value), true));
        }

        /// <summary>
        /// Writes TimeSpan value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValue(TimeSpan value)
        {
            WriteValueInternal(GetSecureString(ToString(value), true));
        }

        /// <summary>
        /// Writes Boolean value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValue(Boolean value)
        {
            WriteValueInternal(ToString(value));
        }

        /// <summary>
        /// Writes Guid value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValue(Guid value)
        {
            WriteValueInternal(GetSecureString(ToString(value), true));
        }

        /// <summary>
        /// Writes 'null' value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        public void WriteValueNull()
        {
            WriteValueInternal(JSonReader.NullString);
        }

        #endregion

        #region Data Serialization As String

        /// <summary>
        /// Converts DateTime to String.
        /// </summary>
        internal static String ToString(DateTime value)
        {
            return value.ToUniversalTime().ToString("u", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts TimeSpan to String.
        /// </summary>
        internal static String ToString(TimeSpan value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Converts Boolean to String.
        /// </summary>
        internal static String ToString(Boolean value)
        {
            return value ? JSonReader.TrueString : JSonReader.FalseString;
        }

        /// <summary>
        /// Converts Guid to String.
        /// </summary>
        internal static String ToString(Guid value)
        {
            return value.ToString("D");
        }

        #endregion

        #region Whole Object serialization

        private void WriteEmbeddedValue(object oValue)
        {
            // check if this is a struct:
            if (oValue is Single)
            {
                WriteValue((Single)oValue);
                return;
            }
            if (oValue is Double)
            {
                WriteValue((Double)oValue);
                return;
            }
            if (oValue is DateTime)
            {
                WriteValue((DateTime)oValue);
                return;
            }
            if (oValue is TimeSpan)
            {
                WriteValue((TimeSpan)oValue);
                return;
            }
            if (oValue is Boolean)
            {
                WriteValue((Boolean)oValue);
                return;
            }
            // check if this is a class object:
            if (oValue == null)
            {
                WriteValueNull();
                return;
            }
            if (oValue is DBNull)
            {
                WriteValueNull();
                return;
            }
            if (oValue is String || oValue is Char)
            {
                WriteValue(oValue.ToString());
                return;
            }
            if (oValue is Guid)
            {
                WriteValue((Guid)oValue);
                return;
            }
            // does it implement the dedicated serialization interface?
            IJSonWritable jValue = oValue as IJSonWritable;

            if (jValue != null)
            {
                jValue.Write(this);
                return;
            }

            // is it marked with serialization attribute?
            Type oType = oValue.GetType();
            JSonSerializableAttribute jsonAttribute = (JSonSerializableAttribute)Attribute.GetCustomAttribute(oType, typeof(JSonSerializableAttribute));

            if (jsonAttribute != null)
            {
                WriteAttributedObject(oValue, oType, jsonAttribute);
                return;
            }

            // is a dictionary?
            IDictionary dict = oValue as IDictionary;

            if (dict != null)
            {
                Write(dict);
                return;
            }

            // is an array of elements?
            IEnumerable array = oValue as IEnumerable;

            if (array != null)
                Write(array);
            else
                WriteValueInternal(oValue.ToString());
        }

        /// <summary>
        /// Writes enumerable collection as a JSON array.
        /// </summary>
        public void Write(IEnumerable array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            // already implements serialization interface?
            IJSonWritable jValue = array as IJSonWritable;

            if (jValue != null)
            {
                jValue.Write(this);
                return;
            }

            // is marked with serialization attribute?
            Type oType = array.GetType();
            JSonSerializableAttribute jsonAttribute = (JSonSerializableAttribute)Attribute.GetCustomAttribute(oType, typeof(JSonSerializableAttribute));

            if (jsonAttribute != null)
            {
                WriteAttributedObject(array, oType, jsonAttribute);
                return;
            }

            WriteArrayBegin();
            foreach (object value in array)
            {
                WriteEmbeddedValue(value);
            }
            WriteArrayEnd();
        }

        /// <summary>
        /// Writes a dictionary as a JSON object.
        /// </summary>
        public void Write(IDictionary dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            // already implements serialization interface?
            IJSonWritable jValue = dictionary as IJSonWritable;

            if (jValue != null)
            {
                jValue.Write(this);
                return;
            }

            // is marked with serialization attribute?
            Type oType = dictionary.GetType();
            JSonSerializableAttribute jsonAttribute = (JSonSerializableAttribute)Attribute.GetCustomAttribute(oType, typeof(JSonSerializableAttribute));

            if (jsonAttribute != null)
            {
                WriteAttributedObject(dictionary, oType, jsonAttribute);
                return;
            }

            WriteObjectBegin();
            foreach (DictionaryEntry entry in dictionary)
            {
                WriteMember(entry.Key.ToString());
                WriteEmbeddedValue(entry.Value);
            }
            WriteObjectEnd();
        }

        /// <summary>
        /// Writes a serializable object as JSON string.
        /// </summary>
        public void Write(IJSonWritable o)
        {
            if (o == null)
                throw new ArgumentNullException("o");

            o.Write(this);
        }

        private void WriteAttributedMember(string itemName, object value, JSonMemberAttribute attr)
        {
            if (attr == null || !attr.SkipWhenNull || (attr.SkipWhenNull && value != null))
            {
                WriteMember(attr == null || string.IsNullOrEmpty(attr.Name) ? itemName : attr.Name);
                WriteEmbeddedValue(value);
            }
        }

        private void WriteAttributedObject(object o, Type oType, JSonSerializableAttribute jsonAttribute)
        {
            if (o == null)
                throw new ArgumentNullException("o");
            if (oType == null)
                throw new ArgumentNullException("oType");
            if (jsonAttribute == null)
                throw new ArgumentNullException("jsonAttribute");

            // get members that will be serialized:
            FieldInfo[] fieldMembers = oType.GetFields(jsonAttribute.Flags);
            PropertyInfo[] propertyMembers = oType.GetProperties(jsonAttribute.Flags);

            WriteObjectBegin();

            // serialize all fields:
            foreach (FieldInfo f in fieldMembers)
            {
                if (!f.IsLiteral)
                {
                    JSonMemberAttribute attr = (JSonMemberAttribute)Attribute.GetCustomAttribute(f, typeof(JSonMemberAttribute));
                    if (attr != null || jsonAttribute.AllowAllFields)
                    {
                        WriteAttributedMember(f.Name, f.GetValue(o), attr);
                    }
                }
            }

            // serialize all properties:
            foreach (PropertyInfo p in propertyMembers)
            {
                // there must be a getter defined:
                if (p.CanRead)
                {
                    JSonMemberAttribute attr = (JSonMemberAttribute)Attribute.GetCustomAttribute(p, typeof(JSonMemberAttribute));
                    if (attr != null || jsonAttribute.AllowAllProperties)
                    {
                        WriteAttributedMember(p.Name, p.GetValue(o, null), attr);
                    }
                }
            }

            WriteObjectEnd();
        }

        /// <summary>
        /// Writes whole object represented as dictionary or enumerable collection.
        /// </summary>
        public void Write(object o)
        {
            // try to access object as IJSonWritable class:
            IJSonWritable jValue = o as IJSonWritable;

            if (jValue != null)
            {
                Write(jValue);
                return;
            }

            if (o != null)
            {
                Type oType = o.GetType();
                JSonSerializableAttribute jsonAttribute = (JSonSerializableAttribute)Attribute.GetCustomAttribute(oType, typeof(JSonSerializableAttribute));

                if (jsonAttribute != null)
                {
                    WriteAttributedObject(o, oType, jsonAttribute);
                    return;
                }
            }

            // try to access object as dictionary:
            IDictionary dict = o as IDictionary;

            if (dict != null)
            {
                Write(dict);
                return;
            }

            // try to access object as enumerable:
            IEnumerable array = o as IEnumerable;

            if (array != null)
            {
                Write(array);
                return;
            }

            throw new ArgumentException("o");
        }

        #endregion

        #region IDispose Pattern

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~JSonWriter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Closes the output and releases internal resources.
        /// </summary>
        public void Close()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_output != null && _closeOutput)
                    _output.Close();
            }
        }

        /// <summary>
        /// Releases managed and native resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        #endregion
    }
}
