using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Leap.Unity.Interaction;

public class AnchorDetector : MonoBehaviour
{
    // Références aux TextMeshPro pour le titre et la description
    public TextMeshPro titleText;
    public TextMeshPro descriptionText;

    // Référence à BIMManager pour mettre à jour anchorObject
    public BIMManager bimManager;

    // Dictionnaire pour stocker les descriptions du fichier
    private Dictionary<string, string> descriptions = new Dictionary<string, string>();

    private Anchor anchor;

    void Start()
    {
        // Charger les descriptions depuis le fichier
        LoadDescriptions();

        // Récupérer la référence de l'Anchor et ajouter un listener pour les objets attachés
        anchor = GetComponent<Anchor>();
        if (anchor != null)
        {
            anchor.OnAnchorablesAttached += UpdateObjectInfo;
        }
        else
        {
            Debug.LogError("Le composant Anchor est manquant sur cet objet.");
        }
    }

    // Fonction pour charger les descriptions depuis le fichier descriptions.txt
    private void LoadDescriptions()
    {
        TextAsset descriptionFile = Resources.Load<TextAsset>("Assemblages");

        if (descriptionFile != null)
        {
            string[] lines = descriptionFile.text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 2)
                {
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
            Debug.LogError("Fichier de description non trouvé dans Resources.");
        }
    }

    // Fonction appelée lorsqu'un objet est attaché à l'Anchor
    private void UpdateObjectInfo()
    {
        foreach (var anchoredObject in anchor.anchoredObjects)
        {
            string objectName = anchoredObject.gameObject.name;

            // Affiche le nom de l'objet dans le TextMeshPro du titre
            titleText.text = objectName;

            // Chercher la description dans le dictionnaire et l'afficher
            if (descriptions.TryGetValue(objectName, out string description))
            {
                descriptionText.text = description;
                Debug.Log($"Description pour '{objectName}': {description}");
            }
            else
            {
                descriptionText.text = "Description non trouvée.";
                Debug.LogWarning($"Description pour '{objectName}' non trouvée.");
            }

            // Mettre à jour anchorObject dans BIMManager
            if (bimManager != null)
            {
                bimManager.anchorObject = anchoredObject.gameObject;
            }

            break; // On sort après le premier objet trouvé
        }
    }

    void OnDestroy()
    {
        // Retirer le listener pour éviter les erreurs si l'objet est détruit
        if (anchor != null)
        {
            anchor.OnAnchorablesAttached -= UpdateObjectInfo;
        }
    }
}
