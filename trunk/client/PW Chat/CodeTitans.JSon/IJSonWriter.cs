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

namespace CodeTitans.JSon
{
    /// <summary>
    /// Interface defining all types of writings that are possible to serialize object into a JSon string.
    /// </summary>
    public interface IJSonWriter
    {
        /// <summary>
        /// Checks if given instance of JSON writer contains a valid content.
        /// </summary>
        bool IsValid
        { get; }

        /// <summary>
        /// Writes an object start.
        /// </summary>
        void WriteObjectBegin();
        /// <summary>
        /// Writes an object end.
        /// </summary>
        void WriteObjectEnd();

        /// <summary>
        /// Writes an array start.
        /// </summary>
        void WriteArrayBegin();
        /// <summary>
        /// Writes an array end.
        /// </summary>
        void WriteArrayEnd();

        /// <summary>
        /// Writes a 'null' text as a value.
        /// It requires to be put inside an object.
        /// </summary>
        void WriteMember(string name, DBNull value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// If 'null' value is passed it will emit the JSON 'null' for given member.
        /// </summary>
        void WriteMember(string name, String value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        void WriteMember(string name, Int32 value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        void WriteMember(string name, Int64 value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        void WriteMember(string name, Single value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        void WriteMember(string name, Double value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// Before writing, the date is converted into universal time representation and written as string.
        /// </summary>
        void WriteMember(string name, DateTime value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        void WriteMember(string name, TimeSpan value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        void WriteMember(string name, Boolean value);
        /// <summary>
        /// Writes a member with a value.
        /// It requires to be put inside an object.
        /// </summary>
        void WriteMember(string name, Guid value);
        /// <summary>
        /// Writes 'null' JSON value.
        /// </summary>
        void WriteMemberNull(string name);
        /// <summary>
        /// Writes a member only to an object.
        /// It requires to be called before writing an object (or array) inside current object.
        /// Writing values after call to this function is also allowed, however it is not recommended
        /// due to number of checks performed. The better performance can be achieved
        /// if other overloaded functions are used.
        /// </summary>
        void WriteMember(string name);

        /// <summary>
        /// Writes a 'null' value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValue(DBNull value);
        /// <summary>
        /// Writes string value.
        /// It can be used only as an array element or value for object member.
        /// If 'null' value is passed it will emit the JSON 'null' for given member.
        /// </summary>
        void WriteValue(String value);
        /// <summary>
        /// Writes integer value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValue(Int32 value);
        /// <summary>
        /// Writes integer value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValue(Int64 value);
        /// <summary>
        /// Writes decimal value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValue(Single value);
        /// <summary>
        /// Writes decimal value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValue(Double value);
        /// <summary>
        /// Writes DateTime value.
        /// It can be used only as an array element or value for object member.
        /// Before writing, the date is converted into universal time representation and written as string.
        /// </summary>
        void WriteValue(DateTime value);
        /// <summary>
        /// Writes TimeSpan value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValue(TimeSpan value);
        /// <summary>
        /// Writes Boolean value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValue(Boolean value);
        /// <summary>
        /// Writes Guid value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValue(Guid value);
        /// <summary>
        /// Writes 'null' value.
        /// It can be used only as an array element or value for object member.
        /// </summary>
        void WriteValueNull();

        /// <summary>
        /// Writes enumerable collection as a JSON array.
        /// </summary>
        void Write(IEnumerable array);
        /// <summary>
        /// Writes a dictionary as a JSON object.
        /// </summary>
        void Write(IDictionary dictionary);
        /// <summary>
        /// Writes a serializable object as JSON string.
        /// </summary>
        void Write(IJSonWritable o);
        /// <summary>
        /// Writes whole object represented as dictionary or enumerable collection.
        /// </summary>
        void Write(Object o);

        /// <summary>
        /// Closes the output and releases internal resources.
        /// </summary>
        void Close();
    }
}
