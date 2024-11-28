using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverDetector : MonoBehaviour
{
    public TextMeshPro titleText;
    public TextMeshPro descriptionText;

    private Dictionary<string, string> descriptions = new Dictionary<string, string>();

    public Collider leftHandCollider;
    public Collider rightHandCollider;

    public GameObject targetParent;

    private List<Collider> targetColliders = new List<Collider>();

    private bool isColliding = false;

    void Start()
    {
        LoadDescriptions();

        if (leftHandCollider == null || rightHandCollider == null)
        {
            Debug.LogError("Les colliders des mains ne sont pas assignés. Assignez-les dans l'Inspector.");
        }

        if (targetParent == null)
        {
            Debug.LogError("L'objet parent cible n'est pas assigné. Assignez-le dans l'Inspector.");
            return;
        }

        foreach (Transform child in targetParent.transform)
        {
            Collider childCollider = child.GetComponent<Collider>();
            if (childCollider != null)
            {
                targetColliders.Add(childCollider);
            }
        }
    }

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
            Debug.LogError("Fichier Assemblages.txt non trouvé dans Resources.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other == leftHandCollider || other == rightHandCollider) && !isColliding)
        {
            if (targetColliders.Contains(other))
            {
                isColliding = true;
                OnCollisionEnterAction(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other == leftHandCollider || other == rightHandCollider) && isColliding)
        {
            if (targetColliders.Contains(other))
            {
                isColliding = false;
                OnCollisionExitAction();
            }
        }
    }

    private void OnCollisionEnterAction(Collider other)
    {
        string objectName = other.gameObject.name;

        titleText.text = objectName;

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
    }

    private void OnCollisionExitAction()
    {
        titleText.text = "";
        descriptionText.text = "";
        Debug.Log("La main a quitté la collision avec l'objet.");
    }
}
