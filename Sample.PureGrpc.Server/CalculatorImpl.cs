using System;
using System.Threading.Tasks;
using Grpc.Core;
using Sample.PureGrpc.Definitions;

namespace Sample.PureGrpc.Server
{
    public class CalculatorImpl : Calculator.CalculatorBase
    {
        public override Task<SumResponse> Sum(SumRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SumResponse
            {
                Result = request.X + request.Y
            });
        }
    }
}
