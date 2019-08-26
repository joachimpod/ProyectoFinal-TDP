using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EstadoJuego : MonoBehaviour
{
    public PuntuacionMaxima puntuacionMax;
    public Jugador jugador;

    public static bool juegoPausado= false;
    public GameObject menuPausa;

    protected int puntosJugador;
    void Start()
    {
        menuPausa.SetActive(false);
    }

    void Update()
    {
        puntosJugador = jugador.obtenerPuntos();
        puntuacionMax.modificarPuntMax(jugador);
        pasarEscena();
        pausa();
        Salir();
    }

    void pasarEscena()
    {
        if (jugador.getVida() == 0)
        {
            puntuacionMax.guardar();
            StartCoroutine(menuMuerte(1.5f));
        }
    }

    private IEnumerator menuMuerte(float seg)
    {
        yield return new WaitForSeconds(seg);
        SceneManager.LoadScene("Menu_Muerte");
    }

    void pausa()
    {
        if (Input.GetKeyDown("p"))
        {
            if (juegoPausado)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    void pause()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }
    public void resume()
    {

        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }
    public void Salir()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
            puntuacionMax.guardar();
        }
    }
    void OnGUI()
    {
        GUI.Box(new Rect(
                            400,
                            0,
                            200,
                            20), "ScoreMaximo:" + puntuacionMax.obtenerPuntacionMax()) ;

        GUI.Box(new Rect(
                            800,
                            0,
                            150,
                            20), "Score:" + puntosJugador);
    }
}
