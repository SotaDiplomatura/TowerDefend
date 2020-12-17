using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeOrdas : MonoBehaviour
{
    GameController gameController;
    [SerializeField]
    GameObject[] enemigosFuego;
    [SerializeField]
    GameObject[] enemigosAgua;
    [SerializeField]
    GameObject enemigosPlanta;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
