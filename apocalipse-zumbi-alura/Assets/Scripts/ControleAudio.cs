using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleAudio : MonoBehaviour
{
    private AudioSource meuAudioSource;
    public static AudioSource instancia;

    private void Awake()
    {
        meuAudioSource = GetComponent<AudioSource>();
        instancia = meuAudioSource;
    }
}
