// Copyright � 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.Linq;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.EfDatabase;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RiskCalculationConfig
{

    public class SafeGetRiskCalculationConfigDbCommand
    {
        private readonly IDbContextProvider<ExposureContentDbContext>_DbContextProvider;

        public SafeGetRiskCalculationConfigDbCommand(IDbContextProvider<ExposureContentDbContext>contextProvider)
        {
            _DbContextProvider = contextProvider;
        }

        public RiskCalculationContentEntity Execute(string id)
        {
            return _DbContextProvider.Current.Set<RiskCalculationContentEntity>()
                .Where(x => x.PublishingId == id)
                .Take(1)
                .ToArray() //TODO sql might let me drop this.
                .SingleOrDefault();
        }
    }
}