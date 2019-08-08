using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField]
    private Game game_ref;

    ///////////////////////////////////////
    ///Vida
    private int maxLife = 3;

    [SerializeField]
    private int life;

    public int Life
    {
        get { return life; }

        set
        {
            if (value <= 0)
            {
                life = 0;
                game_ref.retrybundaFunk();//game over acionar
                game_ref.vivo = false;
            }
            else if (value > maxLife)
            {
                life = maxLife;
            }
            else
                life = value;
            game_ref.updateVidaUI(life);
        }
    }

    //////////////////////////////////////////////////
    ///mudar sentido
    [SerializeField]
    private bool sentidoDireita;

    [SerializeField]
    private float sentidox;

    [SerializeField]
    private bool estaNaParede;

    ////////////////////////////////////////////////////
    ///velocidade
    [SerializeField]
    private float aceleracao;

    [SerializeField]
    private float velocidadeMax;

    [SerializeField]
    private float intervaloVel;

    [SerializeField]
    private float intervaloVel2;

    private float tempo;
    public Vector2 velocidade = new Vector2(0, 0);

    // Start is called before the first frame update
    private void Start()
    {
        Life = maxLife;
        toggleSentido();
        body = GetComponent<Rigidbody2D>();

        tempo = Time.time;

        if (!game_ref || game_ref == null)
        {
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F) && game_ref.vivo)
            toggleSentido();
#endif

#if UNITY_ANDROID
        if (Input.touches.Length == 1)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
                toggleSentido();
        }

#endif
    }

    private void toggleSentido()
    {
        sentidoDireita = !sentidoDireita;
        sentidox = (sentidoDireita) ? 1 : -1;
        /*if (sentidoDireita)
        {
            sentidox = 1;
        }
        else
            sentidox = -1;
            */
        estaNaParede = false;
        //Vector2 velocity5 = new Vector2(sentidox * (0) * Time.deltaTime, 0);
        //body.velocity = new Vector2(0, 0);
        tempo = Time.time;
    }

    private void FixedUpdate()
    {
        if (!estaNaParede)
        {
            if (Time.time < tempo + intervaloVel)
            {
                Vector2 velocity1 = new Vector2(sentidox * (0) * Time.deltaTime, 0);
                body.velocity = velocity1;
                print("aceleracao nula " + body.velocity.magnitude);
            }
            else if (Time.time >= tempo + intervaloVel && Time.time <= tempo + intervaloVel2)
            {
                Vector2 velocity2 = new Vector2(sentidox * (aceleracao / 2) * Time.deltaTime, 0);
                body.velocity = velocity2;
                print("aceleracao reduzida " + body.velocity.magnitude);
            }
            else
            {
                Vector2 velocitye = new Vector2(sentidox * (aceleracao) * Time.deltaTime, 0);
                body.velocity = velocitye;
                print("aceleracao total" + body.velocity.magnitude);
            }

            /*
           Vector2 velocity = new Vector2(sentidox * aceleracao * Time.deltaTime, 0);
           body.AddForce(velocity, ForceMode2D.Impulse);
           */
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ParedeLateral") && !estaNaParede)
        {
            estaNaParede = true;
            //print("colidiu na parede");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projetil"))
        {
            //print("tiro");
            Life--;
        }
    }
}