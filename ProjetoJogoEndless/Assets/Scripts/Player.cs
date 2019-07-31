using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    
    [SerializeField]
    float speed;
    [SerializeField]
    float life;
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



    // Start is called before the first frame update
    void Start()
    {
        speed = 200;
        life = 3;
        sentidoDireita = false;
        toggleSentido();
        body = GetComponent<Rigidbody2D>();
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


}

