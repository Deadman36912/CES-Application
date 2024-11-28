using System.Collections.Generic;
using UnityEngine;

public class AnchorObjectPlacer : MonoBehaviour
{
    public GameObject anchorsParent;   // Parent des anchors
    public GameObject objectsParent;   // Parent des objets à placer

    private List<Transform> anchors;   // Liste des anchors
    private List<Transform> objects;   // Liste des objets à placer

    void Start()
    {
        // Initialisation des anchors et objets
        anchors = new List<Transform>();
        objects = new List<Transform>();

        // Remplir la liste des anchors et désactiver chaque anchor au départ
        foreach (Transform anchor in anchorsParent.transform)
        {
            anchor.gameObject.SetActive(false);
            anchors.Add(anchor);
        }

        // Remplir la liste des objets à placer
        foreach (Transform obj in objectsParent.transform)
        {
            objects.Add(obj);
        }

        // Compter les objets et activer le nombre nécessaire d'anchors
        PlaceObjectsOnAnchors();
    }

    void PlaceObjectsOnAnchors()
    {
        int objectCount = objects.Count;
        int anchorCount = Mathf.Min(objectCount, anchors.Count);

        for (int i = 0; i < anchorCount; i++)
        {
            // Activer l'anchor
            anchors[i].gameObject.SetActive(true);
            
            // Placer l'objet sur l'anchor sans changer sa hiérarchie
            objects[i].position = anchors[i].position;
            objects[i].rotation = anchors[i].rotation;

            // Afficher un message de débogage pour vérifier l'attachement
            Debug.Log($"Objet '{objects[i].name}' attaché à l'anchor '{anchors[i].name}' à la position {anchors[i].position}.");
        }

        // Désactiver les anchors inutilisés (optionnel)
        for (int i = anchorCount; i < anchors.Count; i++)
        {
            anchors[i].gameObject.SetActive(false);
        }

        // Afficher un message si le nombre d'anchors est insuffisant
        if (objectCount > anchors.Count)
        {
            Debug.LogWarning("Pas assez d'anchors pour tous les objets. Certains objets n'ont pas pu être placés.");
        }
    }
}
