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

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("GerarEnemie", delayInicial, delayEntreEnemies);
    }

    private void GerarEnemie()
    {
        var dado = Random.Range(1, 7);

        if (dado <=2)
        {
            //gerar bird
            var posicaoYAleatoria = Random.Range(birdYMinimo, birdYMaximo);

            var posicao = new Vector3(
                transform.position.x,
                transform.position.y + posicaoYAleatoria,
                transform.position.z
            );
            Instantiate(birdPreFab, posicao, Quaternion.identity);
        }
        else
        {
            //gerar ground enemie
        var quantidadeEnemies = groundEnemiesPrefabs.Length;
        var indiceAleatorio = Random.Range(0, quantidadeEnemies);
        var groundEnemiesPrefab = groundEnemiesPrefabs[indiceAleatorio];
        Instantiate(groundEnemiesPrefab, transform.position, Quaternion.identity);

        }
    }
}
