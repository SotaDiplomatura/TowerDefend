using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarUITorreta : MonoBehaviour
{
    GameController gameController;
    Torretas scripTorretas;

    int identificador;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        scripTorretas = transform.parent.GetComponent<Torretas>();
    }
    private void Update()
    {
        if (identificador != scripTorretas.identificador)
        {
            identificador = scripTorretas.identificador;
        }
    }
    void OnMouseDown()
    {
        gameController.mirarIdentificador = identificador;
        gameController.ActivarDesactivarPanelUpTorretas();
        print(identificador);
    }
}
