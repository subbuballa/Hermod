﻿/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Hermod <http://www.github.com/Vanaheimr/Hermod>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

#endregion

namespace de.ahzf.Vanaheimr.Hermod.HTTP
{

    /// <summary>
    /// An abstract HTTP protocol data unit builder.
    /// A HTTP pdu has three parts:
    ///  - First a request/response specific first line
    ///  - A collection of key-value pairs of type &lt;string,object&gt;
    ///    for any kind of metadata
    ///  - A body hosting the transmitted content
    /// </summary>
    public abstract class AHTTPPDUBuilder : AHTTPBasePDU, IEnumerable<KeyValuePair<String, Object>>, INotifyPropertyChanged
    {

        #region Properties

        #region Non-HTTP header fields

        #region ConstructedHTTPHeader

        /// <summary>
        /// Return a string representation of this HTTPHeader.
        /// </summary>
        public String ConstructedHTTPHeader
        {
            get
            {

                if (HeaderFields.Count > 0)
                    return (from   _KeyValuePair in HeaderFields
                            where  _KeyValuePair.Key   != null
                            where  _KeyValuePair.Value != null
                            select _KeyValuePair.Key + ": " + _KeyValuePair.Value).
                            Aggregate((a, b) => a + Environment.NewLine + b);

                return null;

            }
        }

        #endregion

        #region HTTPStatusCode

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        //public HTTPStatusCode HTTPStatusCode
        //{

        //    get
        //    {
        //        return _HTTPStatusCode;
        //    }

        //    set
        //    {
        //        SetProperty(ref _HTTPStatusCode, value, "HTTPStatusCode");
        //        _HTTPStatusCode = value;
        //    }

        //}

        #endregion

        #region ProtocolName

        private String _ProtocolName;

        /// <summary>
        /// The HTTP protocol name field.
        /// </summary>
        public String ProtocolName
        {

            get
            {
                return _ProtocolName;
            }

            set
            {
                SetProperty(ref _ProtocolName, value, "ProtocolName");
            }

        }

        #endregion

        #region ProtocolVersion

        private HTTPVersion _ProtocolVersion;

        /// <summary>
        /// The HTTP protocol version.
        /// </summary>
        public HTTPVersion ProtocolVersion
        {

            get
            {
                return _ProtocolVersion;
            }

            set
            {
                SetProperty(ref _ProtocolVersion, value, "ProtocolVersion");
            }

        }

        #endregion

        #region Content

        private Byte[] _Content;

        /// <summary>
        /// The HTTP body/content as an array of bytes.
        /// </summary>
        public Byte[] Content
        {

            get
            {
                return _Content;
            }

            set
            {
                SetProperty(ref _Content, value, "Content");
                ContentLength = (UInt64) _Content.LongLength;
            }

        }

        #endregion

        #region ContentStream

        private Stream _ContentStream;

        /// <summary>
        /// The HTTP body/content as a stream.
        /// </summary>
        public Stream ContentStream
        {

            get
            {
                return _ContentStream;
            }

            set
            {
                SetProperty(ref _ContentStream, value, "ContentStream");
                // Setting the Content-Length might break lazyness!
                //ContentLength = (UInt64) _ContentStream.Length;
            }

        }

        #endregion

        #endregion

        #region General header fields

        #region Cache-Control

        public String CacheControl
        {

            get
            {
                return GetHeaderField(HTTPHeaderField.CacheControl);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.CacheControl, value);
            }

        }

        #endregion

        #region Connection

        public String Connection
        {

            get
            {
                return GetHeaderField(HTTPHeaderField.Connection);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.Connection, value);
            }

        }

        #endregion

        #region Content-Encoding

        public Encoding ContentEncoding
        {

            get
            {
                return GetHeaderField<Encoding>("Content-Encoding");
            }

            set
            {
                SetHeaderField(HTTPHeaderField.ContentEncoding, value);
            }

        }

        #endregion

        #region Content-Language

        public List<String> ContentLanguage
        {

            get
            {
                return GetHeaderField<List<String>>(HTTPHeaderField.ContentLanguage);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.ContentLanguage, value);
            }

        }

        #endregion

        #region Content-Length

        public UInt64? ContentLength
        {

            get
            {
                return GetHeaderField_UInt64(HTTPHeaderField.ContentLength);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.ContentLength, value);
            }

        }

        #endregion

        #region Content-Location

        public String ContentLocation
        {

            get
            {
                return GetHeaderField(HTTPHeaderField.ContentLocation);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.ContentLocation, value);
            }

        }

        #endregion

        #region Content-MD5

        public String ContentMD5
        {

            get
            {
                return GetHeaderField(HTTPHeaderField.ContentMD5);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.ContentMD5, value);
            }

        }

        #endregion

        #region Content-Range

        public String ContentRange
        {

            get
            {
                return GetHeaderField(HTTPHeaderField.ContentRange);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.ContentRange, value);
            }

        }

        #endregion

        #region Content-Type

        public HTTPContentType ContentType
        {

            get
            {
             //   return GetHeaderField<HTTPContentType>("Content-Type");

                var _ContentType = GetHeaderField<HTTPContentType>("Content-Type");
                if (_ContentType != null)
                    return _ContentType;

                var _ContentTypeString = GetHeaderField<String>("Content-Type");
                if (_ContentTypeString != null)
                {
                    _ContentType = new HTTPContentType(_ContentTypeString);
                    SetHeaderField("Content-Type", _ContentType);
                    return _ContentType;
                }

                return null;

            }

            set
            {
                SetHeaderField(HTTPHeaderField.ContentType, value);
            }

        }

        #endregion

        #region Date

        public String Date
        {

            get
            {
                return GetHeaderField(HTTPHeaderField.Date);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.Date, value);
            }

        }

        #endregion

        #region Via

        public String Via
        {

            get
            {
                return GetHeaderField(HTTPHeaderField.Via);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.Via, value);
            }

        }

        #endregion

        #region Transfer-Encoding

        public String TransferEncoding
        {

            get
            {
                return GetHeaderField(HTTPHeaderField.TransferEncoding);
            }

            set
            {
                SetHeaderField(HTTPHeaderField.TransferEncoding, value);
            }

        }

        #endregion

        #endregion

        #endregion

        #region Events

        #region PropertyChanged

        /// <summary>
        /// Raise an event whenever a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #endregion

        #region Constructor(s)

        #region AHTTPPDUBuilder()

        /// <summary>
        /// Creates a new HTTP header.
        /// </summary>
        public AHTTPPDUBuilder()
        { }

        #endregion

        #endregion


        #region (protected) SetProperty<T>(ref Field, NewValue, PropertyName)

        /// <summary>
        /// Change a property value and raises an PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="Field">The internal field.</param>
        /// <param name="NewValue">The new value of the property.</param>
        /// <param name="PropertyName">The name of the property.</param>
        protected void SetProperty<T>(ref T Field, T NewValue, String PropertyName)
        {

            if (!EqualityComparer<T>.Default.Equals(Field, NewValue))
            {

                Field = NewValue;

                // Take a copy of the handler for concurrency issues!
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(PropertyName));
                }

            }

        }

        #endregion


        #region PrepareImmutability()

        /// <summary>
        /// Prepares the immutability of an HTTP PDU, e.g. calculates
        /// and set the Content-Length header.
        /// </summary>
        protected virtual void PrepareImmutability()
        {

            #region Set the Content-Length if it was not set before

            if (ContentLength == 0)
            {
                if (Content != null)
                    ContentLength = (UInt64) Content.LongLength;

                else if (ContentStream != null)
                    ContentLength = (UInt64) ContentStream.Length;
            }

            #endregion

        }

        #endregion



        #region (protected) TryGetHeaderField(FieldName)

        /// <summary>
        /// Return a http header field.
        /// </summary>
        /// <param name="FieldName">The key of the requested header field.</param>
        /// <param name="Value">The value of the requested header field.</param>
        /// <returns>True if the requested header exists; false otherwise.</returns>
        protected Boolean TryGetHeaderField(String FieldName, out Object Value)
        {
            return HeaderFields.TryGetValue(FieldName, out Value);
        }

        #endregion

        #region (protected) TryGetHeaderField(HeaderField)

        /// <summary>
        /// Return a http header field.
        /// </summary>
        /// <param name="FieldName">The key of the requested header field.</param>
        /// <param name="Value">The value of the requested header field.</param>
        /// <returns>True if the requested header exists; false otherwise.</returns>
        protected Boolean TryGetHeaderField(HTTPHeaderField HeaderField, out Object Value)
        {
            return HeaderFields.TryGetValue(HeaderField.Name, out Value);
        }

        #endregion

        #region TryGet<T>(Key)

        /// <summary>
        /// Return a http header field.
        /// </summary>
        /// <typeparam name="T">The type of the value of the requested header field.</typeparam>
        /// <param name="Key">The key of the requested header field.</param>
        /// <param name="Value">The value of the requested header field.</param>
        /// <returns>True if the requested header exists; false otherwise.</returns>
        public Boolean TryGet<T>(String Key, out T Value)
        {

            Object _Object;

            if (HeaderFields.TryGetValue(Key, out _Object))
            {

                if (_Object is T)
                {
                    Value = (T) _Object;
                    return true;
                }

                else if (typeof(T).Equals(typeof(Int32)))
                {
                    Int32 _Int32;
                    if (Int32.TryParse(_Object.ToString(), out _Int32))
                    {
                        Value = (T) (Object) _Int32;
                        SetHeaderField(Key, Value);
                        return true;
                    }
                }

                else if (typeof(T).Equals(typeof(UInt32)))
                {
                    UInt32 _UInt32;
                    if (UInt32.TryParse(_Object.ToString(), out _UInt32))
                    {
                        Value = (T) (Object) _UInt32;
                        SetHeaderField(Key, Value);
                        return true;
                    }
                }

                else if (typeof(T).Equals(typeof(Int64)))
                {
                    Int64 _Int64;
                    if (Int64.TryParse(_Object.ToString(), out _Int64))
                    {
                        Value = (T) (Object) _Int64;
                        SetHeaderField(Key, Value);
                        return true;
                    }
                }

                else if (typeof(T).Equals(typeof(UInt64)))
                {
                    UInt64 _UInt64;
                    if (UInt64.TryParse(_Object.ToString(), out _UInt64))
                    {
                        Value = (T) (Object) _UInt64;
                        SetHeaderField(Key, Value);
                        return true;
                    }
                }

                else
                {
                    try
                    {
                        Value = (T) (Object) _Object;
                        SetHeaderField(Key, Value);
                        return true;
                    }
                    catch (Exception)
                    {
                        Value = default(T);
                        return false;
                    }                    
                }

            }


            Value = default(T);
            return false;

        }

        #endregion



        #region (protected) GetHeaderField(FieldName)

        /// <summary>
        /// Return the value of the given HTTP header field.
        /// </summary>
        /// <param name="FieldName">The name of the header field.</param>
        protected String GetHeaderField(String FieldName)
        {

            Object Value;
            if (HeaderFields.TryGetValue(FieldName, out Value))
                return Value.ToString();

            return null;

        }

        #endregion

        #region (protected) GetHeaderField<T>(FieldName)

        /// <summary>
        /// Return the given HTTP header field.
        /// </summary>
        /// <typeparam name="T">The expected type of the field value.</typeparam>
        /// <param name="FieldName">The name of the header field.</param>
        protected T GetHeaderField<T>(String FieldName)
        {

            Object Value;
            if (HeaderFields.TryGetValue(FieldName, out Value))
                if (Value is T)
                    return (T) Value;

            return default(T);

        }

        #endregion

        #region (protected) GetHeaderField(HeaderField)

        /// <summary>
        /// Return the value of the given HTTP header field.
        /// </summary>
        /// <param name="HeaderField">The HTTP header field.</param>
        protected String GetHeaderField(HTTPHeaderField HeaderField)
        {

            Object Value;
            if (HeaderFields.TryGetValue(HeaderField.Name, out Value))
                return Value.ToString();

            return null;

        }

        #endregion

        #region (protected) GetHeaderField<T>(HeaderField)

        /// <summary>
        /// Return the value of the given HTTP header field.
        /// </summary>
        /// <typeparam name="T">The expected type of the field value.</typeparam>
        /// <param name="HeaderField">The HTTP header field.</param>
        protected T GetHeaderField<T>(HTTPHeaderField HeaderField)
        {

            Object Value;
            if (HeaderFields.TryGetValue(HeaderField.Name, out Value))
                if (Value is String)
                {
                    if (HeaderField.Type == typeof(String))
                        return (T) Value;
                    else
                    {
                        Object Value2 = null;
                        if (HeaderField.StringParser(Value.ToString(), out Value2))
                            return (T) Value2;
                    }
                }
                else
                    return (T) Value;

            return default(T);

        }

        #endregion

        #region (protected) GetHeaderField_Int64(FieldName)

        /// <summary>
        /// Return the given HTTP header field.
        /// </summary>
        /// <param name="FieldName">The name of the header field.</param>
        protected Int64? GetHeaderField_Int64(String FieldName)
        {

            Object Value;
            if (HeaderFields.TryGetValue(FieldName, out Value))
            {

                if (Value is Int64?)
                    return (Int64?) Value;

                Int64 Int64Value;
                if (Int64.TryParse(Value.ToString(), out Int64Value))
                    return Int64Value;

            }

            return null;

        }

        #endregion

        #region (protected) GetHeaderField_UInt64(FieldName)

        /// <summary>
        /// Return the given HTTP header field.
        /// </summary>
        /// <typeparam name="T">The expected type of the field value.</typeparam>
        /// <param name="FieldName">The name of the header field.</param>
        protected UInt64? GetHeaderField_UInt64(String FieldName)
        {

            Object Value;
            if (HeaderFields.TryGetValue(FieldName, out Value))
            {

                if (Value is UInt64?)
                    return (UInt64?) Value;

                UInt64 UInt64Value;
                if (UInt64.TryParse(Value.ToString(), out UInt64Value))
                    return UInt64Value;

            }

            return null;

        }

        #endregion

        #region (protected) GetHeaderField_UInt64(HeaderField)

        /// <summary>
        /// Return the given HTTP header field.
        /// </summary>
        /// <typeparam name="T">The expected type of the field value.</typeparam>
        /// <param name="HeaderField">The HTTP header field.</param>
        protected UInt64? GetHeaderField_UInt64(HTTPHeaderField HeaderField)
        {

            Object Value;
            if (HeaderFields.TryGetValue(HeaderField.Name, out Value))
            {

                if (Value is UInt64?)
                    return (UInt64?) Value;

                UInt64 UInt64Value;
                if (UInt64.TryParse(Value.ToString(), out UInt64Value))
                    return UInt64Value;

            }

            return null;

        }

        #endregion


        #region (protected) SetHeaderField(FieldName, Value)

        /// <summary>
        /// Set a HTTP header field.
        /// A field value of NULL will remove the field from the header.
        /// </summary>
        /// <param name="FieldName">The name of the header field.</param>
        /// <param name="Value">The value. NULL will remove the field from the header.</param>
        protected void SetHeaderField(String FieldName, Object Value)
        {

            if (Value != null)
            {

                if (HeaderFields.ContainsKey(FieldName))
                    HeaderFields[FieldName] = Value;
                else
                    HeaderFields.Add(FieldName, Value);

            }

            else
                if (HeaderFields.ContainsKey(FieldName))
                    HeaderFields.Remove(FieldName);

        }

        #endregion

        #region (protected) SetHeaderField(HeaderField, Value)

        /// <summary>
        /// Set a HTTP header field.
        /// A field value of NULL will remove the field from the header.
        /// </summary>
        /// <param name="FieldName">The name of the header field.</param>
        /// <param name="Value">The value. NULL will remove the field from the header.</param>
        protected void SetHeaderField(HTTPHeaderField HeaderField, Object Value)
        {

            if (Value != null)
            {

                if (HeaderFields.ContainsKey(HeaderField.Name))
                    HeaderFields[HeaderField.Name] = Value;
                else
                    HeaderFields.Add(HeaderField.Name, Value);

            }

            else
                if (HeaderFields.ContainsKey(HeaderField.Name))
                    HeaderFields.Remove(HeaderField.Name);

        }

        #endregion

        #region (protected) RemoveHeaderField(FieldName)

        /// <summary>
        /// Remove a HTTP header field.
        /// </summary>
        /// <param name="FieldName">The name of the header field.</param>
        protected void RemoveHeaderField(String FieldName)
        {
            if (HeaderFields.ContainsKey(FieldName))
                HeaderFields.Remove(FieldName);
        }

        #endregion


        #region GetEnumerator()

        /// <summary>
        /// Return an enumeration of all header lines.
        /// </summary>
        public IEnumerator<KeyValuePair<String, Object>> GetEnumerator()
        {
            return HeaderFields.GetEnumerator();
        }

        /// <summary>
        /// Return an enumeration of all header lines.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return HeaderFields.GetEnumerator();
        }

        #endregion

    }

}

