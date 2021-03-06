﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.ExposureKeySetsEngine
{
    public interface IExposureKeySetHeaderInfoConfig
    {
        string AppBundleId { get; }
        string AndroidPackage { get; }
        string VerificationKeyId { get; }
        string VerificationKeyVersion { get; }
    }
}