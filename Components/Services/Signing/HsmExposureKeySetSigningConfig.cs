﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using Microsoft.Extensions.Configuration;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Configuration;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services.Signing
{
    public class HsmExposureKeySetSigningConfig : AppSettingsReader, IExposureKeySetSigningConfig
    {
        public HsmExposureKeySetSigningConfig(IConfiguration config, string? prefix = null) : base(config, prefix) { }

        public string Thumbprint => GetValue("ExposureKeySets:Signing:CertificateThumbprint");
    }
}