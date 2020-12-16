using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeOrdas : MonoBehaviour
{
    GameController gameController;
    int enemigosEnEscena;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
