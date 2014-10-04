﻿//-----------------------------------------------------------------------------
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//
//-----------------------------------------------------------------------------

using System;
using System.Security.Cryptography.X509Certificates;

namespace Vvs.Infraestrutura.Security.Cryptography
{
    /// <summary>
    /// A utility class which helps to retrieve an x509 certificate
    /// </summary>
    public class CertificateUtil
    {
        public static X509Certificate2 GetCertificate(StoreName name, StoreLocation location, string subjectName)
        {
            var store = new X509Store(name, location);
            X509Certificate2Collection certificates = null;
            store.Open(OpenFlags.ReadOnly);

            try
            {
                X509Certificate2 result = null;

                //
                // Every time we call store.Certificates property, a new collection will be returned.
                //
                certificates = store.Certificates;

                foreach (var cert in certificates)
                {
                    if (cert.SubjectName.Name != null && cert.SubjectName.Name.Equals(subjectName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (result != null) throw new ApplicationException(string.Format("There are multiple certificates for subject Name {0}", subjectName));
                        result = new X509Certificate2(cert);
                    }
                }

                if (result == null)
                    throw new ApplicationException(string.Format("No certificate was found for subject Name {0}", subjectName));

                return result;
            }
            finally
            {
                if (certificates != null)
                {
                    foreach (X509Certificate2 cert in certificates)
                    {
                        cert.Reset();
                    }
                }

                store.Close();
            }
        }
    }
}
