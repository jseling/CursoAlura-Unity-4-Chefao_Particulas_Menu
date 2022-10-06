using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControleJogador : MonoBehaviour, IMatavel, ICuravel
{
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    private Vector3 direcao;
    public ControleInterface scriptControleInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem meuAnimacaoJogador;
    public Status StatusJogador;

    private void Start()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        meuAnimacaoJogador = GetComponent<AnimacaoPersonagem>();
        StatusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
            direcao.Normalize();

        meuAnimacaoJogador.Movimentar(direcao.magnitude);
    }

    void FixedUpdate()
    {
            meuMovimentoJogador.Movimentar(direcao, StatusJogador.Velocidade);
            meuMovimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int _dano)
    {
        StatusJogador.Vida -= _dano;
        scriptControleInterface.AtualizaVidaJogador();
        ControleAudio.instancia.PlayOneShot(SomDeDano);

        if (StatusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {        
        scriptControleInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        StatusJogador.Vida += quantidadeDeCura;

        if (StatusJogador.Vida > StatusJogador.VidaInicial)
        {
            StatusJogador.Vida = StatusJogador.VidaInicial;
        }


        scriptControleInterface.AtualizaVidaJogador();
    }
}
