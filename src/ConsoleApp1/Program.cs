using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Route256.MerchandiseService.Grpc;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            // создаем клиента
            var client = new MerchApiGrpc.MerchApiGrpcClient(channel);
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            // обмениваемся сообщениями с сервером
            var reply = await client.RequestMerchAsync(new RequestMerchRequest());
            Console.WriteLine("Ответ сервера: " + reply);
            Console.ReadKey();
        }
    }
}