using UnityEngine;
using UnityEngine.SceneManagement;  // Necessário para carregar a cena

public class RestartOnClick : MonoBehaviour
{
    // Esse script pode ser anexado ao objeto que será clicado para reiniciar o nível

    // Detecta o clique no objeto e reinicia a cena
    private void OnMouseDown()
    {
        RestartLevel();
    }

    // Função para reiniciar a cena atual
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recarrega a cena atual
    }
}