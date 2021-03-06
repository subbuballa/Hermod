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
using System.Text;

#endregion

namespace de.ahzf.Vanaheimr.Hermod.Datastructures
{

    /// <summary>
    /// Networking address helpers.
    /// </summary>    
    public static class NetworkingHelpers
    {

        private static short SwapShort(short number)
        {
            return (short)(((number >> 8) & 0xFF) | ((number << 8) & 0xFF00));
        }

        private static int SwapInt(int number)
        {
            return (((number >> 24) & 0xFF)
                | ((number >> 08) & 0xFF00)
                | ((number << 08) & 0xFF0000)
                | ((number << 24)));
        }

        private static long SwapLong(long number)
        {
            return (((number >> 56) & 0xFF)
                | ((number >> 40) & 0xFF00)
                | ((number >> 24) & 0xFF0000)
                | ((number >> 08) & 0xFF000000)
                | ((number << 08) & 0xFF00000000)
                | ((number << 24) & 0xFF0000000000)
                | ((number << 40) & 0xFF000000000000)
                | ((number << 56)));
        }

        public static short HostToNetworkOrder(short host)
        {
            if (!BitConverter.IsLittleEndian)
                return (host);

            return SwapShort(host);
        }

        public static int HostToNetworkOrder(int host)
        {
            if (!BitConverter.IsLittleEndian)
                return (host);

            return SwapInt(host);
        }

        public static long HostToNetworkOrder(long host)
        {
            if (!BitConverter.IsLittleEndian)
                return (host);

            return SwapLong(host);
        }

        public static short NetworkToHostOrder(short network)
        {
            if (!BitConverter.IsLittleEndian)
                return (network);

            return SwapShort(network);
        }

        public static int NetworkToHostOrder(int network)
        {
            if (!BitConverter.IsLittleEndian)
                return (network);

            return SwapInt(network);
        }

        public static long NetworkToHostOrder(long network)
        {
            if (!BitConverter.IsLittleEndian)
                return (network);

            return SwapLong(network);
        }

    }

}
