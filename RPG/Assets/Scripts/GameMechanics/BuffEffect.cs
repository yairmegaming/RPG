using UnityEngine;

public enum BuffTargetStat { Attack, Defense, Health }

public class BuffEffect
{
    public BuffTargetStat stat;
    public float multiplier;
    public int turnsLeft;

    public BuffEffect(BuffTargetStat stat, float multiplier, int turns)
    {
        this.stat = stat;
        this.multiplier = multiplier;
        this.turnsLeft = Mathf.Min(turns, 5); // Clamp to 5 turns max
    }
}