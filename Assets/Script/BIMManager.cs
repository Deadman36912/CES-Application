using UnityEngine;

public class BIMManager : MonoBehaviour
{
    // Références aux parents pour chaque catégorie
    public GameObject carteGraphiqueParent;
    public GameObject processeurParent;
    public GameObject ramParent;

    // Variable pour stocker dynamiquement l'objet attaché
    public GameObject anchorObject;

    // Fonction pour activer les objets correspondants dans chaque catégorie
    public void ShowMatchingComponentsFromAnchor()
    {
        if (carteGraphiqueParent == null || processeurParent == null || ramParent == null || anchorObject == null)
        {
            Debug.LogError("Un ou plusieurs parents de catégorie ou l'objet d'ancrage n'est pas assigné.");
            return;
        }

        // Activer les objets correspondants dans chaque catégorie
        bool foundAny = false;
        foundAny |= ActivateMatchingChildInCategory(carteGraphiqueParent, "CARTE GRAPHIQUE", anchorObject);
        foundAny |= ActivateMatchingChildInCategory(processeurParent, "PROCESSEUR", anchorObject);
        foundAny |= ActivateMatchingChildInCategory(ramParent, "RAM", anchorObject);

        if (!foundAny)
        {
            Debug.LogWarning("Aucun objet correspondant trouvé dans les catégories pour l'objet attaché.");
        }
    }

    // Fonction auxiliaire pour activer un enfant dans une catégorie
    private bool ActivateMatchingChildInCategory(GameObject categoryParent, string categoryPrefix, GameObject anchorObject)
    {
        Transform matchingChild = null;
        foreach (Transform child in anchorObject.transform)
        {
            if (child.name.StartsWith(categoryPrefix))
            {
                matchingChild = child;
                break;
            }
        }

        if (matchingChild == null)
        {
            Debug.LogWarning($"Aucun enfant correspondant avec le préfixe '{categoryPrefix}' trouvé dans l'objet d'ancrage '{anchorObject.name}'.");
            return false;
        }

        bool foundInCategory = false;
        foreach (Transform child in categoryParent.transform)
        {
            if (child.name == matchingChild.name)
            {
                child.gameObject.SetActive(true);
                foundInCategory = true;
                Debug.Log($"Objet correspondant '{child.name}' activé dans la catégorie '{categoryParent.name}'.");
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        return foundInCategory;
    }
}
