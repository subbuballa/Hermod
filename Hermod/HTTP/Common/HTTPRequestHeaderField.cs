﻿/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Hermod
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
using System.Linq;

#endregion

namespace de.ahzf.Hermod.HTTP.Common
{

    /// <summary>
    /// Defines a field within the HTTP request header
    /// </summary>
    public class HTTPRequestHeaderField : AHTTPHeaderField, IEquatable<HTTPRequestHeaderField>, IComparable<HTTPRequestHeaderField>, IComparable
    {

        #region Constructor(s)

        #region HTTPRequestHeaderField(Name, Description = null, Example = null, SeeAlso = null)

        /// <summary>
        /// Creates a new HTTP request header field based on
        /// a name and a description.
        /// </summary>
        /// <param name="Name">The name of the HTTP request header field</param>
        /// <param name="Description">A description of the HTTP request header field</param>
        /// <param name="Example">An usage example</param>
        /// <param name="SeeAlso">An additional source of information, e.g. the defining request-for-comment</param>
        public HTTPRequestHeaderField(String Name, String Description = null, String Example = null, Uri SeeAlso = null)
            : base(Name, null, Description, Example, SeeAlso)
        { }

        #endregion

        #endregion


        #region General header fields

        // http://restpatterns.org

        /// <summary>
        /// The Cache-Control general-header field is used to specify
        /// directives that MUST be obeyed by all caching mechanisms
        /// along the request/response chain. The directives specify
        /// behavior intended to prevent caches from adversely
        /// interfering with the request or response.
        /// </summary>
        public static readonly HTTPRequestHeaderField CacheControl = new HTTPRequestHeaderField(
                                                      "Cache-Control",
                                                      "The Connection general-header field allows the sender to " +
                                                      "specify options that are desired for that particular " +
                                                      "connection and MUST NOT be communicated by proxies over " +
                                                      "further connections.",
                                                      "Cache-Control: no-cache",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Connection general-header field allows the sender
        /// to specify options that are desired for that particular
        /// connection and MUST NOT be communicated by proxies over
        /// further connections.
        /// HTTP/1.1 applications that do not support persistent
        /// connections MUST include the "close" connection option
        /// in every message. 
        /// </summary>
        public static readonly HTTPRequestHeaderField Connection = new HTTPRequestHeaderField(
                                                      "Connection",
                                                      "The Cache-Control general-header field is used to specify " +
                                                      "directives that MUST be obeyed by all caching mechanisms " +
                                                      "along the request/response chain. The directives specify " +
                                                      "behavior intended to prevent caches from adversely " +
                                                      "interfering with the request or response. " +
                                                      "HTTP/1.1 applications that do not support persistent " +
                                                      "connections MUST include the \"close\" connection option " +
                                                      "in every message.",
                                                      "Connection: close",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Content-Encoding entity-header field is used as a modifier
        /// to the media-type. When present, its value indicates what
        /// additional content codings have been applied to the entity-body,
        /// and thus what decoding mechanisms must be applied in order to
        /// obtain the media-type referenced by the Content-Type header
        /// field.
        /// If the content-encoding of an entity in a request message is not
        /// acceptable to the origin server, the server SHOULD respond with
        /// a status code of 415 (Unsupported Media Type).
        /// </summary>
        public static readonly HTTPRequestHeaderField ContentEncoding = new HTTPRequestHeaderField(
                                                      "Content-Encoding",
                                                      "The Content-Encoding entity-header field is used as a " +
                                                      "modifier to the media-type. When present, its value " +
                                                      "indicates what additional content codings have been " +
                                                      "applied to the entity-body, and thus what decoding " +
                                                      "mechanisms must be applied in order to obtain the " +
                                                      "media-type referenced by the Content-Type header field. " +
                                                      "If the content-encoding of an entity in a request message " +
                                                      "is not acceptable to the origin server, the server SHOULD " +
                                                      "respond with a status code of 415 (Unsupported Media Type).",
                                                      "Content-Encoding: gzip",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Content-Language entity-header field describes the natural
        /// language(s) of the intended audience for the enclosed entity.
        /// </summary>
        public static readonly HTTPRequestHeaderField ContentLanguage = new HTTPRequestHeaderField(
                                                      "Content-Language",
                                                      "The Content-Language entity-header field describes the " +
                                                      "natural language(s) of the intended audience for the " +
                                                      "enclosed entity.",
                                                      "Content-Language: en, de",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Content-Length entity-header field indicates the size of
        /// the entity-body, in decimal number of OCTETs, sent to the
        /// recipient or, in the case of the HEAD method, the size of the
        /// entity-body that would have been sent if the request had been
        /// a GET request.
        /// </summary>
        public static readonly HTTPRequestHeaderField ContentLength = new HTTPRequestHeaderField(
                                                      "Content-Length",
                                                      "The Content-Length entity-header field indicates the size of " +
                                                      "the entity-body, in decimal number of OCTETs, sent to the " +
                                                      "recipient or, in the case of the HEAD method, the size of the " +
                                                      "entity-body that would have been sent if the request had been " +
                                                      "a GET request.",
                                                      "Content-Length: 3495",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Content-Location entity-header field MAY be used to supply
        /// the resource location for the entity enclosed in the message
        /// when that entity is accessible from a location separate from
        /// the requested resource's URI.
        /// If the Content-Location is a relative URI, the relative URI is
        /// interpreted relative to the Request-URI. 
        /// </summary>
        public static readonly HTTPRequestHeaderField ContentLocation = new HTTPRequestHeaderField(
                                                      "Content-Location",
                                                      "The Content-Location entity-header field MAY be used to supply " +
                                                      "the resource location for the entity enclosed in the message " +
                                                      "when that entity is accessible from a location separate from " +
                                                      "the requested resource's URI. " +
                                                      "If the Content-Location is a relative URI, the relative URI " + 
                                                      "is interpreted relative to the Request-URI.",
                                                      "Content-Location: ../test.html",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Content-MD5 entity-header field, is an MD5 digest of the
        /// entity-body for the purpose of providing an end-to-end
        /// message integrity check (MIC) of the entity-body.
        /// Note: a MIC is good for detecting accidental modification
        /// of the entity-body in transit, but is not proof against
        /// malicious attacks.
        /// </summary>
        public static readonly HTTPRequestHeaderField ContentMD5 = new HTTPRequestHeaderField(
                                                      "Content-MD5",
                                                      "The Content-MD5 entity-header field, is an MD5 digest of the " +
                                                      "entity-body for the purpose of providing an end-to-end " +
                                                      "message integrity check (MIC) of the entity-body. " +
                                                      "Note: a MIC is good for detecting accidental modification " +
                                                      "of the entity-body in transit, but is not proof against " +
                                                      "malicious attacks.",
                                                      "MD5-Digest: <base64 of 128 bit MD5 digest as per RFC 1864>",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Content-Range entity-header is sent with a partial
        /// entity-body to specify where in the full entity-body the
        /// partial body should be applied.
        /// The header SHOULD indicate the total length of the full
        /// entity-body, unless this length is unknown or difficult
        /// to determine. The asterisk "*" character means that the
        /// instance-length is unknown at the time when the response
        /// was generated.
        /// Partinal content replies must be sent using the response
        /// code 206 (Partial content). When an HTTP message includes
        /// the content of multiple ranges, these are transmitted as a
        /// multipart message using the media type "multipart/byteranges".
        /// Syntactically invalid content-range reqeuests, SHOULD be
        /// treated as if the invalid Range header field did not exist.
        /// Normally, this means return a 200 response containing the
        /// full entity.
        /// If the server receives a request (other than one including
        /// an If-Range request-header field) with an unsatisfiable
        /// Range request-header field, it SHOULD return a response
        /// code of 416 (Requested range not satisfiable).
        /// </summary>         
        public static readonly HTTPRequestHeaderField ContentRange = new HTTPRequestHeaderField(
                                                      "Content-Range",
                                                      "The Content-Range entity-header is sent with a partial " +
                                                      "entity-body to specify where in the full entity-body the " +
                                                      "partial body should be applied. " +
                                                      "The header SHOULD indicate the total length of the full " +
                                                      "entity-body, unless this length is unknown or difficult " +
                                                      "to determine. The asterisk \"*\" character means that the " +
                                                      "instance-length is unknown at the time when the response " +
                                                      "was generated. " +
                                                      "Partinal content replies must be sent using the response " +
                                                      "code 206 (Partial content). When an HTTP message includes " +
                                                      "the content of multiple ranges, these are transmitted as a " +
                                                      "multipart message using the media type \"multipart/byteranges\". " +
                                                      "Syntactically invalid content-range reqeuests, SHOULD be " +
                                                      "treated as if the invalid Range header field did not exist. " +
                                                      "Normally, this means return a 200 response containing the " +
                                                      "full entity. " +
                                                      "If the server receives a request (other than one including " +
                                                      "an If-Range request-header field) with an unsatisfiable " +
                                                      "Range request-header field, it SHOULD return a response " +
                                                      "code of 416 (Requested range not satisfiable).",
                                                      "Content-Range: bytes 21010-47021/47022",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Content-Type entity-header field indicates the media
        /// type of the entity-body sent to the recipient or, in the
        /// case of the HEAD method, the media type that would have
        /// been sent had the request been a GET.
        /// </summary>         
        public static readonly HTTPRequestHeaderField ContentType = new HTTPRequestHeaderField(
                                                      "Content-Type",
                                                      "The Content-Type entity-header field indicates the media type " +
                                                      "of the entity-body sent to the recipient or, in the case of the " +
                                                      "HEAD method, the media type that would have been sent had the " +
                                                      "request been a GET.",
                                                      "Content-Type: text/html; charset=ISO-8859-4",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));


        /// <summary>
        /// The Via general-header field MUST be used by gateways
        /// and proxies to indicate the intermediate protocols and
        /// recipients between the user agent and the server on
        /// requests, and between the origin server and the client
        /// on responses.
        /// </summary>         
        public static readonly HTTPRequestHeaderField Via = new HTTPRequestHeaderField(
                                                      "Via",
                                                      "The Via general-header field MUST be used by gateways and " +
                                                      "proxies to indicate the intermediate protocols and recipients " +
                                                      "between the user agent and the server on requests, and between " +
                                                      "the origin server and the client on responses.",
                                                      "Via: 1.0 fred, 1.1 nowhere.com (Apache/1.1)",
                                                      new Uri("http://tools.ietf.org/html/rfc2616"));



        #endregion


        #region Operator overloading

        #region Operator == (HTTPRequestHeaderField1, HTTPRequestHeaderField2)

        public static Boolean operator == (HTTPRequestHeaderField HTTPRequestHeaderField1, HTTPRequestHeaderField HTTPRequestHeaderField2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(HTTPRequestHeaderField1, HTTPRequestHeaderField2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) HTTPRequestHeaderField1 == null) || ((Object) HTTPRequestHeaderField2 == null))
                return false;

            return HTTPRequestHeaderField1.Equals(HTTPRequestHeaderField2);

        }

        #endregion

        #region Operator != (HTTPRequestHeaderField1, HTTPRequestHeaderField2)

        public static Boolean operator != (HTTPRequestHeaderField HTTPRequestHeaderField1, HTTPRequestHeaderField HTTPRequestHeaderField2)
        {
            return !(HTTPRequestHeaderField1 == HTTPRequestHeaderField2);
        }

        #endregion

        #region Operator <  (HTTPRequestHeaderField1, HTTPRequestHeaderField2)

        public static Boolean operator < (HTTPRequestHeaderField HTTPRequestHeaderField1, HTTPRequestHeaderField HTTPRequestHeaderField2)
        {

            // Check if HTTPRequestHeaderField1 is null
            if ((Object) HTTPRequestHeaderField1 == null)
                throw new ArgumentNullException("Parameter HTTPRequestHeaderField1 must not be null!");

            // Check if HTTPRequestHeaderField2 is null
            if ((Object) HTTPRequestHeaderField2 == null)
                throw new ArgumentNullException("Parameter HTTPRequestHeaderField2 must not be null!");

            return HTTPRequestHeaderField1.CompareTo(HTTPRequestHeaderField2) < 0;

        }

        #endregion

        #region Operator >  (HTTPRequestHeaderField1, HTTPRequestHeaderField2)

        public static Boolean operator > (HTTPRequestHeaderField HTTPRequestHeaderField1, HTTPRequestHeaderField HTTPRequestHeaderField2)
        {

            // Check if HTTPRequestHeaderField1 is null
            if ((Object) HTTPRequestHeaderField1 == null)
                throw new ArgumentNullException("Parameter HTTPRequestHeaderField1 must not be null!");

            // Check if HTTPRequestHeaderField2 is null
            if ((Object) HTTPRequestHeaderField2 == null)
                throw new ArgumentNullException("Parameter HTTPRequestHeaderField2 must not be null!");

            return HTTPRequestHeaderField1.CompareTo(HTTPRequestHeaderField2) > 0;

        }

        #endregion

        #region Operator <= (HTTPRequestHeaderField1, HTTPRequestHeaderField2)

        public static Boolean operator <= (HTTPRequestHeaderField HTTPRequestHeaderField1, HTTPRequestHeaderField HTTPRequestHeaderField2)
        {
            return !(HTTPRequestHeaderField1 > HTTPRequestHeaderField2);
        }

        #endregion

        #region Operator >= (HTTPRequestHeaderField1, HTTPRequestHeaderField2)

        public static Boolean operator >= (HTTPRequestHeaderField HTTPRequestHeaderField1, HTTPRequestHeaderField HTTPRequestHeaderField2)
        {
            return !(HTTPRequestHeaderField1 < HTTPRequestHeaderField2);
        }

        #endregion

        #endregion

        #region IComparable<HTTPRequestHeaderField> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public new Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an HTTPRequestHeaderField.
            var HTTPRequestField = Object as HTTPRequestHeaderField;
            if ((Object) HTTPRequestField == null)
                throw new ArgumentException("The given object is not a HTTPRequestHeaderField!");

            return CompareTo(HTTPRequestField);

        }

        #endregion

        #region CompareTo(HTTPRequestHeaderField)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HTTPStatusCode">An object to compare with.</param>
        public Int32 CompareTo(HTTPRequestHeaderField HTTPRequestHeaderField)
        {

            if ((Object) HTTPRequestHeaderField == null)
                throw new ArgumentNullException("The given HTTPRequestHeaderField must not be null!");

            return Name.CompareTo(HTTPRequestHeaderField.Name);

        }

        #endregion

        #endregion

        #region IEquatable<HTTPRequestHeaderField> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object is an HTTPRequestHeaderField.
            var HTTPRequestField = Object as HTTPRequestHeaderField;
            if ((Object) HTTPRequestField == null)
                return false;

            return this.Equals(HTTPRequestField);

        }

        #endregion

        #region Equals(HTTPRequestHeaderField)

        /// <summary>
        /// Compares two HTTPRequestHeaderFields for equality.
        /// </summary>
        /// <param name="HTTPRequestHeaderField">An HTTPRequestHeaderField to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(HTTPRequestHeaderField HTTPRequestHeaderField)
        {

            if ((Object) HTTPRequestHeaderField == null)
                return false;

            return Name == HTTPRequestHeaderField.Name;

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return Name.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return Name;
        }

        #endregion

    }

}
