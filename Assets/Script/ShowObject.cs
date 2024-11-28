using UnityEngine;

public class ObjectVisibilityController : MonoBehaviour
{
    // Référence à l'objet que vous souhaitez faire apparaître ou disparaître
    public GameObject targetObject;

    // Fonction publique pour faire apparaître l'objet
    public void ShowObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
            Debug.Log($"{targetObject.name} est maintenant visible.");
        }
        else
        {
            Debug.LogError("Aucun objet n'a été assigné dans 'targetObject'.");
        }
    }

    // Fonction publique pour désactiver l'objet
    public void HideObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            Debug.Log($"{targetObject.name} est maintenant caché.");
        }
        else
        {
            Debug.LogError("Aucun objet n'a été assigné dans 'targetObject'.");
        }
    }
}
