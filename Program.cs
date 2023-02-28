// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;

Console.WriteLine("Hello, World!");

var config = new ConsumerConfig
{
    GroupId = "gid-consumers",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
{
    consumer.Subscribe("testdata");
    while (true)
    {
        var bookingDetails = consumer.Consume();
        Console.WriteLine(bookingDetails.Message.Value);
    }
}

