using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes

public class GestionScene : MonoBehaviour
{
    public void RechargerScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
