using UnityEngine;
using UnityEngine.SceneManagement;  // Necess�rio para carregar a cena

public class RestartOnClick : MonoBehaviour
{
    // Esse script pode ser anexado ao objeto que ser� clicado para reiniciar o n�vel

    // Detecta o clique no objeto e reinicia a cena
    private void OnMouseDown()
    {
        RestartLevel();
    }

    // Fun��o para reiniciar a cena atual
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recarrega a cena atual
    }
}