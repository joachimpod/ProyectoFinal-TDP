using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControladorNivel : MonoBehaviour
{
    public GameObject controles;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            controles.SetActive(false);
        }
    }
    public void CargarNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }

    public void Salir()
    {
        Application.Quit(0);
    }

    public void Controles()
    {
        controles.SetActive(true);
    }
}

