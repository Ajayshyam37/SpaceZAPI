namespace SpaceZAPI;

public class SpaceCraft
{
    public int spaceCraft_ID { get; set; }
    public string name { get; set; }
    public State state { get; set; }
}

public enum State
{
    Waiting,
    Active,
    DeOrbited
}


