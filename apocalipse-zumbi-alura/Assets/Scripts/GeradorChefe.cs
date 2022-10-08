using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    private ControleInterface scriptControleInterface;
    public Transform[] PosicoesPossiveisGeracao;
    private Transform jogador;

    // Start is called before the first frame update
    void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        //scriptControleInterface = GameObject.FindObjectOfType<ControleInterface>();
        scriptControleInterface = GameObject.FindObjectOfType(typeof(ControleInterface)) as ControleInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Vector3 posicaoCriacao = CalcularPosicaoMaisDistante();
            Instantiate(ChefePrefab, posicaoCriacao, Quaternion.identity);
            scriptControleInterface.AparecerTextoChefeCriado();
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2);
    }

    Vector3 CalcularPosicaoMaisDistante()
    {
        Vector3 posicaoDeMaisDistancia = Vector3.zero;

        float maiorDistanciaJogador = 0;
        foreach (Transform posicao in PosicoesPossiveisGeracao)
        {
            float distanciaJogador = Vector3.Distance(posicao.position, jogador.position);
            if (distanciaJogador > maiorDistanciaJogador)
            {
                posicaoDeMaisDistancia = posicao.position;
                maiorDistanciaJogador = distanciaJogador;
            }
        }

        return posicaoDeMaisDistancia;
    }
}
