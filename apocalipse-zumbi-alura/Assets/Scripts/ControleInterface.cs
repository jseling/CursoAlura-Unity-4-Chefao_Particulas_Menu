using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControleInterface : MonoBehaviour
{
    private ControleJogador scriptControleJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoTempoDeSobrevivenciaMaximo;
    private float tempoPontuacaoSalvo;
    private int quantidadeDeZumbisMortos;
    public Text TextoQuantidadeDeZumbisMortos;

    // Start is called before the first frame update
    void Start()
    {
        scriptControleJogador = GameObject.FindWithTag("Jogador").GetComponent<ControleJogador>();

        SliderVidaJogador.maxValue = scriptControleJogador.StatusJogador.Vida;
        AtualizaVidaJogador();
        Time.timeScale = 1;

        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizaVidaJogador()
    {
        SliderVidaJogador.value = scriptControleJogador.StatusJogador.Vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos()
    {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeDeZumbisMortos.text = "x " + quantidadeDeZumbisMortos.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        PainelDeGameOver.SetActive(true);

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoTempoDeSobrevivencia.text = "Você sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima(int min, int seg)
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoTempoDeSobrevivenciaMaximo.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);

            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        if (TextoTempoDeSobrevivenciaMaximo.text == "")
        {
            min = (int)(tempoPontuacaoSalvo / 60);
            seg = (int)(tempoPontuacaoSalvo % 60);
            TextoTempoDeSobrevivenciaMaximo.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }
}
