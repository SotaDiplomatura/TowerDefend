using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioControles : MonoBehaviour
{
    GameController gameController;
    public AudioMixer audioController;

    public Slider sliderMusica;
    public Slider sliderMaster;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        ValorMaster(sliderMaster.value);
        ValorMusica(sliderMusica.value);
    }
    private void Update()
    {
        if(gameController.pausa)
        {
            ValorMaster(sliderMaster.value);
            ValorMusica(sliderMusica.value);
        }
    }
    public void ValorMusica(float musicaLvl)
    {
        audioController.SetFloat("Música", musicaLvl);
    }
    public void ValorMaster(float masterLevel)
    {
        audioController.SetFloat("Master", masterLevel);
    }
}
