using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile($"appsettings.json");

var config = configuration.Build();

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = config["KafkaBrokers"],
    GroupId = config["ConsumerGroup"],
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
{
    consumer.Subscribe(config["Topic"]);

    var cancellationTokenSource = new CancellationTokenSource();
    Console.CancelKeyPress += (_, e) =>
    {
        e.Cancel = true;
        cancellationTokenSource.Cancel();
    };

    while (!cancellationTokenSource.IsCancellationRequested)
    {
        var consumeResult = consumer.Consume(cancellationTokenSource.Token);

        if (consumeResult != null)
        {
            Console.WriteLine($"[Message]: '{consumeResult.Message.Value}' [TopicPartitionOffset]: '{consumeResult.TopicPartitionOffset}' [TopicPartition]: {consumeResult.TopicPartition}.");
        }
    }
}