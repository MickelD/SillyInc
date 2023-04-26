using System.Collections.Generic;

[System.Serializable]
public class Strike
{

    public int strikeDuration;
    public float reputationPenaltyPerDay;
    public Department[] affectedDeps;

    public Strike(int strikeDuration, float reputationPenaltyPerDay, Department[] affectedDeps)
    {
        this.strikeDuration = strikeDuration;
        this.reputationPenaltyPerDay = reputationPenaltyPerDay;
        this.affectedDeps = affectedDeps;
    }
}
