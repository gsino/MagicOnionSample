using Grpc.Core;
using MagicOnion.Hosting;
using MagicOnion.Server;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Sample.MagicOnion.Core
{
    class Program
    {
        static async Task Main(string[] _)
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

            //MagicOnion.Hostingを使用する場合
            {
                await MagicOnionHost.CreateDefaultBuilder()
                  .UseMagicOnion(searchAssembly, magicOnionOptions, serverPort)
                  .RunConsoleAsync();
            }

            //自前でgRPC.Core.Serverを実行する場合
            {
                var server = new Grpc.Core.Server()
                {
                    Ports = { serverPort },
                    Services = { MagicOnionEngine.BuildServerServiceDefinition(searchAssembly, magicOnionOptions) }
                };

                server.Start();

                Console.ReadLine();
            }
        }

    }
}
