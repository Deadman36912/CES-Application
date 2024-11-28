using UnityEngine;
using Leap.Unity.Interaction;

public class AnchorChildMover : MonoBehaviour
{
    private Anchor _anchor;

    void Start()
    {
        // Récupère le composant Anchor attaché à cet objet
        _anchor = GetComponent<Anchor>();

        if (_anchor == null)
        {
            Debug.LogError("Anchor component is required for this script to work.");
        }
    }

    void Update()
    {
        // Vérifie si un objet est actuellement ancré
        if (_anchor.hasAnchoredObjects)
        {
            // Déplace chaque objet ancré dans l'ancre dans la hiérarchie
            foreach (var anchoredObject in _anchor.anchoredObjects)
            {
                if (anchoredObject.transform.parent != this.transform)
                {
                    // Change le parent de l'objet ancré pour qu'il soit l'Anchor lui-même
                    anchoredObject.transform.SetParent(this.transform);
                    Debug.Log($"{anchoredObject.name} has been moved to the Anchor in the hierarchy.");
                }
            }
        }
    }
}
