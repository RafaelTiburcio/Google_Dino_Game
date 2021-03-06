using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

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
        public int pulosExtras = 1;
        public GameObject restartButton;
        public AudioSource backgroundMusic;
        public GameObject sairButton;




        private void Start()
        {
            highscore = PlayerPrefs.GetFloat("HIGHSCORE");
            // sistema de guardar uma informação, mesmo após o fechamento da aplicação.
            PontosHighscoreText.text = "HIGHSCORE: " + Mathf.FloorToInt(highscore).ToString();
        }

    // Update is called once per frame
    void Update()
    {
        pontos += Time.deltaTime * multiplicadorPontos;

        var pontosArredondado = Mathf.FloorToInt(pontos);
        pontosText.text = "SCORE: " + pontosArredondado.ToString();

        if (pontosArredondado > 0 &&
            pontosArredondado % 100 == 0
                && !cemPontosSFX.isPlaying)
        {
            cemPontosSFX.Play();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || CrossPlatformInputManager.GetButtonDown("Jump"))
        //if (CrossPlatformInputManager.GetButtonDown("Jump")) -> somente android
        {
             Pular();

            jumpSFX.Play();

            animatorComponent.SetBool("Pulando", true);
         }

        else if (Input.GetKeyUp(KeyCode.UpArrow) || CrossPlatformInputManager.GetButtonUp("Jump")){
            animatorComponent.SetBool("Pulando", false);

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Abaixar();
        }

        else if (CrossPlatformInputManager.GetButtonDown("Down"))
        {
            Abaixar();
        }

        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Levantar();
        }

        else if (CrossPlatformInputManager.GetButtonUp("Down"))
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

        if (estaNoChao == false && pulosExtras > 0){
            rb.AddForce(Vector2.up * forcaPulo);
            pulosExtras--;
        }

        if (estaNoChao){
            pulosExtras = 1;
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

            backgroundMusic.Stop();

            restartButton.SetActive(true);

            sairButton.SetActive(true);

            Time.timeScale = 0;

        }
    }
}
