using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemigo
{
    public Sprite vida10;
    public Sprite vida9;
    public Sprite vida8;
    public Sprite vida7;
    public Sprite vida6;
    public Sprite vida5;
    public Sprite vida4;

    public GameObject portal;

    protected override void Start()
    {
        base.Start();
        vidaEnemigo = vida10;
        portal.SetActive(false);
    }
    void OnGUI()
    {
        if (hp > 0)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.DrawTexture(new Rect(pos.x - vidaEnemigo.texture.width/2, Screen.height - pos.y - 100, vidaEnemigo.texture.width, vidaEnemigo.texture.height),
                            vidaEnemigo.texture);
        }
    }

    protected override void morir()
    {
        base.morir();
        portal.SetActive(true);
    }

    protected override void actualizarVida()
    {
        if (hp == 10)
        {
            vidaEnemigo = vida10;
        }
        else
        {
            if (hp == 9)
            {
                vidaEnemigo = vida9;
            }
            else
            {
                if (hp == 8)
                {
                    vidaEnemigo = vida8;
                }
                else
                {
                    if (hp == 7)
                    {
                        vidaEnemigo = vida7;
                    }
                    else
                    {
                        if (hp == 6)
                        {
                            vidaEnemigo = vida6;
                        }
                        else
                        {
                            if (hp == 5)
                            {
                                vidaEnemigo = vida5;
                            }
                            else
                            {
                                if (hp == 4)
                                {
                                    vidaEnemigo = vida4;
                                }
                                else
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
                            }
                        }

                    }
                }
            }
        }



    }
}
