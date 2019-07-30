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
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(sentidox * speed * Time.deltaTime, body.velocity.y);
        body.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space))
            body.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
    }
}

