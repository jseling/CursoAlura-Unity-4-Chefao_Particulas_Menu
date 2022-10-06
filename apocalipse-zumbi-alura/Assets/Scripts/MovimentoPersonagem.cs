using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meurigidbody;

    void Awake()
    {
        meurigidbody = GetComponent<Rigidbody>();
    }
    public void Movimentar(Vector3 direcao, float velocidade)
    {
        direcao.Normalize();
        meurigidbody.MovePosition(
            meurigidbody.position +
            direcao * velocidade * Time.deltaTime);
    }

    public void Rotacionar(Vector3 direcao)
    {
        if (direcao != Vector3.zero)
        {
            direcao.Normalize();
            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            meurigidbody.MoveRotation(novaRotacao);
        }
    }

    public void Morrer()
    {
        meurigidbody.constraints = RigidbodyConstraints.None;
        meurigidbody.velocity = Vector3.zero;
        meurigidbody.isKinematic = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
