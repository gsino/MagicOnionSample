using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Sample.PureGrpc.Definitions;

namespace Sample.PureGrpc.Server
{
    public class HealthImpl : Health.HealthBase
    {
        public override Task<Empty> IsHealthy(Empty request, ServerCallContext context)
        {
            return Task.FromResult(Empty.Default);
        }
    }
}

