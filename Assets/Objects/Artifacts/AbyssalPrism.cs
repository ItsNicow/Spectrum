using UnityEngine;

public class AbyssalPrism : Artifact
{
    public override void Passive()
    {
        base.Passive();
    }

    public override void Active()
    {
        gameManager.prismUses++;
        gameManager.playerManager.Heal(0.05f * gameManager.playerManager.baseHealth);
        StartCoroutine(gameManager.playerManager.IncreaseHealth(true, 50, 10));
        base.Active();
        artifactManager.DisableArtifact(GetType());
    }
}
