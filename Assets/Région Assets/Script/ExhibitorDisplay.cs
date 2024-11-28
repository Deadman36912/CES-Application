using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExhibitorDisplay : MonoBehaviour
{
    // Références pour les éléments du Canvas
    public TMP_Text nomText;
    public TMP_Text standText;
    public TMP_Text descriptionLongueText;
    public SpriteRenderer logoSpriteRenderer;

    // Nom du fichier CSV (sans l'extension, doit être dans le dossier Resources)
    public string csvFileName = "CES2025_Exhibitors";

    // L'objet que vous souhaitez désactiver
    public GameObject objectToDisable;

    // Classe représentant un exposant
    [System.Serializable]
    public class Exhibitor
    {
        public string Stand;
        public string Nom;
        public string DescriptionLongue;
        public string LogoPath; // Chemin vers le logo
    }

    // Liste des exposants chargés depuis le CSV
    private List<Exhibitor> exhibitors = new List<Exhibitor>();

    void Start()
    {
        LoadCSV();
    }

    /// <summary>
    /// Charge les données du fichier CSV (avec des points-virgules comme séparateurs).
    /// </summary>
    void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
        if (csvFile == null)
        {
            Debug.LogError($"Fichier CSV '{csvFileName}' introuvable dans Resources !");
            return;
        }

        string[] lines = csvFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++) // Ignorer la première ligne (en-têtes)
        {
            string[] data = lines[i].Split(';'); // Utiliser le séparateur point-virgule
            if (data.Length < 7) continue; // Vérifier qu'il y a suffisamment de colonnes

            Exhibitor exhibitor = new Exhibitor
            {
                Stand = data[1].Trim(), // Colonne Stand (index 1)
                Nom = data[0].Trim(), // Colonne Nom (index 0)
                DescriptionLongue = data[3].Trim(), // Colonne Description longue (index 3)
                LogoPath = $"Logo/{data[6].Trim().Replace(".PNG", "")}" // Colonne Logo (index 6), sans extension
            };
            exhibitors.Add(exhibitor);
        }

        Debug.Log($"Données chargées : {exhibitors.Count} exposants trouvés.");
    }

    /// <summary>
    /// Méthode appelée lorsqu'un bouton est cliqué.
    /// </summary>
    /// <param name="stand">Le numéro de stand, passé depuis le bouton.</param>
    public void OnButtonClick(string stand)
    {
        // Recherche de l'exposant correspondant au numéro de stand
        Exhibitor exhibitor = exhibitors.Find(e => e.Stand == stand);
        if (exhibitor == null)
        {
            Debug.LogError($"Aucun exposant trouvé pour le stand '{stand}' !");
            return;
        }

        // Mettre à jour les textes
        nomText.text = exhibitor.Nom;
        standText.text = "Stand: " + exhibitor.Stand;
        descriptionLongueText.text = exhibitor.DescriptionLongue;

        // Charger et afficher le logo
        Sprite logo = Resources.Load<Sprite>(exhibitor.LogoPath);
        if (logo != null)
        {
            logoSpriteRenderer.sprite = logo;
        }
        else
        {
            Debug.LogWarning($"Logo introuvable pour le chemin '{exhibitor.LogoPath}' !");
            logoSpriteRenderer.sprite = null; // Efface le sprite si le logo est introuvable
        }

        // S'assurer que le SpriteRenderer est activé
        logoSpriteRenderer.enabled = true;

        // Désactiver l'objet assigné
        DisableObject();
    }

    /// <summary>
    /// Méthode pour désactiver l'objet défini dans la variable publique.
    /// </summary>
    private void DisableObject()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
            Debug.Log($"Objet '{objectToDisable.name}' désactivé.");
        }
        else
        {
            Debug.LogWarning("Aucun objet assigné pour désactivation.");
        }
    }
}
