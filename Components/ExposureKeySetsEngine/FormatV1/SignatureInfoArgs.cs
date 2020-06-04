﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.ExposureKeySetsEngine.FormatV1
{
    public class SignatureInfoArgs
    {
        public string AppBundleId { get; set; }
        public string AndroidPackage { get; set; }
        public string SignatureAlgorithm { get; set; }
        public string VerificationKeyId { get; set; }
        public string VerificationKeyVersion { get; set; }
    }
}