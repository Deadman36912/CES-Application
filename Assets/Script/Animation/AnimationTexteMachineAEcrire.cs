using System.Collections;
using UnityEngine;
using TMPro;

public class AnimationTexteMachineAEcrire : MonoBehaviour
{
    [Header("Paramètres d'animation")]
    public float delaiAvantAnimation = 0.5f; // Temps d'attente avant de démarrer l'animation
    public float vitesseEcriture = 0.05f; // Vitesse entre chaque caractère
    public bool afficherCurseur = true; // Afficher un curseur clignotant à la fin
    public float vitesseCurseur = 0.5f; // Vitesse de clignotement du curseur

    private TMP_Text texteTMP; // Composant TextMeshPro
    private string texteComplet = ""; // Texte complet à animer
    private string texteActuel = ""; // Texte affiché progressivement
    private bool curseurVisible = true; // État du curseur clignotant
    private Coroutine animationEnCours; // Référence à l'animation en cours
    private string dernierTexte = ""; // Sauvegarde le dernier texte pour éviter le spam

    private void OnEnable()
    {
        texteTMP = GetComponent<TMP_Text>();

        if (texteTMP == null)
        {
            Debug.LogError("Aucun composant TMP_Text trouvé sur l'objet.");
            return;
        }

        // Relance proprement l'animation si nécessaire
        if (animationEnCours != null)
        {
            StopCoroutine(animationEnCours);
        }
        animationEnCours = StartCoroutine(LancerAnimationAvecDelai());
    }

    private IEnumerator LancerAnimationAvecDelai()
    {
        // Attend un délai initial avant de vérifier le texte
        yield return new WaitForSeconds(delaiAvantAnimation);

        // Vérifie si le texte a changé et si un texte valide est présent
        if (texteTMP.text != dernierTexte && !string.IsNullOrEmpty(texteTMP.text))
        {
            texteComplet = texteTMP.text; // Récupère le texte complet
            dernierTexte = texteComplet; // Sauvegarde pour éviter de relancer inutilement
            texteTMP.text = ""; // Réinitialise l'affichage pour démarrer l'animation
            texteActuel = ""; // Réinitialise le texte en cours

            // Lance l'animation
            yield return StartCoroutine(AnimerTexte());
        }
    }

    private IEnumerator AnimerTexte()
    {
        // Affiche le texte caractère par caractère
        for (int i = 0; i < texteComplet.Length; i++)
        {
            texteActuel += texteComplet[i];
            texteTMP.text = texteActuel + (afficherCurseur ? "|" : ""); // Ajoute temporairement le curseur
            yield return new WaitForSeconds(vitesseEcriture);
        }

        // Une fois l'écriture terminée, démarre le clignotement du curseur
        if (afficherCurseur)
        {
            StartCoroutine(ClignoterCurseur());
        }
    }

    private IEnumerator ClignoterCurseur()
    {
        while (true)
        {
            curseurVisible = !curseurVisible; // Alterne l'état du curseur
            texteTMP.text = texteActuel + (curseurVisible ? "|" : ""); // Ajoute ou enlève le curseur
            yield return new WaitForSeconds(vitesseCurseur);
        }
    }

    private void OnDisable()
    {
        // Stoppe toutes les animations en cours si l'objet est désactivé
        if (animationEnCours != null)
        {
            StopCoroutine(animationEnCours);
        }
        StopAllCoroutines();
        texteTMP.text = dernierTexte; // Réinitialise le texte complet sans le curseur
    }
}
