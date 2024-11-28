using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    private Animator animator;

    // Champ public pour spécifier le nom de l'animation dans l'inspecteur Unity
    public string animationName = "Scene"; // Valeur par défaut, peut être changée dans l'inspecteur

    void Awake()
    {
        // Récupérer l'Animator attaché à l'objet
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void OnEnable()
    {
        if (animator != null)
        {
            if (!string.IsNullOrEmpty(animationName))
            {
                Debug.Log("Playing animation: " + animationName + " on " + gameObject.name);
                animator.Play(animationName);
            }
            else
            {
                Debug.LogError("Animation name is empty on " + gameObject.name);
            }
        }
    }
}
