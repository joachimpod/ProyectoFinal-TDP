using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float visionRadius;
    public float attackRadius;
    public float speed;


    // Variables relacionadas con el ataque
    public GameObject rockPrefab;
    public float attackSpeed;
    protected bool attacking;

    // Variables relacionadas con la vida
    public int maxHp;
    protected int hp;


    // Variable para guardar al jugador
    protected GameObject player;

    // Variable para guardar la posición inicial
    protected Vector3 initialPosition;
    protected Vector3 target;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    protected Animator anim;
    protected Rigidbody2D rb2d;


    protected Vector3 forward;
    protected float distance;
    protected Vector3 dir;
    protected RaycastHit2D hit;


    protected Sprite vidaEnemigo;
    public Sprite vida3;
    public Sprite vida2;
    public Sprite vida1;

    public Jugador jugador;
    public int puntos;

    protected virtual void Start()
    {

        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        hp = maxHp;
        vidaEnemigo = vida3;
    }

    void Update()
    {
        if (hp > 0) {
            movimiento();
        }

    }

    private void movimiento()
    {

        // Por defecto nuestro target siempre será nuestra posición inicial
        target = initialPosition;

        // Comprobamos un Raycast del enemigo hasta el jugador
        hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            visionRadius,
            1 << LayerMask.NameToLayer("Default")
        );

        // debugear el Raycast
        forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }

        // Calculamos la distancia y dirección actual hasta el target
        distance = Vector3.Distance(target, transform.position);
        dir = (target - transform.position).normalized;
        
        if (target != initialPosition && distance <= attackRadius && !attacking)
        {
            atacar(dir);
        }
        else
        {
                if (!attacking)
                {
                    rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

                    // Al movernos establecemos la animación de movimiento
                    anim.speed = 1;
                    anim.SetFloat("movX", dir.x);
                    anim.SetFloat("movY", dir.y);
                    anim.SetBool("caminando", true);
                }
  
        }

        // Una última comprobación para evitar bugs forzando la posición inicial
        if (target == initialPosition && distance < 0.05f)
        {
            transform.position = initialPosition;
            anim.SetBool("caminando", false);
        }
        if (distance > visionRadius)
        {
            hp = maxHp;
            actualizarVida();
        }

            Debug.DrawLine(transform.position, target, Color.green);
    }
    protected void atacar(Vector3 dir)
    {
        anim.SetFloat("movX", dir.x);
        anim.SetFloat("movY", dir.y);
        anim.Play("Enemigo_Attack");

        StartCoroutine(Attack(attackSpeed));
    }
    
    // Podemos dibujar el radio de visión y ataque sobre la escena dibujando una esfera
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }

    IEnumerator Attack(float seconds)
    {

        attacking = true;  // Activamos la bandera
        // Si tenemos objetivo y el prefab es correcto creamos la roca
        if (target != initialPosition && rockPrefab != null)
        {
            Instantiate(rockPrefab, transform.position, transform.rotation);
            // Esperamos los segundos de turno antes de hacer otro ataque
            yield return new WaitForSeconds(seconds);
        }
        attacking = false; // Desactivamos la bandera
    }

   private void atacado()
    {
        --hp;
        actualizarVida();
    }

    protected virtual void actualizarVida()
    {
        if (hp == 3)
        {
            vidaEnemigo = vida3;
        }
        else
        {
            if (hp == 2)
            {
                vidaEnemigo = vida2;
            }
            else
            {
                if (hp == 1)
                {
                    vidaEnemigo = vida1;
                }
                else
                {
                    morir();
                }
            }
        }
    }

    protected virtual void morir()
    {
        anim.Play("Enemigo_Death");
        Destroy(GetComponent<Collider2D>());
        jugador.sumarPuntos(puntos);
    }

     void OnGUI()
    {
        if (hp > 0)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.DrawTexture(new Rect(pos.x - 40, Screen.height - pos.y - 40, vidaEnemigo.texture.width, vidaEnemigo.texture.height),
                            vidaEnemigo.texture);
        }
    }

}
