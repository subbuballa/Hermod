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

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Vanaheimr.Hermod.HTTP
{
    
    public class HTTPBasicAuthentication
    {

        #region Properties

        public String                  Username           { get; private set; }
        public String                  Password           { get; private set; }
        public HTTPAuthenticationTypes HTTPCredentialType { get; private set; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create the credentials based on a base64 encoded string which comes from a HTTP header Authentication:
        /// </summary>
        /// <param name="HTTPHeaderCredential"></param>
        public HTTPBasicAuthentication(String HTTPHeaderCredential)
        {

            #region Initial checks

            if (HTTPHeaderCredential.IsNullOrEmpty())
                throw new ArgumentNullException("HTTPHeaderCredential", "The given credential string must not be null or empty!");

            #endregion

            var splitted = HTTPHeaderCredential.Split(new[] { ' ' });

            if (splitted.IsNullOrEmpty())
                throw new ArgumentException("invalid credentials " + HTTPHeaderCredential);

            if (splitted[0].ToLower() == "basic")
            {

                HTTPCredentialType = HTTPAuthenticationTypes.Basic;
                var usernamePassword = splitted[1].FromBase64().Split(new[] { ':' });

                if (usernamePassword.IsNullOrEmpty())
                    throw new ArgumentException("invalid username/password " + splitted[1].FromBase64());

                Username = usernamePassword[0];
                Password = usernamePassword[1];

            }
            
            else
                throw new ArgumentException("invalid credentialType " + splitted[0]);

        }

        #endregion

    }

}
