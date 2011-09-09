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
using de.ahzf.Hermod.TCP;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.HTTP.Common;

#endregion

namespace de.ahzf.Hermod.Demo
{

    //[HTTPService(Host: "localhost:8080", ForceAuthentication: true)]
    [HTTPService(HostAuthentication: true)]
    public interface IRESTService : IHTTPService
    {

        #region Landingpage

        /// <summary>
        /// Get Landingpage
        /// </summary>
        /// <returns>Some HTML and JavaScript</returns>
        [HTTPMapping(HTTPMethods.GET, "/"), NoAuthentication]
        HTTPResponse GetRoot();

        #endregion

        #region Defaults

        /// <summary>
        /// Get the raw http request header.
        /// </summary>
        /// <returns>Some plain text.</returns>
        [HTTPMapping(HTTPMethods.GET, "/raw")]
        HTTPResponse GetRAWRequestHeader();

        #endregion

        #region /HelloWorld

        [HTTPMapping(HTTPMethods.OPTIONS, "/HelloWorld")]
        HTTPResponse HelloWorld_OPTIONS();

        [HTTPMapping(HTTPMethods.HEAD,    "/HelloWorld}")]
        HTTPResponse HelloWorld_HEAD();

        [HTTPMapping(HTTPMethods.GET,     "/HelloWorld")]
        HTTPResponse HelloWorld_GET();

        #endregion


        #region Utilities

        /// <summary>
        /// Will return internal resources
        /// </summary>
        /// <returns>internal resources</returns>
        [NoAuthentication]
        [HTTPMapping(HTTPMethods.GET, "/resources/{myResource}")]
        HTTPResponse GetResources(String myResource);

        /// <summary>
        /// Get /favicon.ico
        /// </summary>
        /// <returns>Some HTML and JavaScript.</returns>
        [NoAuthentication]
        [HTTPMapping(HTTPMethods.GET, "/favicon.ico")]
        HTTPResponse GetFavicon();

        /// <summary>
        /// Get /robots.txt
        /// </summary>
        /// <returns>Some search engine info.</returns>
        [NoAuthentication]
        [HTTPMapping(HTTPMethods.GET, "/robots.txt")]
        HTTPResponse GetRobotsTxt();

        /// <summary>
        /// Get /humans.txt
        /// </summary>
        /// <returns>Some search engine info.</returns>
        [NoAuthentication]
        [HTTPMapping(HTTPMethods.GET, "/humans.txt")]
        HTTPResponse GetHumansTxt();

        #endregion

    }

}
