using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefabs : MonoBehaviour
{
    // Référence au prefab que tu veux instancier
    public GameObject prefabToInstantiate;

    // Référence à l'objet parent contenant les enfants
    public Transform objectsParent;

    // Décalage sur l'axe X entre chaque prefab instancié
    public float xOffset;

    void Start()
    {
        // Vérifie si le prefab et l'objet parent sont bien assignés
        if (prefabToInstantiate != null && objectsParent != null)
        {
            // Vérifie si l'objet parent a des enfants
            int childCount = objectsParent.childCount;
            if (childCount == 0)
            {
                Debug.LogError("L'objet parent n'a aucun enfant à traiter.");
                return; // Arrête le script si aucun enfant n'est présent
            }

            int movedChildrenCount = 0; // Compteur pour vérifier le nombre d'enfants déplacés

            // Parcourt chaque enfant de `objectsParent` pour créer une instance de prefab pour chacun
            for (int i = 0; i < childCount; i++)
            {
                // Calcule la position d'instanciation en décalant sur l'axe X à chaque itération
                Vector3 instantiatePosition = this.transform.position + new Vector3(i * xOffset, 0, 0);

                // Instancie une copie du prefab et le place comme enfant du GameObject qui contient ce script
                GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, instantiatePosition, Quaternion.identity, this.transform);

                // Récupère l'enfant actuel
                Transform child = objectsParent.GetChild(0); // Toujours prendre le premier enfant car l'ordre change après SetParent

                // Renomme le prefab instancié en fonction du nom de l'enfant
                instantiatedPrefab.name = child.name + " Catalogue";

                // Déplace cet enfant sous le prefab instancié
                child.SetParent(instantiatedPrefab.transform);

                // Réinitialise la position locale de l'enfant pour qu'il soit centré dans le prefab
                child.localPosition = Vector3.zero;

                // Incrémente le compteur d'enfants déplacés
                movedChildrenCount++;
            }

            // Vérification finale
            if (movedChildrenCount == childCount)
            {
                Debug.Log("Tous les enfants ont été déplacés avec succès.");
            }
            else
            {
                Debug.LogError("Erreur : Le nombre d'enfants déplacés (" + movedChildrenCount + ") ne correspond pas au nombre d'enfants initial (" + childCount + ").");
            }
        }
        else
        {
            Debug.LogError("Le prefab ou l'objet parent n'est pas assigné.");
        }
    }
}
