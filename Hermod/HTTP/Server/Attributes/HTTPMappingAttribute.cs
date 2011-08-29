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
using de.ahzf.Hermod.HTTP.Common;

#endregion

namespace de.ahzf.Hermod.HTTP
{

    /// <summary>
    /// Mapps a HTTP request onto a .NET method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class HTTPMappingAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// The HTTP method of this HTTP mapping.
        /// </summary>
        public HTTPMethod HTTPMethod { get; private set; }

        /// <summary>
        /// The URI template of this HTTP mapping.
        /// </summary>
        public String UriTemplate { get; private set; }

        #endregion

        #region Constructor(s)

        #region HTTPMappingAttribute(HTTPMethod, UriTemplate)

        /// <summary>
        /// Generates a new HTTP mapping.
        /// </summary>
        /// <param name="HTTPMethod">The HTTP method of this HTTP mapping.</param>
        /// <param name="UriTemplate">The URI template of this HTTP mapping.</param>
        public HTTPMappingAttribute(HTTPMethod HTTPMethod, String UriTemplate)
        {
            this.HTTPMethod  = HTTPMethod;
            this.UriTemplate = UriTemplate;
        }

        #endregion

        #region HTTPMappingAttribute(HTTPMethod, UriTemplate)

        /// <summary>
        /// Generates a new HTTP mapping.
        /// </summary>
        /// <param name="HTTPMethod">The HTTP method of this HTTP mapping.</param>
        /// <param name="UriTemplate">The URI template of this HTTP mapping.</param>
        public HTTPMappingAttribute(HTTPMethods HTTPMethod, String UriTemplate)
        {
            
            this.HTTPMethod  = de.ahzf.Hermod.HTTP.Common.HTTPMethod.ParseEnum(HTTPMethod);

            if (this.HTTPMethod == null)
                throw new ArgumentNullException("Invalid HTTPMethod!");
            
            this.UriTemplate = UriTemplate;

        }

        #endregion

        #endregion

    }

}
