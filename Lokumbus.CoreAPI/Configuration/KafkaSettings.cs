namespace Lokumbus.CoreAPI.Configuration;

public class KafkaSettings
{
    public string? BootstrapServers { get; set; }
    public string? GroupId { get; set; }
    public int? NumPartitions { get; set; }
    public short? ReplicationFactor { get; set; }


    public List<string?>? Topics { get; set; }
    public string? UserEventsTopic { get; set; }
}