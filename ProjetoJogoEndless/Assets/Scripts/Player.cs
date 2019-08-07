using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField]
    Game game_ref;
    ///////////////////////////////////////
    ///Vida
    int maxLife = 3;
    [SerializeField]
    private int life;
    public int Life
    {
        get { return life; }

        set
        {
            if (value < 0)
            {
                life = 0; //game over acionar
            }
            else if(value > maxLife)
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
    bool sentidoDireita;
    [SerializeField]
    float sentidox;
    [SerializeField]
    bool estaNaParede;
    ////////////////////////////////////////////////////
    ///velocidade
    [SerializeField]
    float aceleracao;
    [SerializeField]
    float velocidadeMax;
    Vector2 velocidade = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Life = maxLife;
        toggleSentido();
        body = GetComponent<Rigidbody2D>();

        if(!game_ref || game_ref == null)
        {
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F))
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

    void toggleSentido()
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
    }

    private void FixedUpdate()
    {
        if (!estaNaParede)
        {
            
            if (body.velocity.magnitude < velocidadeMax)
            {
                 velocidade += new Vector2 (sentidox * aceleracao, 0);
                 body.AddForce(velocidade, ForceMode2D.Impulse);
                 print(body.velocity.magnitude);

            }
            /*
           Vector2 velocity = new Vector2(sentidox * aceleracao * Time.deltaTime, body.velocity.y);
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

