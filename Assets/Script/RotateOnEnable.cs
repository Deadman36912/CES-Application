using UnityEngine;

public class RotateOnEnable : MonoBehaviour
{
    // Vitesse de rotation en Y (en degrés par seconde)
    public float rotationSpeedY = 100f;

    // Fonction appelée chaque frame
    private void Update()
    {
        // Appliquer la rotation continue autour de l'axe Y uniquement
        transform.Rotate(0, rotationSpeedY * Time.deltaTime, 0);
    }
}
