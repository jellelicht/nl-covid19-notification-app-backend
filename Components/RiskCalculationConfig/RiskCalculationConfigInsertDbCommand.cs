// Copyright � 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.Threading.Tasks;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.EfDatabase;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Mapping;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RiskCalculationConfig
{
    public class RiskCalculationConfigInsertDbCommand
    {
        private readonly IDbContextProvider<ExposureContentDbContext>_DbContextProvider;
        private readonly IPublishingIdCreator _PublishingIdCreator;

        public RiskCalculationConfigInsertDbCommand(IDbContextProvider<ExposureContentDbContext>contextProvider, IPublishingIdCreator publishingIdCreator)
        {
            _DbContextProvider = contextProvider;
            _PublishingIdCreator = publishingIdCreator;
        }

        public async Task Execute(RiskCalculationConfigArgs args)
        {
            var e = args.ToEntity();
            e.PublishingId = _PublishingIdCreator.Create(e);
            _DbContextProvider.Current.Add(e);
        }
    }
}