using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField]
    Game game_ref;
    [SerializeField]
    float speed;
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
    [SerializeField]
    bool sentidoDireita;
    [SerializeField]
    float sentidox;
    [SerializeField]
    float jumpforce;
    [SerializeField]
    bool estaNoSoloOuPlataforma;
    [SerializeField]
    bool estaNaParede;

    int maxLife = 3;


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
        if (Input.GetKeyDown(KeyCode.F))
            toggleSentido();
        

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
            Vector2 velocity = new Vector2(sentidox * speed * Time.deltaTime, body.velocity.y);
            body.velocity = velocity;
        }

        if (Input.GetKeyDown(KeyCode.Space) && estaNoSoloOuPlataforma)
        {
            body.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            estaNoSoloOuPlataforma = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("ParedeLateral") && !estaNaParede)
        {
            estaNaParede = true;
            print("colidiu na parede");
        }

        if((collision.gameObject.CompareTag("Solo") || collision.gameObject.CompareTag("Plataforma")) && !estaNoSoloOuPlataforma)
        {
            estaNoSoloOuPlataforma = true;
            print("colidiu no solo ou plataforma");

        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projetil"))
        {
            print("tiro");
            Life--;
        }
    }

}

