using UnityEngine;

public class TearOfTheLake : Artifact
{
    public override void Passive()
    {
        base.Passive();
    }

    public override void Active()
    {
        gameManager.tearUses++;
        artifactManager.ReduceCooldowns(true, true, 2f);
        base.Active();
        artifactManager.DisableArtifact(GetType());
    }
}
