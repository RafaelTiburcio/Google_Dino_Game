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
        public AudioSource cemPontosSFX;
        public AudioSource FimDeJogoSFX;
        private bool estaNoChao;
        public float pontos;
        private float highscore;
        public float multiplicadorPontos = 1;
        public Text pontosText;
        public Text PontosHighscoreText;
        public Animator animatorComponent;


        private void Start()
        {
            highscore = PlayerPrefs.GetFloat("HIGHSCORE");
            // sistema de guardar uma informação, mesmo após o fechamento da aplicação.
            PontosHighscoreText.text = "Recorde: " + Mathf.FloorToInt(highscore).ToString();
        }

    // Update is called once per frame
    void Update()
    {
        pontos += Time.deltaTime * multiplicadorPontos;

        var pontosArredondado = Mathf.FloorToInt(pontos);
        pontosText.text = "Pontuação: " + pontosArredondado.ToString();

        if (pontosArredondado > 0 &&
            pontosArredondado % 100 == 0
                && !cemPontosSFX.isPlaying)
        {
            cemPontosSFX.Play();
        }

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
            if (pontos  > highscore)
            {
                highscore = pontos;

                PlayerPrefs.SetFloat("HIGHSCORE", highscore);
            }
        // condição de highscore

            FimDeJogoSFX.Play();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //variavel de gameover
        }
    }
}
