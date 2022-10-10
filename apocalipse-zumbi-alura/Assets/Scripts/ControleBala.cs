using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleBala : MonoBehaviour
{
    public float Velocidade = 30;

    private Rigidbody compRigidBody;

    public AudioClip SomDeMorte;

    // Start is called before the first frame update
    void Start()
    {
        compRigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        compRigidBody.MovePosition
            (compRigidBody.position +
            transform.forward * Velocidade * Time.deltaTime);

    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion rotacaoOpostaBala = Quaternion.LookRotation(-transform.forward);
        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
                ControleZumbi inimigo = objetoDeColisao.GetComponent<ControleZumbi>();
                inimigo.TomarDano(1, transform.position, rotacaoOpostaBala);
                break;
            case "Chefe":
                ControleChefe chefe = objetoDeColisao.GetComponent<ControleChefe>();
                chefe.TomarDano(1, transform.position, rotacaoOpostaBala);
                break;
        }
        Destroy(gameObject);
    }    
}
