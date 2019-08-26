using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public float speed = 1f;

    protected Animator anim;
    protected Rigidbody2D cuerpoRigido;
    protected Vector2 mov;


    protected AnimatorStateInfo stateInfoAtaque;
    protected bool atacando;

    protected AnimatorStateInfo stateInfoAtaqueCargado;
    protected bool cargando;
    protected float angulo;
    protected GameObject slashObj;
    protected Slash slash;

    protected Aura aura;

    public GameObject slashCargado;
    protected bool movePrevent;


    protected int maxHp = 5;
    protected int hp;

    public Sprite vidaJugador;
    public Sprite vida4;
    public Sprite vida3;
    public Sprite vida2;
    public Sprite vida1;
    public Sprite vida0;

    protected int score;


    CircleCollider2D attackcolider;
    float playbackTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        cuerpoRigido = GetComponent<Rigidbody2D>();

        attackcolider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackcolider.enabled = false;

        aura = transform.GetChild(1).GetComponent<Aura>();

        hp = maxHp;

    }

    void Update()
    {

        movimiento();
        animacion();
        atacar();
        ataqueCargado();
        prevenirMovimiento();
    }

    void movimiento()
    {
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
           );
    }

    void animacion()
    {

        if (mov != Vector2.zero)
        {
            //espadaAnimacion.SetFloat("movXEspada", mov.x);
            // espadaAnimacion.SetFloat("movYEspada", mov.y);

            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("caminando", true);
        }
        else
        {
            anim.SetBool("caminando", false);
        }
    }

    void atacar()
    {
        stateInfoAtaque = anim.GetCurrentAnimatorStateInfo(0);
        atacando = stateInfoAtaque.IsName("Jugador_Attack");

        if (Input.GetKeyDown("space") && !atacando)
        {
            anim.SetTrigger("atacando");
            movePrevent = true;
        }

        if (mov != Vector2.zero)
        {
            attackcolider.offset = new Vector2(mov.x * 0.1f, mov.y * 0.1f);
        }

        if (atacando)
        {
            playbackTime = stateInfoAtaque.normalizedTime;
            if (playbackTime > 0.33 && playbackTime < 0.66)
            {
                attackcolider.enabled = true;
            }
            else
            {
                attackcolider.enabled = false;
            }

            if (playbackTime > 0.8f)
             {
                    movePrevent = false;

            }

        }
    }

    void ataqueCargado()
    {
        stateInfoAtaqueCargado = anim.GetCurrentAnimatorStateInfo(0);
        atacando = stateInfoAtaqueCargado.IsName("Jugador_Slash");

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            anim.SetTrigger("cargando");
            movePrevent = true;
            aura.AuraStart();
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                anim.SetTrigger("atacando");
                if (aura.IsLoaded())
                {
                    angulo = Mathf.Atan2(anim.GetFloat("movY"), anim.GetFloat("movX")) * Mathf.Rad2Deg;
                    slashObj = Instantiate(slashCargado, transform.position, Quaternion.AngleAxis(angulo, Vector3.forward));
                    slash = slashObj.GetComponent<Slash>();
                    slash.mov.x = anim.GetFloat("movX");
                    slash.mov.y = anim.GetFloat("movY");
                }
                aura.AuraStop();
                StartCoroutine(habilitarMovimiento(0.2f));
            }
        }

    }
    void prevenirMovimiento()
    {
        if (movePrevent)
        {
            mov = Vector2.zero;
        }
    }

    IEnumerator habilitarMovimiento(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        movePrevent = false;
    }

    void FixedUpdate()
    {
        cuerpoRigido.MovePosition(cuerpoRigido.position + mov * speed * Time.deltaTime);
    }

    public void atacado()
    {
        --hp;
        actualizarVida();
    }
    public void actualizarVida()
    {
        if (hp == 4)
        {
            vidaJugador = vida4;
        }
        else
        {
            if (hp == 3)
            {
                vidaJugador = vida3;
            }
            else
            {
                if (hp == 2)
                {
                    vidaJugador = vida2;
                }
                else
                {
                    if (hp == 1)
                    {
                        vidaJugador = vida1;
                    }
                    else
                    {
                        vidaJugador = vida0;
                        muerte();
                    }
                }
            }
        }
    }
    private void muerte()
    {
        anim.Play("Jugador_Death");
        movePrevent = true;

    }


    public void sumarPuntos(int puntos)
    {
        score = score + puntos*hp;
    }


    public int getVida()
    {
        return hp;
    }
    public int obtenerPuntos()
    {
        return score;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(
                            0,
                            0,
                            vidaJugador.texture.width * 1.5f,
                            vidaJugador.texture.height * 1.5f),
                        vidaJugador.texture);
    }
}
