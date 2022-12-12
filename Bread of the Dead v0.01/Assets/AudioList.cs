using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioList : MonoBehaviour
{
     private AudioSource source;
     public AudioClip Inicio;
     public AudioClip Fin;
     public AudioClip Atole;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(Inicio);
    }

    public void llegoElAtole(){
        source.PlayOneShot(Atole);
    }

    public void finDelJuego(){
        source.PlayOneShot(Fin);
    }
}
