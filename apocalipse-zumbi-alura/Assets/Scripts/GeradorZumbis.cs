using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    public float DistanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaDeZumbisVivos = 2;
    private int quantidadeDeZumbisVivos;
    public float tempoProximoAumentoDeDificuldade = 30;
    private float contadorDeAumentarDificuldade;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");

        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;

        for (int i = 0; i < quantidadeMaximaDeZumbisVivos; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > DistanciaDoJogadorParaGeracao;

        bool possoGerarZumbisPelaQuantidade = quantidadeDeZumbisVivos < quantidadeMaximaDeZumbisVivos;

        if (possoGerarZumbisPelaDistancia && possoGerarZumbisPelaQuantidade)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }    

        if(Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
        {
            quantidadeMaximaDeZumbisVivos++;
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while (colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        ControleZumbi zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControleZumbi>();
        zumbi.MeuGerador = this;

        quantidadeDeZumbisVivos++;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos()
    {
        quantidadeDeZumbisVivos--;
    }

}
