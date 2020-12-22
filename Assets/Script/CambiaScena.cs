using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaScena : MonoBehaviour
{
    public void Jugar()
    {
        if (PlayerPrefs.GetInt("puntos") <= 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void PasarAJuego()
    {
        SceneManager.LoadScene(2);
    }

    public void Salir()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
