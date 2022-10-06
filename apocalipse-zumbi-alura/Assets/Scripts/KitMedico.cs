using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeDeCura = 15;
    private int tempoDeDestruicao = 10;

    private void Start()
    {
        Destroy(gameObject, tempoDeDestruicao);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Jogador")
        {
            other.GetComponent<ControleJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Vector3 axis = new Vector3(0f, 1f, 0f);
        transform.Rotate(axis, Time.deltaTime * 30);
    }
}
