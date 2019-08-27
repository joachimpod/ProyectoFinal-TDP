using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;

    protected GameObject player;   
    protected Rigidbody2D rb2d;    
    protected Vector3 target, dir; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();

        // Recuperamos posición del jugador y la dirección normalizada
        if (player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;
        }
    }

    void FixedUpdate()
    {
        // Si hay un objetivo movemos el proyectil hacia su posición
        if (target != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player" && col.tag != "ataque" && col.tag !="Enemigo")
        {
            Destroy(gameObject);
        }
        // Si chocamos contra el jugador o un ataque la borramos
        if (col.transform.tag == "Player" || col.transform.tag == "Attack")
        {

            if (col.tag == "Player")
            {
                col.SendMessage("atacado");
            }
            Destroy(gameObject);
        }
    }


    void OnBecameInvisible()
    {
        // Si se sale de la pantalla borramos el proyectil
        Destroy(gameObject);
    }
}
