using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public string nomeDaCena;

    public void ChangeScene()
    {
        SceneManager.LoadScene(nomeDaCena);
    }

    public void Sair()
    {
        Application.Quit();
    }

 }
