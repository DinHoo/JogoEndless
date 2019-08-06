using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    Rigidbody2D bodyProjetil;

    Game gameRef;

    public static float speedP_inicial = 80;
    [SerializeField]
    private float speedP;
    public float SpeedP
    {
        get { return speedP; }

        set
        {
            if (value < speedP_inicial)
            {
                speedP = speedP_inicial; //game over acionar
            }
            else
                speedP = value;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        bodyProjetil = GetComponent<Rigidbody2D>();
        

        if (!gameRef || gameRef == null)
            gameRef = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2(0, -1 * SpeedP * Time.deltaTime);
        bodyProjetil.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Solo"))
        {
            gameRef.addtopool(this);
            gameRef.Pontos++;
        }
    }
}
