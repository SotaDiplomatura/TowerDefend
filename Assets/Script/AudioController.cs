using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    GameController gameController;
    public AudioMixer audioController;
    AudioSource audioGritos;

    public Slider sliderMusica;
    public Slider sliderMaster;
    public Slider sliderEfectos;
    [SerializeField]
    List<AudioClip> gritos = new List<AudioClip> { };

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audioGritos = transform.GetChild(0).GetComponent<AudioSource>();
        ValorMaster(sliderMaster.value);
        ValorMusica(sliderMusica.value);
        ValorEfectos(sliderEfectos.value);
    }
    private void Update()
    {
        if(gameController.pausa)
        {
            ValorMaster(sliderMaster.value);
            ValorMusica(sliderMusica.value);
            ValorEfectos(sliderEfectos.value);
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
    public void ValorEfectos(float efectosLevel)
    {
        audioController.SetFloat("Efectos", efectosLevel);
    }
    public void Gritar()
    {
        audioGritos.clip = gritos[Random.Range(0, gritos.Count)];
        audioGritos.pitch = Random.Range(0.3f, 1f);
        audioGritos.Play();
    }
}
