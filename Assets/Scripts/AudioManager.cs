using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] artifacts;

    [SerializeField]
    AudioSource click, purchase, theme;

    public void Click()
    {
        click.Play();
    }

    public void PurchaseClick()
    {
        purchase.Play();
    }
}
