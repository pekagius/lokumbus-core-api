using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Options;

namespace Lokumbus.CoreAPI.Configuration.Bootstrapping;

/// <summary>
/// A class responsible for bootstrapping Kafka topics based on the provided settings.
/// </summary>
public class BootstrapKafka : IHostedService
{
    private readonly KafkaSettings _kafkaSettings;
    private readonly ILogger<BootstrapKafka> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BootstrapKafka"/> class.
    /// </summary>
    /// <param name="kafkaSettings">The Kafka settings.</param>
    /// <param name="logger">The logger.</param>
    public BootstrapKafka(IOptions<KafkaSettings> kafkaSettings, ILogger<BootstrapKafka> logger)
    {
        _kafkaSettings = kafkaSettings.Value;
        _logger = logger;
    }

    /// <summary>
    /// Starts the Kafka topic creation process.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Using Kafka Bootstrap Servers: {_kafkaSettings.BootstrapServers}");

        if (string.IsNullOrEmpty(_kafkaSettings.BootstrapServers))
        {
            _logger.LogError("Kafka BootstrapServers aren't configured.");
            return;
        }

        if (_kafkaSettings.Topics == null || _kafkaSettings.Topics.Count == 0)
        {
            _logger.LogWarning("No topics defined to create.");
            return;
        }

        var config = new AdminClientConfig { BootstrapServers = _kafkaSettings.BootstrapServers };
        using var adminClient = new AdminClientBuilder(config).Build();

        foreach (var topic in _kafkaSettings.Topics)
        {
            // Define the topic specification
            var topicSpec = new TopicSpecification
            {
                Name = topic,
                NumPartitions = _kafkaSettings.NumPartitions.GetValueOrDefault(1), // Default value 1
                ReplicationFactor = _kafkaSettings.ReplicationFactor.GetValueOrDefault(1) // Default value 1
            };

            try
            {
                var createTopicsOptions = new CreateTopicsOptions
                {
                    // Optional: Set timeouts or other options
                    RequestTimeout = TimeSpan.FromSeconds(10)
                };

                // Create the topic
                await adminClient.CreateTopicsAsync(new[] { topicSpec }, createTopicsOptions);
                _logger.LogInformation($"Topic '{topic}' created successfully.");
            }
            catch (CreateTopicsException e)
            {
                foreach (var result in e.Results)
                {
                    if (result.Error.Code == ErrorCode.TopicAlreadyExists)
                    {
                        _logger.LogInformation($"Topic '{result.Topic}' already exists.");
                    }
                    else
                    {
                        _logger.LogError($"Error creating topic '{result.Topic}': {result.Error.Reason}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"General error creating topic '{topic}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Stops the Kafka topic creation process.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}