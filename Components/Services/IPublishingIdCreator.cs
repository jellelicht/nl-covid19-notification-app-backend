﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Manifest;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RivmAdvice;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RiskCalculationConfig;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services
{
    public interface IPublishingIdCreator
    {
        string Create(ManifestEntity e);
        string Create(RiskCalculationContentEntity e);
        string Create(ExposureKeySetContentEntity e);
        string Create(RivmAdviceContentEntity e);
    }
}