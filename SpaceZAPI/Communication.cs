﻿namespace SpaceZAPI;

public class Communication
{
    public int spaceCraft_ID { get; set; }
    public string name { get; set; }
    public State state { get; set; }
    public CommunicationType commType { get; set; }
    public State payloadState { get; set; }
    public Guid payLoad_ID { get; set; }
    public PayloadType payloadType { get; set; }
    public Boolean telemetry { get; set; }
    public Boolean payloadData { get; set; }
    public double orbitRadius { get; set; }
    public double totalTimeToOrbit { get; set; }
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

