using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    // Liste des objets à activer/désactiver
    public List<GameObject> targetObjects = new List<GameObject>();

    // Fonction pour désactiver tous les objets de la liste
    public void DisableObjects()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    // Fonction pour activer tous les objets de la liste
    public void EnableObjects()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}
