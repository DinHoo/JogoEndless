using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    
    int pontosInicial = 0000;
    [SerializeField]
    public TextMeshProUGUI textoVida;
    [SerializeField]
    public TextMeshProUGUI textoPontos;

    [SerializeField]
    private int pontos;
    public int Pontos
    {
        get { return pontos; }

        set
        {
            if (value < 0)
            {
                pontos = pontosInicial; //game over acionar
            }
           else
                pontos = value;

            textoPontos.text = "Score: " + pontos.ToString("0000");
            
        }

    }




    // Start is called before the first frame update
    void Start()
    {
        Pontos = pontosInicial;


    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void updateVidaUI(int vida)
    {
        textoVida.text = "Vida: " + vida;
    }

}
