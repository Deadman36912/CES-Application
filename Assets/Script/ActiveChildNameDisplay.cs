using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveChildNameDisplay : MonoBehaviour
{
    // Les trois objets parents
    public GameObject parent1;
    public GameObject parent2;
    public GameObject parent3;

    // Les TextMeshPro associés, nommés comme les parents
    public TextMeshPro parent1TMP;
    public TextMeshPro parent2TMP;
    public TextMeshPro parent3TMP;

    // Mots à supprimer des noms affichés
    private readonly List<string> wordsToRemove = new List<string> { "PROCESSEUR", "CARTE GRAPHIQUE", "RAM" };

    // Fonction pour mettre à jour les noms affichés dans les TextMeshPro
    public void UpdateActiveChildNames()
    {
        if (parent1 != null && parent1TMP != null)
        {
            string activeChildName = GetActiveChildName(parent1);
            parent1TMP.text = activeChildName != null ? RemoveWords(activeChildName) : "Aucun enfant actif";
        }

        if (parent2 != null && parent2TMP != null)
        {
            string activeChildName = GetActiveChildName(parent2);
            parent2TMP.text = activeChildName != null ? RemoveWords(activeChildName) : "Aucun enfant actif";
        }

        if (parent3 != null && parent3TMP != null)
        {
            string activeChildName = GetActiveChildName(parent3);
            parent3TMP.text = activeChildName != null ? RemoveWords(activeChildName) : "Aucun enfant actif";
        }
    }

    // Fonction auxiliaire pour récupérer le nom de l'enfant actif dans un parent donné
    private string GetActiveChildName(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                return child.name;
            }
        }
        return null; // Aucun enfant actif trouvé
    }

    // Fonction pour supprimer des mots spécifiques du nom
    private string RemoveWords(string input)
    {
        foreach (string word in wordsToRemove)
        {
            input = input.Replace(word, "").Trim(); // Remplace le mot par une chaîne vide et enlève les espaces
        }
        return input;
    }
}
