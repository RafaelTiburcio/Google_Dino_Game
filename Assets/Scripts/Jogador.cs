using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
        public Rigidbody2D rb;
        public float forcaPulo;
        public LayerMask layerChao;
        public float distanciaMinimaChao = 1;
        public AudioSource jumpSFX;
        private bool estaNoChao;
        private float pontos;
        public float multiplicadorPontos = 1;
        public Text pontosText;
        public Animator animatorComponent;

    // Update is called once per frame
    void Update()
    {
        pontos += Time.deltaTime * multiplicadorPontos;

        pontosText.text = "Pontuação: " + Mathf.FloorToInt(pontos).ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
             Pular();

            jumpSFX.Play();

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Abaixar();

        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Levantar();
        }
    }


    void Pular()
    {
        if (estaNoChao)
        {
        rb.AddForce(Vector2.up * forcaPulo);
        }
    }

    void Abaixar()
    {
        animatorComponent.SetBool("Abaixado", true);
    }

    void Levantar()
    {
        animatorComponent.SetBool("Abaixado", false);
    }


    private void FixedUpdate()
    {
        estaNoChao = Physics2D.Raycast(transform.position, Vector2.down, distanciaMinimaChao, layerChao);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
