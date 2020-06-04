// Copyright � 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System;
using System.Globalization;
using System.Linq;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RivmAdvice
{
    public class RivmAdviceValidator
    {
        private readonly HardCodedRivmAdviceValidationConfig _Config = new HardCodedRivmAdviceValidationConfig();

        public static bool IsBase64(string value)
        {
            //Convert.TryFromBase64String(value, new Span<byte>(), out int _);

            if (string.IsNullOrWhiteSpace(value))
                return false;

            try
            {
                var _ = Convert.FromBase64String(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool CultureExists(string cultureName)
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => string.Equals(culture.Name, cultureName, StringComparison.CurrentCultureIgnoreCase));
        }

        private bool LocalValid(string name)
            => !string.IsNullOrWhiteSpace(name) && _Config.Locales.Any(x => string.Equals(name, x, StringComparison.CurrentCultureIgnoreCase));

        public bool Valid(MobileDeviceRivmAdviceArgs args)
        {
            if (args == null)
                return false;

            if (args.TemporaryExposureKeyRetentionDays < 1) //TODO range?
                return false;

            if (args.ObservedTemporaryExposureKeyRetentionDays < 1) //TODO range?
                return false;

            if (args.IsolationPeriodDays < 1) //TODO range?
                return false;

            if (args.Release.Year < 2020) //TODO range?
                return false;

            var locales = args.Text.Select(x => x.Locale).ToArray();

            if (locales.Any(x => !LocalValid(x)))
                return false;

            if (locales.Distinct().Count() != locales.Length)
                return false;

            return args.Text.All(Valid);
        }

        private bool Valid(LocalizableTextArgs args)
        {
            if (!IsBase64(args.IsolationAdviceLong))
                return false;

            if (!IsBase64(args.IsolationAdviceShort))
                return false;

            return true;
        }
    }
}
