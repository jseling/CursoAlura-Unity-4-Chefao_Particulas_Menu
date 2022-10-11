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
        meurigidbody.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;

       // StartCoroutine(SumirCorpo());
    }

    //IEnumerator SumirCorpo()
    //{
    //    yield return new WaitForSeconds(0.5f);
        //se o jogo estiver pausado como no game over, deve-se usar
        //o WaitForSecondsRealtime
    //    meurigidbody.velocity = new Vector3(0, -100f, 0);
    //}
}
