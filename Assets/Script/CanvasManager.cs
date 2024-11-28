using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas targetCanvas;  // Le Canvas à afficher/masquer

    // Méthode pour faire apparaître le Canvas
    public void ShowCanvas()
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Aucun Canvas n'a été assigné au CanvasManager.");
        }
    }

    // Méthode pour masquer le Canvas (optionnelle)
    public void HideCanvas()
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Aucun Canvas n'a été assigné au CanvasManager.");
        }
    }
}
