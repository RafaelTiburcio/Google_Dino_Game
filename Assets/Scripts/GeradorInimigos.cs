using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigos : MonoBehaviour
{

    public GameObject[] groundEnemiesPrefabs;
    public GameObject birdPreFab;
    public float birdYMinimo = -1;
    public float birdYMaximo = 1;
    public float delayInicial;
    public float delayEntreEnemies;
    public float distanciaMinima = 4;
    public float distanciaMaxima = 8;
    public float birdSpawnPontuacaoMinima = 300;
    public Jogador jogadorScript;

    // Start is called before the first frame update
    private void Start()
    {
        //InvokeRepeating("GerarEnemie", delayInicial, delayEntreEnemies); não é mais necessaria, por causa do tempo.
        StartCoroutine(GerarEnemie());
    }


    private IEnumerator GerarEnemie()
    {
        yield return new WaitForSeconds(delayInicial);

        GameObject ultimoInimigoGerado = null;

        var distanciaNecessaria = 0f;

        while(true)
        {
                    var geracaoObjetoLiberada = ultimoInimigoGerado == null
                    || Vector3.Distance(transform.position, ultimoInimigoGerado.transform.position)
                    >= distanciaNecessaria;

                if (geracaoObjetoLiberada)
                {
                    var dado = Random.Range(1, 7);

               if (jogadorScript.pontos >= birdSpawnPontuacaoMinima && dado <=2)
                                    {
                                        //gerar bird
                    var posicaoYAleatoria = Random.Range(birdYMinimo, birdYMaximo);

                    var posicao = new Vector3(
                         transform.position.x,
                        transform.position.y + posicaoYAleatoria,
                        transform.position.z
                        );
                    ultimoInimigoGerado = Instantiate(birdPreFab, posicao, Quaternion.identity);
             }
                else
               {
                    //gerar ground enemie
                    var quantidadeEnemies = groundEnemiesPrefabs.Length;
                    var indiceAleatorio = Random.Range(0, quantidadeEnemies);
                    var groundEnemiesPrefab = groundEnemiesPrefabs[indiceAleatorio];
                    ultimoInimigoGerado = Instantiate(groundEnemiesPrefab, transform.position, Quaternion.identity);
               }

                    distanciaNecessaria = Random.Range(distanciaMinima, distanciaMaxima);
        }

                yield return null;
               // yield return new WaitForSeconds(delayEntreEnemies);
        }
    }
}
