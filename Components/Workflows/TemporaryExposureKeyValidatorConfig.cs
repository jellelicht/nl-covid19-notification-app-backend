﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using Microsoft.Extensions.Configuration;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Configuration;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Workflows
{
    public class TemporaryExposureKeyValidatorConfig : AppSettingsReader, ITemporaryExposureKeyValidatorConfig
    {
        public TemporaryExposureKeyValidatorConfig(IConfiguration config) : base(config)
        {
        }

        private const string Prefix = "Validation:AgWorkflowKey:";

        public int RollingPeriodMin => GetValueInt32(Prefix+nameof(RollingPeriodMin));
        public int RollingPeriodMax => GetValueInt32(Prefix + nameof(RollingPeriodMax));
        public int DailyKeyByteCount => GetValueInt32(Prefix + nameof(DailyKeyByteCount));
    }
}