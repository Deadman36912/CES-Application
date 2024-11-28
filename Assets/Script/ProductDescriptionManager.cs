using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductDescriptionManager : MonoBehaviour
{
    public TextMeshPro titleText;       // Champ texte pour afficher le nom sans "Assemblage"
    public TextMeshPro descriptionText; // Champ texte pour afficher la description
    private Dictionary<string, string> descriptions = new Dictionary<string, string>();

    void Start()
    {
        LoadDescriptions();
    }

    // Fonction pour charger le fichier Assemblages.txt
    private void LoadDescriptions()
    {
        // Charger le fichier Assemblages.txt depuis Resources
        TextAsset descriptionFile = Resources.Load<TextAsset>("Assemblages");
        if (descriptionFile != null)
        {
            // Lire chaque ligne du fichier
            string[] lines = descriptionFile.text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 2)
                {
                    // Récupérer le nom et la description, en supprimant les espaces en trop
                    string name = parts[0].Trim();
                    string description = parts[1].Trim();
                    
                    if (!descriptions.ContainsKey(name))
                    {
                        descriptions.Add(name, description);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Fichier Assemblages.txt non trouvé dans Resources.");
        }
    }

    // Fonction pour mettre à jour l'affichage de l'objet en fonction de son nom
    public void UpdateObjectInfo(string objectName)
    {
        // Supprime le préfixe "Assemblage " du nom pour l'afficher proprement
        string displayName = objectName.Replace("Assemblage ", "").Trim();
        titleText.text = displayName;

        // Récupérer la description correspondante si elle existe
        if (descriptions.TryGetValue(objectName, out string description))
        {
            descriptionText.text = description;
        }
        else
        {
            descriptionText.text = "Description non trouvée.";
        }
    }
}