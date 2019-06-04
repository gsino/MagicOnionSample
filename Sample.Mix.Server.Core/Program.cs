using Grpc.Core;
using MagicOnion.Server;
using System;
using System.Threading.Tasks;

namespace Sample.Mix.Server.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //gRPCサーバーのAddress・Port設定
            var serverPort = new ServerPort("localhost", 1234, ServerCredentials.Insecure);

            //ロガーとかの設定
            var magicOnionOptions = new MagicOnionOptions(isReturnExceptionStackTraceInErrorDetail: true)
            {
                //todo:settings
            };

            //サービスクラスの実装が別アセンブリの場合はアセンブリを指定する
            var searchAssembly = new[] { typeof(Sample.MagicOnion.Server.Calculator).Assembly };


            var server = new Grpc.Core.Server()
            {
                Ports = { serverPort },
                Services =
                {
                    //MagicOnionサービス
                    MagicOnionEngine.BuildServerServiceDefinition(searchAssembly, magicOnionOptions),

                    //PureGrpcサービス
                    PureGrpc.Definitions.Calculator.BindService(new Sample.PureGrpc.Server.CalculatorImpl()),
                    PureGrpc.Definitions.Health.BindService(new Sample.PureGrpc.Server.HealthImpl()),
                }
            };

            server.Start();

            Console.ReadLine();
        }
    }
}
