using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    Rigidbody2D bodyProjetil;

    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        bodyProjetil = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2(0, -1 * speed * Time.deltaTime);
        bodyProjetil.velocity = velocity;
    }


}
