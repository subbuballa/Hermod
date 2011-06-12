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
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Net;

#endregion

namespace de.ahzf.Hermod.Datastructures
{

    /// <summary>
    /// An IPSocket is a combination of an IPAddress and a port.
    /// </summary>    
    public class IPSocket : IComparable, IComparable<IPSocket>, IEquatable<IPSocket>
    {

        #region Properties

        /// <summary>
        /// The IPAddress of this IPSocket.
        /// </summary>
        public IIPAddress IPAddress { get; protected set; }

        /// <summary>
        /// Returns the port of this IPSocket.
        /// </summary>
        public IPPort     Port      { get; protected set; }

        #endregion

        #region Constructor(s)

        #region IPSocket(IPAddress, Port)

        /// <summary>
        /// Generates a new IPSocket.
        /// </summary>
        /// <param name="IPAddress">The IPAdress of the socket.</param>
        /// <param name="Port">The port of the socket.</param>
        public IPSocket(IIPAddress IPAddress, IPPort Port)
        {
            this.IPAddress = IPAddress;
            this.Port      = Port;
        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (myIPSocket1, myIPSocket2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIPSocket1">A IPSocket.</param>
        /// <param name="myIPSocket2">Another IPSocket.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (IPSocket myIPSocket1, IPSocket myIPSocket2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myIPSocket1, myIPSocket2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myIPSocket1 == null) || ((Object) myIPSocket2 == null))
                return false;

            return myIPSocket1.Equals(myIPSocket2);

        }

        #endregion

        #region Operator != (myIPSocket1, myIPSocket2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIPSocket1">A IPSocket.</param>
        /// <param name="myIPSocket2">Another IPSocket.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (IPSocket myIPSocket1, IPSocket myIPSocket2)
        {
            return !(myIPSocket1 == myIPSocket2);
        }

        #endregion

        #endregion


        #region IComparable Members

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("myObject must not be null!");

            // Check if myObject can be casted to an ElementId object
            var myIPSocket = myObject as IPSocket;
            if ((Object) myIPSocket == null)
                throw new ArgumentException("myObject is not of type IPSocket!");

            return CompareTo(myIPSocket);

        }

        #endregion

        #region IComparable<IPSocket> Members

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myElementId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IPSocket myIPSocket)
        {

            // Check if myIPSocket is null
            if (myIPSocket == null)
                throw new ArgumentNullException("myElementId must not be null!");

            //return _IPAddress.GetAddressBytes() .CompareTo(myIPSocket._IPAddress);
            return 0;

        }

        #endregion

        #region IEquatable<IPSocket> Members

        #region Equals(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("Parameter myObject must not be null!");

            // Check if myObject can be cast to IPSocket
            var myIPSocket = myObject as IPSocket;
            if ((Object) myIPSocket == null)
                throw new ArgumentException("Parameter myObject could not be casted to type IPSocket!");

            return this.Equals(myIPSocket);

        }

        #endregion

        #region Equals(myIPSocket)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myElementId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(IPSocket myIPSocket)
        {

            // Check if myIPSocket is null
            if (myIPSocket == null)
                throw new ArgumentNullException("Parameter myIPSocket must not be null!");

            var __IPAddress = this.IPAddress.Equals(myIPSocket.IPAddress);

            if (__IPAddress)
                return this.Port.Equals(myIPSocket.Port);

            return false;

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
            return IPAddress.GetHashCode() ^ Port.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override String ToString()
        {
            return IPAddress.ToString() + ":" + Port.ToString();
        }

        #endregion

    }

}
