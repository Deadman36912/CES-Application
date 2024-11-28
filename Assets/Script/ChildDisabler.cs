using System.Collections.Generic;
using UnityEngine;

public class ChildDisabler : MonoBehaviour
{
    // Liste d'objets dont les enfants seront désactivés
    public List<GameObject> parents = new List<GameObject>();

    // Fonction publique pour désactiver tous les enfants des objets dans la liste
    public void DisableAllChildren()
    {
        foreach (GameObject parent in parents)
        {
            if (parent != null)
            {
                DisableChildren(parent);
            }
        }
    }

    // Fonction auxiliaire pour désactiver tous les enfants d'un objet parent donné
    private void DisableChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
