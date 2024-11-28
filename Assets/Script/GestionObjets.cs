using System.Collections.Generic;
using UnityEngine;

public class GestionObjets : MonoBehaviour
{
    // Liste des objets à gérer
    [Header("Liste des objets à activer ou désactiver")]
    public List<GameObject> objets = new List<GameObject>();

    /// <summary>
    /// Active tous les objets dans la liste
    /// </summary>
    public void ActiverObjets()
    {
        foreach (GameObject obj in objets)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Désactive tous les objets dans la liste
    /// </summary>
    public void DesactiverObjets()
    {
        foreach (GameObject obj in objets)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
