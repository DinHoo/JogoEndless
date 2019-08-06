using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    //////////////////////////////////////////////////////////////
    /// Queue
    Queue<Projetil> poolProjetils = new Queue<Projetil>();
    int poolMax = 15;

    [SerializeField]
    Transform[] arraySpawns;
    
    ////////////////////////////////////////////////////////////
    /// Prefab serializado
    [SerializeField]
    GameObject projPrefab;

    /////////////////////////
    /// Interface
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


    //////////////////////////////////////////////////
    /// Array pra função geradora de posições
    /// 
    int[] posicao;
    


    // Start is called before the first frame update
    void Start()
    {
        Pontos = pontosInicial; //zerar o score

        if (GameObject.FindGameObjectWithTag("Spawn")) // Achar os spawns
        {
            List<Transform> spawnPointProjetil = new List<Transform>();
            print("achou");
            foreach (Transform filho in GameObject.FindGameObjectWithTag("Spawn").transform)
            {
                spawnPointProjetil.Add(filho);
            }
            arraySpawns = spawnPointProjetil.ToArray();
        }


        spanwProjetil(4, geraPosicoes(4, arraySpawns), 80);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateVidaUI(int vida)
    {
        textoVida.text = "Vida: " + vida; //setar a vida
    }

    //////////////////////////////////////////////////////////////////////////////////////
    /// Projetil
    ///
    //Função principal
    public void spanwProjetil(int num, int[] posicoes, float velocidade)
    {
        if (num > posicoes.Length)
            return;
        for (int i = 0; i < posicoes.Length; i++)
        {
            spawnProjetil(posicoes[i], velocidade);
        }
    }

    void spawnProjetil(int id, float velocidade)
    {
        if(poolProjetils.Count > 0)
        {
            print("respawn pool");
            respawn(id, velocidade);
        }
        else
        {
            print("cria novo");
            Projetil p = Instantiate(projPrefab, arraySpawns[id].position, Quaternion.identity).GetComponent<Projetil>();
            p.SpeedP = velocidade;
        }
       
    }

    public void respawn(int id, float velocidade)
    {

        Projetil p = poolProjetils.Dequeue();
        p.transform.position = arraySpawns[id].position;
        p.SpeedP = velocidade;
        

    }



    public void addtopool(Projetil p)
    {
        if (poolProjetils.Count < poolMax)
        {
            p.transform.position = Vector3.up * 8000;
            poolProjetils.Enqueue(p);
        }
        else
            Destroy(p.gameObject);
    }

    ///////////////////////////////////////////////////////////////////////
    /// Gerador de posições
    /// Recebe um número pra quantidade de projetils a serem spawnados e um array de transform dos pontos de spawn
    /// Retorna um array de inteiros contendo as posiçoes
    /// exemplo: geraPosicoes(3, arraySpawns); é um array de transforme contendo as posiçoes de spawn
    int[] geraPosicoes(int quantidade, Transform[] spawns)
    {
        int[] posicoes = new int[quantidade]; /// cria um array de inteiros que vai receber as posições

        for (int i = 0; i < quantidade; i++) ///for pra encher o array com -1 pq ele inicia zerado e complica pois uso valor zero
        {
            posicoes[i] = -1;
        }

        int aux;
        for(int i = 0; i<quantidade; i++)  ///for pra preencher o array de inteiros com numeros de 0 a 5
        {
            aux = Random.Range(0, spawns.Length);
            print("antes do while" + aux);
            
            while(System.Array.Exists(posicoes, x => x == aux))  ///como os valores não podem repetir, caso o random encontra o mesmo valor ele entra no while pra gerar outro que ainda não está na lista
            {
                aux = Random.Range(0, spawns.Length);
                print("dentro do while"+ aux);
            }
            posicoes[i] = aux;
        }

        return posicoes;
    }
}
