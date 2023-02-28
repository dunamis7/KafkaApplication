using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProducerApplication.Models;

namespace ProducerApplication.Controllers;

[ApiController]
[Route("api/cars")]
public class CarsController :ControllerBase
{
    private readonly ProducerConfig _configuration;
    private readonly IConfiguration _config;

    public CarsController(ProducerConfig configuration, IConfiguration config)
    {
        _configuration = configuration;
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> BookCar([FromBody] CarDetails carDetails)
    {
        string serializedData = JsonConvert.SerializeObject(carDetails);

      // var topic = _config.GetValue<string>("TopicName");

      var config = new ProducerConfig()
      {
         BootstrapServers = "localhost:9092"
      };

      
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            await producer.ProduceAsync("testdata", new Message<Null, string> { Value = serializedData });
            producer.Flush(TimeSpan.FromSeconds(10));
            return Ok(true);
        }
    }

}