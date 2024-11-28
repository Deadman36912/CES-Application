using System.Linq;
using UnityEngine;
using Leap.Unity.Interaction;

public class AnchorObjectActivator : MonoBehaviour
{
    public GameObject parentObject; // L'objet parent dont les enfants doivent être activés/désactivés

    private Anchor _anchor;

    void Start()
    {
        _anchor = GetComponent<Anchor>();

        // Masquer tous les enfants de l'objet parent au début
        if (parentObject != null)
        {
            foreach (Transform child in parentObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        // Abonner aux événements d'attachement et de détachement
        _anchor.OnAnchorablesAttached += OnAnchorablesAttached;
        _anchor.OnNoAnchorablesAttached += OnNoAnchorablesAttached;
    }

    void OnDestroy()
    {
        // Désabonner des événements pour éviter les fuites de mémoire
        if (_anchor != null)
        {
            _anchor.OnAnchorablesAttached -= OnAnchorablesAttached;
            _anchor.OnNoAnchorablesAttached -= OnNoAnchorablesAttached;
        }
    }

    private void OnAnchorablesAttached()
    {
        // Récupérer l'objet ancré
        var anchoredObject = _anchor.anchoredObjects.FirstOrDefault();
        if (anchoredObject != null)
        {
            // Masquer tous les enfants de l'objet parent
            foreach (Transform child in parentObject.transform)
            {
                child.gameObject.SetActive(false);
            }

            // Afficher l'enfant correspondant au nom de l'objet ancré
            Transform targetChild = parentObject.transform.Find(anchoredObject.name);
            if (targetChild != null)
            {
                targetChild.gameObject.SetActive(true);
            }
        }
    }

    private void OnNoAnchorablesAttached()
    {
        // Masquer tous les enfants de l'objet parent lorsque tous les objets sont détachés
        if (parentObject != null)
        {
            foreach (Transform child in parentObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
