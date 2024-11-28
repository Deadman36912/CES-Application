using UnityEngine;
using Leap.Unity.Interaction;

[RequireComponent(typeof(Anchor))]
public class SpecificAnchorAttachmentHandler : MonoBehaviour
{
    public GameObject objectToToggle;  // L'objet à faire apparaître/disparaître
    public Anchor targetAnchor;        // L'ancre spécifique à vérifier

    private Anchor _anchor;

    void Start()
    {
        _anchor = GetComponent<Anchor>();

        // Masquer l'objet au début si rien n'est ancré
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
        }
    }

    void Update()
    {
        if (_anchor.hasAnchoredObjects && _anchor == targetAnchor)
        {
            // Afficher l'objet si quelque chose est ancré et que c'est la bonne ancre
            if (objectToToggle != null && !objectToToggle.activeSelf)
            {
                objectToToggle.SetActive(true);
            }
        }
        else
        {
            // Masquer l'objet si rien n'est ancré ou si ce n'est pas la bonne ancre
            if (objectToToggle != null && objectToToggle.activeSelf)
            {
                objectToToggle.SetActive(false);
            }
        }
    }
}
