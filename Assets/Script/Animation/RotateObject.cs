using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 10f;  // Vitesse de rotation

    // Méthode publique pour faire tourner l'objet
    public void Rotate()
    {
        // Calculer la rotation Y en fonction de la vitesse et du deltaTime
        float rotationY = rotationSpeed * Time.deltaTime;
        
        // Appliquer la rotation sur l'axe Y de l'objet
        transform.Rotate(0, rotationY, 0);

        // Remettre les rotations en X et Z à 0 pour conserver uniquement la rotation en Y
        Vector3 currentRotation = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, 0);

    }
}
