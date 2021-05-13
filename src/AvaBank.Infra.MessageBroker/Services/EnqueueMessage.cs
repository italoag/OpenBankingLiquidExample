using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Messaging.Azure;
using Liquid.Messaging.Azure.Attributes;
using Liquid.Messaging.Configuration;
using Microsoft.Extensions.Logging;
using AvaBank.Domain.Messages.Publishers;
using System.Collections.Generic;

namespace AvaBank.Infra.MessageBroker.Services
{
    [ServiceBusProducer("AvaBank", "openbankingtopic")]
    public class EnqueueMessage : ServiceBusProducer<AccountAproval>
    {
        public EnqueueMessage(ILightContextFactory contextFactory,
            ILightTelemetryFactory telemetryFactory,
            ILoggerFactory loggerFactory,
            ILightConfiguration<List<MessagingSettings>> messagingSettings)
            : base(contextFactory,
                  telemetryFactory,
                  loggerFactory,
                  messagingSettings)
        {
        }
    }
}
