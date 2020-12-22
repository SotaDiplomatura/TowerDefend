using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargaPuntuacionMaxima : MonoBehaviour
{
    [SerializeField]
    Text puntuacion;

    void Start()
    {
        puntuacion.text = PlayerPrefs.GetInt("puntos").ToString();
    }
}
