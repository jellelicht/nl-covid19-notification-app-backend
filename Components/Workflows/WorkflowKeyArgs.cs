// Copyright � 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Workflows
{

    /// <summary>
    /// TODO the WHOLE thing is a TEK? Or a Daily key?
    /// </summary>
    public class WorkflowKeyArgs
    {
        /// <summary>
        /// TemporaryExposureKey
        /// The 'ephemeral key' is called the RollingProximityIdentifier
        /// </summary>
        public string KeyData { get; set; }
        /// <summary>
        /// Equivalent of Dp3T Epoch
        /// </summary>
        public int RollingStartNumber { get; set; }
        /// <summary>
        /// Number of epochs in 24hours
        /// Currently fixed at 144? e.g. 10mins
        /// </summary>
        public int RollingPeriod { get; set; }
        /// <summary>
        /// Yet to be well defined.
        /// Self-diagnosis support?
        /// Comes from server?
        /// Phone can transmit risk level.
        /// 1-8 or 0-100?
        /// </summary>
        public int TransmissionRiskLevel { get; set; }

    }
}