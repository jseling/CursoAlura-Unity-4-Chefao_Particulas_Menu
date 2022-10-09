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
        SceneManager.LoadScene("game");
    }

    public void SairJogo()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
