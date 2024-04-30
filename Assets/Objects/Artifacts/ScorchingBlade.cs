using UnityEngine;

public class ScorchingBlade : Artifact
{
    public override void Passive()
    {
        base.Passive();
    }

    public override void Active()
    {
        gameManager.bladeUses++;
        StartCoroutine(gameManager.playerManager.IncreaseAttack(false, 1.1f, 6));
        base.Active();
        artifactManager.DisableArtifact(GetType());
    }
}
