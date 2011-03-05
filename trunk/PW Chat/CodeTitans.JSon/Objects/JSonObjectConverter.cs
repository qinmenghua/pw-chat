﻿#region License
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
using System.Reflection;

namespace CodeTitans.JSon.Objects
{
    /// <summary>
    /// Class that converts IJSonObject to given type.
    /// </summary>
    internal static class JSonObjectConverter
    {
        /// <summary>
        /// Gets the object value from given <see cref="IJSonObject"/>.
        /// It is capable of converting most of the FCL types and also all custom classes/structures marked with JSonSerializable attribute.
        /// It can also detect if a collection of objects should be returned.
        /// </summary>
        public static object ToObject(IJSonObject source, Type oType)
        {
            if (oType == typeof(Single))
                return source.SingleValue;
            if (oType == typeof(Double))
                return source.DoubleValue;
            if (oType == typeof(DateTime))
                return source.DateTimeValue;
            if (oType == typeof(TimeSpan))
                return source.TimeSpanValue;
            if (oType == typeof(Boolean))
                return source.BooleanValue;
            if (source.IsNull || oType == typeof(DBNull))
                return null;
            if (oType == typeof(string))
                return source.StringValue;
            if (oType == typeof(char))
                return string.IsNullOrEmpty(source.StringValue) ? '\0' : source.StringValue[0];
            if (oType == typeof(Guid))
                return source.GuidValue;
            if (oType == typeof(Byte))
                return (Byte)source.UInt32Value;
            if (oType == typeof(SByte))
                return (SByte)source.Int32Value;
            if (oType == typeof(UInt16))
                return (UInt16)source.UInt32Value;
            if (oType == typeof(Int16))
                return (Int16)source.Int32Value;
            if (oType == typeof(Int32))
                return source.Int32Value;
            if (oType == typeof(UInt32))
                return source.UInt32Value;
            if (oType == typeof(Int64))
                return source.Int64Value;
            if (oType == typeof(object))
                return source.ObjectValue;
            if (oType == typeof(IJSonObject))
                return source;

            // if a collection should be parsed:
            if (oType.IsGenericType && (oType.Namespace.StartsWith("System.Collections", StringComparison.OrdinalIgnoreCase) || oType.IsArray))
            {
                if (!source.IsEnumerable || source.Names != null)
                    throw new JSonException("Expected source is not a collection.");

                Type resultType = oType.GetGenericArguments()[0];
                Array result = Array.CreateInstance(resultType, source.Count);
                int i = 0;

                foreach (IJSonObject data in source)
                {
                    result.SetValue(ToObject(data, Activator.CreateInstance(resultType), resultType), i++);
                }

                return result;
            }

            return ToObject(source, Activator.CreateInstance(oType), oType);
        }

        /// <summary>
        /// Gets the object value from given IJSonObject.
        /// It is capable of converting most of the FCL types and also all custom classes/structures marked with JSonSerializable attribute.
        /// It can also detect if a collection of objects should be returned.
        /// </summary>
        public static T ToObject<T>(IJSonObject source) where T : new()
        {
            return ToObject(source, new T(), typeof(T));
        }

        private static T ToObject<T>(IJSonObject source, T destination, Type oType)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");

            if (!source.IsEnumerable || source.Names == null)
                throw new ArgumentException("Source is not a JSON Object");

            JSonSerializableAttribute jsonAttribute = (JSonSerializableAttribute)Attribute.GetCustomAttribute(oType, typeof(JSonSerializableAttribute));

            if (jsonAttribute == null)
                throw new JSonException("Object doesn't marked with JSonSerializable attribute");

            // get members that will be serialized:
            FieldInfo[] fieldMembers = oType.GetFields(jsonAttribute.Flags);
            PropertyInfo[] propertyMembers = oType.GetProperties(jsonAttribute.Flags);

            // deserialize all fields:
            foreach (FieldInfo fieldInfo in fieldMembers)
            {
                ToObjectSetField<T>(source, destination, oType, jsonAttribute, fieldInfo);
            }

            // deserialize all properties:
            foreach (PropertyInfo propertyInfo in propertyMembers)
            {
                ToObjectSetProperty<T>(source, destination, oType, jsonAttribute, propertyInfo);
            }

            return destination;
        }

        private static void ToObjectSetField<T>(IJSonObject source, T destination, Type oType, JSonSerializableAttribute jsonAttribute, FieldInfo fieldInfo)
        {
            if (fieldInfo.IsLiteral)
                return;

            JSonIgnoreAttribute ignore = (JSonIgnoreAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(JSonIgnoreAttribute));

            if (ignore != null)
                return;

            JSonMemberAttribute attr = (JSonMemberAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(JSonMemberAttribute));

            if (attr == null && !jsonAttribute.AllowAllFields)
                return;

            string name = attr != null && !string.IsNullOrEmpty(attr.Name) ? attr.Name : fieldInfo.Name;
            Type type = attr != null && attr.ReadAs != null ? attr.ReadAs : fieldInfo.FieldType;

            if (source.Contains(name))
                fieldInfo.SetValue(destination, ToObject(source[name], type));
            else
                if (attr != null)
                {
                    if (attr.DefaultValue != null)
                        fieldInfo.SetValue(destination, ToObject(attr.DefaultValue, type));
                    else
                        if (!attr.SupressThrowWhenMissing && !attr.SkipWhenNull)
                            throw new JSonMemberMissingException("Missing required field", oType, name);
                }
                else
                    if (!jsonAttribute.SuppressThrowWhenMissing)
                        throw new JSonMemberMissingException("Field value not provided", oType, name);
        }


        private static void ToObjectSetProperty<T>(IJSonObject source, T destination, Type oType, JSonSerializableAttribute jsonAttribute, PropertyInfo propertyInfo)
        {
            // there must be a getter defined:
            if (!propertyInfo.CanWrite)
                return;

            JSonIgnoreAttribute ignore = (JSonIgnoreAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(JSonIgnoreAttribute));

            if (ignore != null)
                return;

            JSonMemberAttribute attr = (JSonMemberAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(JSonMemberAttribute));
            if (attr == null && !jsonAttribute.AllowAllProperties)
                return;

            string name = attr != null && !string.IsNullOrEmpty(attr.Name) ? attr.Name : propertyInfo.Name;
            Type type = attr != null && attr.ReadAs != null ? attr.ReadAs : propertyInfo.PropertyType;

            if (source.Contains(name))
                propertyInfo.SetValue(destination, ToObject(source[name], type), null);
            else
                if (attr != null)
                {
                    if (attr.DefaultValue != null)
                        propertyInfo.SetValue(destination, ToObject(attr.DefaultValue, type), null);
                    else
                        if (!attr.SupressThrowWhenMissing && !attr.SkipWhenNull)
                            throw new JSonMemberMissingException("Missing required property", oType, name);
                }
                else
                    if (!jsonAttribute.SuppressThrowWhenMissing)
                        throw new JSonMemberMissingException("Property value not provided", oType, name);
        }
    }
}
