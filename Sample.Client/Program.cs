using Grpc.Core;
using MagicOnion.Client;
using Sample.MagicOnion.Definitions;
using System;
using System.Threading.Tasks;

namespace Sample.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await MagicOnionSum();

            await PureGrpcSum();

            await PureGrpcIsHealty();

            Console.ReadLine();
        }

        public static CallInvoker CreateCallInvoker()
        {
            var channel = new Channel("localhost", 1234, ChannelCredentials.Insecure);
            //return new DefaultCallInvoker(channel);
            return new LoggingCallInvoker(channel);
        }

        public static async Task MagicOnionSum()
        {
            var callInvoker = CreateCallInvoker();
            var calculator = MagicOnionClient.Create<ICalculator>(callInvoker);

            var result = await calculator.Sum(1, 2);

            Console.WriteLine("MagicOnionSum 1 + 2 = " + result);
        }

        public static async Task PureGrpcSum()
        {
            var callInvoker = CreateCallInvoker();
            var calculator = new Sample.PureGrpc.Definitions.Calculator.CalculatorClient(callInvoker);

            var result = await calculator.SumAsync(new PureGrpc.Definitions.SumRequest()
            {
                X = 3,
                Y = 4,
            });

            Console.WriteLine("PureGrpcSum 3 + 4 = " + result.Result);
        }

        public static async Task PureGrpcIsHealty()
        {
            var callInvoker = CreateCallInvoker();
            var health = new Sample.PureGrpc.Definitions.Health.HealthClient(callInvoker);

            var asyncUnaryCall = health.IsHealthyAsync(PureGrpc.Definitions.Empty.Default);

            try
            {
                await asyncUnaryCall;
            }
            catch(RpcException ex)
            {
                Console.WriteLine("PureGrpcIsHealty : false " + ex.Message);
            }
            Console.WriteLine("PureGrpcIsHealty : true");
        }

        /// <summary>
        /// UnaryCall実行時にログを吐く
        /// </summary>
        public class LoggingCallInvoker : DefaultCallInvoker
        {
            public LoggingCallInvoker(Channel channel) : base(channel) { }

            public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options, TRequest request)
            {
                Console.WriteLine($"UnaryCall {method.ServiceName}/{method.Name}");
                return base.AsyncUnaryCall(method, host, options, request);
            }
        }
    }
}
