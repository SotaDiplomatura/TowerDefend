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
        scripTorretas = GetComponentInParent<Torretas>().GetComponent<Torretas>();
        identificador = scripTorretas.identificador;
    }

    void OnMouseDown()
    {
        gameController.ActivarDesactivarPanelUpTorretas();
        gameController.identificadorDeTorretas = identificador;
    }
}
