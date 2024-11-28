using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // La caméra que l'objet doit regarder
    public Camera targetCamera;

    void Start()
    {
        // Si aucune caméra n'est spécifiée, utiliser la caméra principale par défaut
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        // S'assurer que la caméra cible est définie
        if (targetCamera != null)
        {
            // Faire en sorte que l'objet regarde la caméra
            transform.LookAt(targetCamera.transform);
        }
    }
}
