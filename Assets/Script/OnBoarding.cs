using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnBoarding : MonoBehaviour
{
    public Text TexteOnboarding;
    public Animator FadeInScreen;

    public GameObject SupportTexteOnboarding;

    float NbMessage = 0;

    public static bool FirstSpeechDone = false;

    // Start is called before the first frame update
    void Start()
    {
        TexteOnboarding.text = "La première guerre mondiale ... Depuis 1914, le conflit dure, s'étendant partout. Notr epays a été attaqué par les allemands suite à une escalade de tension.";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwitchTexte();
            NbMessage += 1;
        }
    }

    void SwitchTexte()
    {
        if (NbMessage == 0)
        {
            TexteOnboarding.text = "Depuis 2 ans, nos braves soldats se battent partout, bloquant la progression ennemi ... La Marne, Verdun ... De grandes batailles qui ont montré notre détermination à bouter l'envahisseur hors de notre territoire !";
        }

        if (NbMessage == 1)
        {
            TexteOnboarding.text = "Depuis 1915, nous sommes bloqués dans les tranchées et la boue, livrant une sale guerre de position. Cependant, nous sommes en 1916 et nos chefs ont enfin décidé d'attaquer conjointement avec les anglais. C'est la première fois de la guerre que nous le faisons.";
        }

        if (NbMessage == 2)
        {
            TexteOnboarding.text = "Il parrait que les britanniques ont une nouvelle armes contre les germains, que ça va enfin briser la guerre de mouvement et permettre de reprendre l'initiative, je me demande de quoi il s'agit ...";
        }

        if (NbMessage == 3)
        {
            TexteOnboarding.text = "Je me présente, Célestin Favourier, jeune capitaine du 212eme Régiment d'Infanterie de l'armée française. Je viens tout juste d'arriver au front ... Et ceci est mon histoire.";
        }

        if (NbMessage == 4)
        {
            SupportTexteOnboarding.SetActive(false);
            FirstSpeechDone = true;

            FadeInScreen.SetTrigger("FirstSpeechDone");
        }
    }
}
