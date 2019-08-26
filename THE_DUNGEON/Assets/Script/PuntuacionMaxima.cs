using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class PuntuacionMaxima : MonoBehaviour
{
    protected int puntuacionMaxima;
    protected StreamWriter sw;
    protected StreamReader sr;
    void Start()
    {
        crearArchivo();
        cargar();

    }


    public void guardar()
    {
        sw = new StreamWriter(Application.persistentDataPath + "/Puntajes.txt");
        sw.WriteLine(puntuacionMaxima);
        sw.Close();
    }

    public void crearArchivo()
    {
        if (!File.Exists(Application.persistentDataPath + "/Puntajes.txt"))
        {
            File.Create(Application.persistentDataPath + "/Puntajes.txt");
            guardar();
        }
    }

    public void cargar()
    {
        sr = new StreamReader(Application.persistentDataPath + "/Puntajes.txt");
        puntuacionMaxima = Int32.Parse(sr.ReadLine());
        sr.Close();
    }

    public void modificarPuntMax(Jugador jugador)
    {
        if (jugador.obtenerPuntos() >= puntuacionMaxima)
        {
            puntuacionMaxima = jugador.obtenerPuntos();
        }
    }

    public int obtenerPuntacionMax()
    {
        return puntuacionMaxima;
    }
}
