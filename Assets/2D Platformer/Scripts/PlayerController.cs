using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Necess�rio para carregar a cena

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;
        private bool doubleJump = true;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private GameManager gameManager;

        private bool gameOver = false;  // Controle de estado do jogo (game over)

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        private void FixedUpdate()
        {
            if (gameOver) return;  // N�o permite mais movimento ou intera��es se o jogo acabou.

            CheckGround();
        }

        void Update()
        {
            if (gameOver)  // Se o jogo acabou, espera o clique para reiniciar
            {
                if (Input.GetMouseButtonDown(0))  // Clique do mouse para reiniciar
                {
                    RestartGame();  // Reinicia o jogo
                }
                return;  // N�o executa o restante do c�digo se o jogo acabou
            }

            // Controle de movimento (apenas para teste - se voc� quiser controle do jogador, pode usar Input.GetAxis)
            moveInput = 1;  // Movimento fixo para direita, mas voc� pode alterar isso
            Vector3 direction = transform.right * moveInput;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
            animator.SetInteger("playerState", 1); // Ativa anima��o de correr

            // Verifica se o jogador est� no ch�o
            if (isGrounded)
            {
                doubleJump = true;
            }

            // Verifica se o jogador clicou com o mouse (bot�o esquerdo)
            if (Input.GetMouseButtonDown(0) && (isGrounded || (!isGrounded && doubleJump)))
            {
                if (!isGrounded)
                {
                    doubleJump = false;
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0); // Reseta a velocidade Y antes de pular.
                }

                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);  // Aplica a for�a do pulo
                animator.SetInteger("playerState", 2); // Ativa anima��o de pulo
            }

            // Verifica se o personagem precisa ser virado (flip) dependendo da dire��o do movimento.
            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }

        // Fun��o para inverter a dire��o do personagem (flip).
        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;  // Inverte o eixo X.
            transform.localScale = Scaler;
        }

        // Fun��o para checar se o jogador est� no ch�o.
        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;  // Se houver mais de um collider, o jogador est� no ch�o.
        }

        // Detec��o de colis�o com inimigos (se o jogador colidir com um inimigo, entra em estado de morte).
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                deathState = true;  // Diz ao GameManager que o jogador morreu
                gameOver = true;    // Ativa o estado de "game over"
                animator.SetInteger("playerState", 3);  // Ativa a anima��o de morte, por exemplo
            }
            else
            {
                deathState = false;
            }
        }

        // Detec��o de coleta de moedas (aumenta a contagem de moedas).
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin")
            {
                gameManager.coinsCounter += 1;
                Destroy(other.gameObject);  // Destroi o objeto da moeda
            }
        }

        // Fun��o para reiniciar o jogo (recarrega a cena atual)
        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recarrega a cena atual
        }
    }
}