using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleMenu : MonoBehaviour
{
    public GameObject BotaoSair;

    void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        BotaoSair.SetActive(true);
        #endif
    }

    public void JogarJogo()
    {
        StartCoroutine(MudarCena("game"));
    }

    IEnumerator MudarCena(string name)
    {
        yield return new WaitForSeconds(0.3f);
        //se o jogo estiver pausado como no game over, deve-se usar
        //o WaitForSecondsRealtime
        SceneManager.LoadScene(name);
    }

    public void SairJogo()
    {
        StartCoroutine(Sair());
    }

    IEnumerator Sair()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
