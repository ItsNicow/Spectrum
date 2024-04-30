using UnityEngine;

public class ZephyrsRing : Artifact
{
    public override void Passive()
    {
        base.Passive();
    }

    public override void Active()
    {
        gameManager.ringUses++;
        foreach (Monster monster in gameManager.monsters)
        {
            StartCoroutine(monster.DecreaseSpeed(false, 0.8f, 8));
        }
        base.Active();
        artifactManager.DisableArtifact(GetType());
    }
}
