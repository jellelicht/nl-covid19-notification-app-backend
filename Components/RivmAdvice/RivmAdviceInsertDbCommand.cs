// Copyright � 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.Threading.Tasks;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.EfDatabase;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Mapping;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RivmAdvice
{
    public class RivmAdviceInsertDbCommand
    {
        private readonly IDbContextProvider<ExposureContentDbContext>_DbConfig;
        private readonly IPublishingIdCreator _PublishingIdCreator;

        public RivmAdviceInsertDbCommand(IDbContextProvider<ExposureContentDbContext>dbConfig, IPublishingIdCreator publishingIdCreator)
        {
            _DbConfig = dbConfig;
            _PublishingIdCreator = publishingIdCreator;
        }

        public async Task Execute(MobileDeviceRivmAdviceArgs args)
        {
            var e = args.ToEntity();
            e.PublishingId = _PublishingIdCreator.Create(e);
            await _DbConfig.Current.AddAsync(e);
        }
    }
}