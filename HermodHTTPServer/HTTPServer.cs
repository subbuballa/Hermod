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
using System.Net;

using de.ahzf.Vanaheimr.Hermod.Sockets.TCP;
using de.ahzf.Vanaheimr.Hermod.Datastructures;

#endregion

namespace de.ahzf.Vanaheimr.Hermod.HTTP
{

    #region HTTPServer -> HTTPServer<DefaultHTTPService>

    /// <summary>
    /// A HTTP server serving a default HTTP service.
    /// </summary>
    public class HTTPServer : HTTPServer<IDefaultHTTPService>
    {

        #region Constructor(s)

        #region HTTPServer(Port, NewHTTPConnectionHandler = null, AutoStart = false)

        /// <summary>
        /// Initialize the HTTPServer using IPAddress.Any and the given parameters.
        /// </summary>
        /// <param name="Port">The listening port</param>
        /// <param name="NewHTTPConnectionHandler">A delegate called for every new http connection.</param>
        /// <param name="Autostart">Autostart the http server.</param>
        public HTTPServer(IPPort Port, NewHTTPServiceHandler NewHTTPConnectionHandler = null, Boolean Autostart = false)
            : this(IPv4Address.Any, Port, NewHTTPConnectionHandler, Autostart)
        { }

        #endregion

        #region HTTPServer(IIPAddress, Port, NewHTTPConnectionHandler = null, AutoStart = false)

        /// <summary>
        /// Initialize the HTTPServer using the given parameters.
        /// </summary>
        /// <param name="IIPAddress">The listening IP address(es)</param>
        /// <param name="Port">The listening port</param>
        /// <param name="NewHTTPConnectionHandler">A delegate called for every new http connection.</param>
        /// <param name="Autostart">Autostart the http server.</param>
        public HTTPServer(IIPAddress IIPAddress, IPPort Port, NewHTTPServiceHandler NewHTTPConnectionHandler = null, Boolean Autostart = false)
            : base(IIPAddress, Port, NewHTTPConnectionHandler, Autostart)
        { }

        #endregion

        #region HTTPServer(IPSocket, NewHTTPConnectionHandler = null, Autostart = false)

        /// <summary>
        /// Initialize the HTTPServer using the given parameters.
        /// </summary>
        /// <param name="IPSocket">The listening IPSocket.</param>
        /// <param name="NewHTTPConnectionHandler">A delegate called for every new http connection.</param>
        /// <param name="Autostart">Autostart the http server.</param></param>
        public HTTPServer(IPSocket IPSocket, NewHTTPServiceHandler NewHTTPConnectionHandler = null, Boolean Autostart = false)
            : this(IPSocket.IPAddress, IPSocket.Port, NewHTTPConnectionHandler, Autostart)
        { }

        #endregion

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {

            var _Running = "";
            if (IsRunning) _Running = " (running)";

            return String.Concat(this.GetType().Name, " ", IPAddress.ToString(), ":", Port, _Running);

        }

        #endregion

    }

    #endregion

    #region HTTPServer<DefaultHTTPService>

    /// <summary>
    ///  This http server will listen on a tcp port and maps incoming urls
    ///  to methods of HTTPServiceInterface.
    /// </summary>
    /// <typeparam name="HTTPServiceInterface">A http service interface.</typeparam>
    public class HTTPServer<HTTPServiceInterface> : AHTTPServer<HTTPServiceInterface>, IHTTPServer
        where HTTPServiceInterface : class, IHTTPService
    {

        #region Data

        /// <summary>
        /// The internal TCP server.
        /// </summary>
        private readonly TCPServer<HTTPConnection<HTTPServiceInterface>> _TCPServer;

        #endregion

        #region Properties

        #region IPAdress

        /// <summary>
        /// Gets the IPAddress on which the HTTPServer listens.
        /// </summary>
        public IIPAddress IPAddress
        {
            get
            {
                
                if (_TCPServer != null)
                    return _TCPServer.IPAddress;

                return null;

            }
        }

        #endregion

        #region Port

        /// <summary>
        /// Gets the port on which the HTTPServer listens.
        /// </summary>
        public IPPort Port
        {
            get
            {

                if (_TCPServer != null)
                    return _TCPServer.Port;

                return null;
            
            }
        }

        #endregion

        #region IsRunning

        /// <summary>
        /// True while the HTTPServer is listening for new clients.
        /// </summary>
        public Boolean IsRunning
        {
            get
            {
                
                if (_TCPServer != null)
                    return _TCPServer.IsRunning;

                return false;

            }
        }

        #endregion

        #region StopRequested

        /// <summary>
        /// The HTTPServer was requested to stop and will no
        /// longer accept new client connections.
        /// </summary>
        public Boolean StopRequested
        {
            get
            {

                if (_TCPServer != null)
                    return _TCPServer.StopRequested;

                return false;

            }
        }

        #endregion

        #region NumberOfClients

        /// <summary>
        /// The current number of connected clients.
        /// </summary>
        public UInt64 NumberOfClients
        {
            get
            {

                if (_TCPServer != null)
                    return _TCPServer.NumberOfClients;

                return 0;

            }
        }

        #endregion

        #region MaxClientConnections

        /// <summary>
        /// The maximum number of pending client connections.
        /// </summary>
        public UInt32 MaxClientConnections
        {
            get
            {
                
                if (_TCPServer != null)
                    return _TCPServer.MaxClientConnections;

                return 0;

            }
        }

        #endregion

        #region ClientTimeout

        /// <summary>
        /// Will set the ClientTimeout for all incoming client connections
        /// </summary>
        public UInt32 ClientTimeout
        {
            get
            {

                if (_TCPServer != null)
                    return _TCPServer.ClientTimeout;

                return 0;

            }
        }

        #endregion

        #region DefaultServerName

        private const String _DefaultServerName = "Hermod HTTP Server v0.1";

        /// <summary>
        /// The default server name.
        /// </summary>
        public virtual String DefaultServerName
        {
            get
            {
                return _DefaultServerName;
            }
        }

        #endregion

        #endregion

        #region Events

        #region OnExceptionOccured

        private event ExceptionOccuredHandler _OnExceptionOccured;

        /// <summary>
        /// An exception has occured.
        /// </summary>
        public event ExceptionOccuredHandler OnExceptionOccured
        {

            add
            {
                _OnExceptionOccured           += value;
                _TCPServer.OnExceptionOccured += value;
            }

            remove
            {
                _OnExceptionOccured           -= value;
                _TCPServer.OnExceptionOccured -= value;
            }

        }

        #endregion

        #region OnNewHTTPService

        /// <summary>
        /// A delegate definition for every incoming HTTP connection.
        /// </summary>
        /// <param name="HTTPServiceInterfaceType">The interface of the associated http connections.</param>
        public delegate void NewHTTPServiceHandler(HTTPServiceInterface HTTPServiceInterfaceType);

        /// <summary>
        /// An event called for every incoming HTTP connection.
        /// </summary>
        public event NewHTTPServiceHandler OnNewHTTPService;

        #endregion

        #endregion

        #region Constructor(s)

        #region HTTPServer(NewHTTPConnectionHandler = null)

        /// <summary>
        /// Initialize the HTTPServer using IPAddress.Any, http port 80 and start the server.
        /// </summary>
        /// <param name="NewHTTPConnectionHandler">A delegate called for every new http connection.</param>
        public HTTPServer(NewHTTPServiceHandler NewHTTPConnectionHandler = null)
            : this(IPv4Address.Any, IPPort.HTTP, NewHTTPConnectionHandler, true)
        { }

        #endregion

        #region HTTPServer(Port, NewHTTPServiceHandler = null, AutoStart = true)

        /// <summary>
        /// Initialize the HTTPServer using IPAddress.Any and the given parameters.
        /// </summary>
        /// <param name="Port">The listening port</param>
        /// <param name="NewHTTPServiceHandler">A delegate called for every new http connection.</param>
        /// <param name="Autostart">Autostart the http server.</param>
        public HTTPServer(IPPort Port, NewHTTPServiceHandler NewHTTPServiceHandler = null, Boolean Autostart = true)
            : this(IPv4Address.Any, Port, NewHTTPServiceHandler, Autostart)
        { }

        #endregion

        #region HTTPServer(IIPAddress, Port, NewHTTPServiceHandler = null, AutoStart = true)

        /// <summary>
        /// Initialize the HTTPServer using the given parameters.
        /// </summary>
        /// <param name="IIPAddress">The listening IP address(es)</param>
        /// <param name="Port">The listening port</param>
        /// <param name="NewHTTPServiceHandler">A delegate called for every new http connection.</param>
        /// <param name="Autostart">Autostart the http server.</param>
        public HTTPServer(IIPAddress IIPAddress, IPPort Port, NewHTTPServiceHandler NewHTTPServiceHandler = null, Boolean Autostart = true)
        {

            ServerName = _DefaultServerName;

            if (NewHTTPServiceHandler != null)
                OnNewHTTPService += NewHTTPServiceHandler;

            _TCPServer = new TCPServer<HTTPConnection<HTTPServiceInterface>>(
                                 IIPAddress,
                                 Port,
                                 NewHTTPConnection =>
                                     {
                          
                                         NewHTTPConnection.HTTPServer            = this;
                                         NewHTTPConnection.ServerName            = ServerName;
                                         NewHTTPConnection.HTTPSecurity          = HTTPSecurity;
                                         NewHTTPConnection.URLMapping            = URLMapping;
                                         NewHTTPConnection.NewHTTPServiceHandler = OnNewHTTPService;
                                         NewHTTPConnection.Implementations       = Implementations;
                          
                                         try
                                         {
                                             NewHTTPConnection.ProcessHTTP();
                                         }
                                         catch (Exception Exception)
                                         {
                                             var OnExceptionOccured_Local = _OnExceptionOccured;
                                             if (OnExceptionOccured_Local != null)
                                                 OnExceptionOccured_Local(this, Exception);
                                         }
                          
                                     },
                                 // Don't do it now, do it a bit later...
                                 Autostart: false,
                                 ThreadDescription: "HTTPServer<" + typeof(HTTPServiceInterface).Name + ">");

            if (Autostart)
                _TCPServer.Start();

        }

        #endregion

        #region HTTPServer(IPSocket, NewHTTPServiceHandler = null, Autostart = true)

        /// <summary>
        /// Initialize the HTTPServer using the given parameters.
        /// </summary>
        /// <param name="IPSocket">The listening IPSocket.</param>
        /// <param name="NewHTTPServiceHandler">A delegate called for every new http connection.</param>
        /// <param name="Autostart">Autostart the http server.</param>
        public HTTPServer(IPSocket IPSocket, NewHTTPServiceHandler NewHTTPServiceHandler = null, Boolean Autostart = true)
            : this(IPSocket.IPAddress, IPSocket.Port, NewHTTPServiceHandler, Autostart)
        { }

        #endregion

        #endregion


        #region Start()

        /// <summary>
        /// Start the server.
        /// </summary>
        public void Start()
        {
            _TCPServer.Start();
        }

        #endregion

        #region Shutdown()

        /// <summary>
        /// Shutdown the server.
        /// </summary>
        public void Shutdown()
        {
            _TCPServer.Shutdown();
        }

        #endregion

        #region Dispose()

        /// <summary>
        /// Dispose this HTTP server.
        /// </summary>
        public void Dispose()
        {
            Shutdown();
        }

        #endregion


        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {

            var _TypeName         = this.GetType().Name;
            var _GenericArguments = this.GetType().GetGenericArguments();
            var _GenericTypeName  = "";

            if (_GenericArguments.Length > 0)
            {
                _GenericTypeName  = String.Concat("<", _GenericArguments[0].Name, ">");
                _TypeName         = _TypeName.Remove(_TypeName.Length - 2);
            }

            var _Running = "";
            if (_TCPServer.IsRunning) _Running = " (running)";

            return String.Concat(_TypeName, _GenericTypeName, " at ", _TCPServer.IPAddress.ToString(), ":", _TCPServer.Port, _Running);

        }

        #endregion

    }

    #endregion

}
