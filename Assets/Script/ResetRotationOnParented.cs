using UnityEngine;

public class ResetRotationOnParented : MonoBehaviour
{
    [Header("Reset Rotation Axes")]
    public bool resetX = true;
    public bool resetY = true;
    public bool resetZ = true;

    void OnTransformParentChanged()
    {
        // Vérifier si l'objet a un parent
        if (transform.parent != null)
        {
            // Récupérer les rotations actuelles
            Vector3 currentRotation = transform.localRotation.eulerAngles;

            // Appliquer la réinitialisation en fonction des axes choisis
            float x = resetX ? 0 : currentRotation.x;
            float y = resetY ? 0 : currentRotation.y;
            float z = resetZ ? 0 : currentRotation.z;

            // Appliquer la rotation
            transform.localRotation = Quaternion.Euler(x, y, z);
        }
    }
}
