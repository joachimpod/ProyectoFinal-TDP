using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{

    // Tiempo de precarga
    public float waitBeforePlay;

    protected Animator anim;
    protected Coroutine manager;
    protected bool cargado;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AuraStart()
    {
        manager = StartCoroutine(Manager());
        anim.Play("Aura_Idle");
    }

    public void AuraStop()
    {
        StopCoroutine(manager);
        anim.Play("Aura_Idle");
        cargado = false;
    }

    // Método para comprobar si ya hemos cargado suficiente
    public IEnumerator Manager()
    {
        yield return new WaitForSeconds(waitBeforePlay);
        anim.Play("Aura_Show");
        cargado = true;
    }

    // Método para comprobar si ya hemos cargado suficiente
    public bool IsLoaded()
    {
        return cargado;
    }
}