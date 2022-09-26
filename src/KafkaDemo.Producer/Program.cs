using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Net;

var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile($"appsettings.json");

var config = configuration.Build();

var producerConfig = new ProducerConfig
{
    BootstrapServers = config["KafkaBrokers"],
    ClientId = Dns.GetHostName(),
};

using (var p = new ProducerBuilder<Null, string>(producerConfig).Build())
{
    while (true)
    {
        Console.WriteLine("Introduzca el mensaje a enviar: ");
        var input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            p.Produce(config["Topic"], new Message<Null, string> { Value = input });

            p.Flush();
        }
    }
}