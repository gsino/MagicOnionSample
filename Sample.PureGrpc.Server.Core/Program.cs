using Grpc.Core;
using System;

namespace Sample.PureGrpc.Server.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //gRPCサーバーのAddress・Port設定
            var serverPort = new ServerPort("localhost", 1234, ServerCredentials.Insecure);

            var server = new Grpc.Core.Server()
            {
                Ports = { serverPort },
                Services =
                {
                    PureGrpc.Definitions.Calculator.BindService(new PureGrpc.Server.CalculatorImpl()),
                    PureGrpc.Definitions.Health.BindService(new PureGrpc.Server.HealthImpl()),
                }
            };

            server.Start();

            Console.ReadLine();
        }
    }
}
