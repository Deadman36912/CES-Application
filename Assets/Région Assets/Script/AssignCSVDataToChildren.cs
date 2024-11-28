using System.Collections.Generic;
using UnityEngine;

public class AssignCSVDataToChildren : MonoBehaviour
{
    public Transform parent; // Le parent contenant les objets enfants existants
    public string csvFileName = "CES2025_Exhibitors"; // Nom du fichier CSV (sans extension)

    [System.Serializable]
    public class Exhibitor
    {
        public string Nom;
        public string Stand;
        public string DescriptionCourte;
        public string DescriptionLongue;
        public string Contact;
        public string LogoPath;
    }

    private List<Exhibitor> exhibitors = new List<Exhibitor>();

    void Start()
    {
        LoadCSV();
        AssignDataToChildren();
    }

    /// <summary>
    /// Charge les données du fichier CSV.
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
            if (data.Length < 6) continue;

            Exhibitor exhibitor = new Exhibitor
            {
                Nom = data[0].Trim(),
                Stand = data[1].Trim(),
                DescriptionCourte = data[2].Trim(),
                DescriptionLongue = data[3].Trim(),
                Contact = data[4].Trim(),
                LogoPath = data[5].Trim()
            };

            exhibitors.Add(exhibitor);
        }

        Debug.Log($"Données chargées : {exhibitors.Count} exposants trouvés.");
    }

    /// <summary>
    /// Associe les données du CSV aux objets enfants existants.
    /// </summary>
    void AssignDataToChildren()
    {
        int childCount = parent.childCount;

        if (exhibitors.Count > childCount)
        {
            Debug.LogWarning($"Il y a plus d'entreprises dans le CSV ({exhibitors.Count}) que d'objets enfants ({childCount}). Les surplus seront ignorés.");
        }

        for (int i = 0; i < childCount; i++)
        {
            if (i >= exhibitors.Count) break;

            Transform child = parent.GetChild(i);
            Exhibitor exhibitor = exhibitors[i];

            // Renommer l'enfant avec le numéro de stand et le nom de l'entreprise
            child.name = $"{exhibitor.Stand}- {exhibitor.Nom}";

            // Ajouter les informations à un composant dédié (optionnel)
            var info = child.gameObject.AddComponent<ExhibitorInfo>();
            info.Nom = exhibitor.Nom;
            info.Stand = exhibitor.Stand;
            info.DescriptionLongue = exhibitor.DescriptionLongue;
            info.Contact = exhibitor.Contact;

            Debug.Log($"Objet {child.name} assigné à l'entreprise : {exhibitor.Nom}");
        }
    }
}

/// <summary>
/// Classe pour stocker les informations d'un exposant (attachée à chaque objet enfant).
/// </summary>
public class ExhibitorInfo : MonoBehaviour
{
    public string Nom;
    public string Stand;
    public string DescriptionLongue;
    public string Contact;
}
