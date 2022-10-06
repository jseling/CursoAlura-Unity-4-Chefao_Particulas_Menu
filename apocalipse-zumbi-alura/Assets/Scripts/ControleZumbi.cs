using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleZumbi : MonoBehaviour, IMatavel
{
    public GameObject Jogador;
    private StatusInimigo statusInimigo;

    private ControleJogador compControleJogador;

    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;

    public AudioClip SomDeMorte;

    private Vector3 posicaoAleatoria;
    private Vector3 direcao;

    private float contadorVagar;
    public float DistanciaVagar = 10;

    private float porcentagemGerarKitMedico = 0.1f;
    public GameObject KitMedicoPrefab;
    private ControleInterface scriptControleInterface;

    [HideInInspector]
    public GeradorZumbis MeuGerador;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<StatusInimigo>();
        AleatorizarZumbi();
        scriptControleInterface = GameObject.FindObjectOfType(typeof(ControleInterface)) as ControleInterface;
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            Perseguir();
        }  
        else
        {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }  
    }

    void Perseguir()
    {
        direcao = Jogador.transform.position - transform.position;
        movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        animacaoInimigo.Atacar(false);
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += statusInimigo.TempoEntrePosicoesVagar + Random.Range(-1f, 1f);
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * DistanciaVagar;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 30);
        compControleJogador.TomarDano(dano);
    }

    void AleatorizarZumbi()
    {
        int tipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(tipoZumbi).gameObject.SetActive(true);
        compControleJogador = Jogador.GetComponent<ControleJogador>();
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if (statusInimigo.Vida <= 0)
        {
            Morrer();
        }

    }

    public void Morrer()
    {
        Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        movimentaInimigo.Morrer();
        this.enabled = false;
        ControleAudio.instancia.PlayOneShot(SomDeMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControleInterface.AtualizarQuantidadeDeZumbisMortos();
        MeuGerador.DiminuirQuantidadeDeZumbisVivos();
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
