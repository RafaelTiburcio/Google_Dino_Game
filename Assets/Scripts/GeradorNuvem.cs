
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorNuvem : MonoBehaviour
{

    public GameObject[] nuvemPrefabs;
    public float delayInicial;
    public float delayEntreNuvens;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("GerarNuvem", delayInicial, delayEntreNuvens);
    }

    private void GerarNuvem()
    {
        var quantidadeNuvens = nuvemPrefabs.Length;
        var indiceAleatorio = Random.Range(0, quantidadeNuvens);
        var nuvemPrefab = nuvemPrefabs[indiceAleatorio];
        Instantiate(nuvemPrefab, transform.position, Quaternion.identity);
    }
}
