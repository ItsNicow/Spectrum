using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource water, fire, fireExplosion, air, earth;
    public AudioSource monsterDeath, monsterDamage, monsterDamageCritical, monsterDamageWeak;

    public AudioSource click, purchase;
    public AudioSource[] themes;
    public Dictionary<AudioSource, float> maxVolumes = new();

    void Awake()
    {
        foreach (AudioSource theme in themes)
        {
            maxVolumes[theme] = theme.volume;
        }
    }

    public void Click()
    {
        click.Play();
    }

    public void PurchaseClick()
    {
        purchase.Play();
    }

    public IEnumerator FadeSound(AudioSource sound, string inOut, float speed, bool start = false, bool end = false)
    {
        if (start) sound.Play();

        float volume = maxVolumes[sound];

        if (inOut == "in")
        {
            for (float i = 0; i <= 1; i += Time.deltaTime * speed)
            {
                sound.volume = volume * i;
                yield return null;
            }
            yield return sound.volume = volume;
        }
        else if (inOut == "out")
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
            {
                sound.volume = volume * i;
                yield return null;
            }
            yield return sound.volume = 0;
        }

        if (end) sound.Stop();
    }
}
