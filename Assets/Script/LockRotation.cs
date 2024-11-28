using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    [Header("Lock Axes")]
    public bool lockX = true;
    public bool lockY = false;
    public bool lockZ = true;

    void Start()
    {
        // Enregistrer la rotation initiale en fonction des axes verrouillés
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Récupérer les rotations actuelles
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Appliquer les verrouillages d'axe selon les cases cochées
        float x = lockX ? initialRotation.eulerAngles.x : currentRotation.x;
        float y = lockY ? initialRotation.eulerAngles.y : currentRotation.y;
        float z = lockZ ? initialRotation.eulerAngles.z : currentRotation.z;

        // Appliquer la rotation en fonction des valeurs choisies
        transform.rotation = Quaternion.Euler(x, y, z);
    }
}
