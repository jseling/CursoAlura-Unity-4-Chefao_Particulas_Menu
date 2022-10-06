using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoArma;
    public AudioClip SomDeTiro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bala, CanoArma.transform.position, CanoArma.transform.rotation);
            ControleAudio.instancia.PlayOneShot(SomDeTiro);
        }
    }
}
