// Copyright � 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using Microsoft.Extensions.Configuration;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Configuration;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.AgProtocolSettings
{
    public class AgConfigAppSettings : AppSettingsReader, IAgConfig
    {
        public AgConfigAppSettings(IConfiguration config) : base(config) { }

        public int ExposureKeySetCapacity => GetValueInt32("Ag:ExposureKeySet:Capacity", 21);
        public double ManifestLifeTimeHours { get; set; }
        public int ExposureKeySetLifetimeDays => GetValueInt32("Ag:ExposureKeySet:LifetimeDays", 21);
        public int WorkflowSecretLifetimeDays => GetValueInt32("Ag:Workflow:LifetimeDays", 12);
    }
}