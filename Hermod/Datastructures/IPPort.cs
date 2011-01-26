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

#endregion

namespace de.ahzf.Hermod.Datastructures
{

    /// <summary>
    /// An IPPort is a combination of an IP Address and a port.
    /// </summary>    
    public class IPPort : IComparable, IComparable<IPPort>, IEquatable<IPPort>
    {

        #region Data

        /// <summary>
        /// The internal port number.
        /// </summary>
        protected readonly UInt16 _IPPort;

        #endregion

        #region Properties

        

        #endregion

        #region Constructor(s)

        #region IPPort(myUInt16)

        /// <summary>
        /// Generates a new IPPort.
        /// </summary>
        public IPPort(UInt16 myUInt16)
        {
            _IPPort = myUInt16;
        }

        #endregion

        #endregion


        #region /etc/services

        public static readonly IPPort SSH    = new IPPort(22);
        public static readonly IPPort TELNET = new IPPort(23);
        public static readonly IPPort HTTP   = new IPPort(80);
        public static readonly IPPort HTTPS  = new IPPort(443);

        #endregion


        #region ToUInt16()

        /// <summary>
        /// Returns the IPPort as UInt16.
        /// </summary>
        public UInt16 ToUInt16()
        {
            return _IPPort;
        }

        #endregion

        #region ToInt32()

        /// <summary>
        /// Returns the IPPort as Int32.
        /// </summary>
        public Int32 ToInt32()
        {
            return _IPPort;
        }

        #endregion


        #region Operator overloading

        #region Operator == (myIPPort1, myIPPort2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIPPort1">A IPPort.</param>
        /// <param name="myIPPort2">Another IPPort.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (IPPort myIPPort1, IPPort myIPPort2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myIPPort1, myIPPort2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myIPPort1 == null) || ((Object) myIPPort2 == null))
                return false;

            return myIPPort1.Equals(myIPPort2);

        }

        #endregion

        #region Operator != (myIPPort1, myIPPort2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIPPort1">A IPPort.</param>
        /// <param name="myIPPort2">Another IPPort.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (IPPort myIPPort1, IPPort myIPPort2)
        {
            return !(myIPPort1 == myIPPort2);
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
            var myIPPort = myObject as IPPort;
            if ((Object) myIPPort == null)
                throw new ArgumentException("myObject is not of type IPPort!");

            return CompareTo(myIPPort);

        }

        #endregion

        #region IComparable<IPPort> Members

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myElementId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IPPort myIPPort)
        {

            // Check if myIPPort is null
            if (myIPPort == null)
                throw new ArgumentNullException("myElementId must not be null!");

            return _IPPort.CompareTo(myIPPort._IPPort);

        }

        #endregion

        #region IEquatable<IPPort> Members

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

            // Check if myObject can be cast to IPPort
            var myIPPort = myObject as IPPort;
            if ((Object) myIPPort == null)
                throw new ArgumentException("Parameter myObject could not be casted to type IPPort!");

            return this.Equals(myIPPort);

        }

        #endregion

        #region Equals(myIPPort)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myElementId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(IPPort myIPPort)
        {

            // Check if myIPPort is null
            if (myIPPort == null)
                throw new ArgumentNullException("Parameter myIPPort must not be null!");

            return _IPPort.Equals(myIPPort._IPPort);

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
            return _IPPort;
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override String ToString()
        {
            return _IPPort.ToString();
        }

        #endregion

    }

}
