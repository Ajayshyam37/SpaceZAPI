namespace SpaceZAPI;

public class Communication
{
    public int spaceCraft_ID { get; set; }
    public string name { get; set; }
    public State state { get; set; }
    public CommunicationType commType { get; set; }
    public PayloadType payloadType { get; set; }
}

public class Telemetry
{
    public Guid id { get; set; }
    public string altitude { get; set; }
    public string longitude { get; set; }
    public string latitude { get; set; }
    public string temperature { get; set; }
    public string timeToOrbit { get; set; }
}

