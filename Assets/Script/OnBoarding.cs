using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnBoarding : MonoBehaviour
{
    public GameObject ImageFond1;
    public GameObject ImageFond2;
    public GameObject ImageFond3;

    public GameObject SonDiapo;

    public Text TexteOnboarding;
    public Animator FadeInScreen;

    private float waitTime = 2.0f;
    private float timer = 0.0f;
    private float conteur = 0f;

    public GameObject SupportTexteOnboarding;
    public GameObject PanelInfo;

    float NbMessage = 0;

    public static bool FirstSpeechDone = false;
    private bool LancementPanelInfo = false;
    private bool FinPanelInfo = false;

    // Start is called before the first frame update
    void Start()
    {
        TexteOnboarding.text = "La Grande Guerre ... C'est le 3 septembre 1914, tout à basculé : notre vie, notre destin, celle de notre nation, celle de l'Europe ... Il a suffit d'un meutre dans une ville perdue des Balkans et tout s'est enchainé très vite, une escalade de tension.";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwitchTexte();
            NbMessage += 1;
        }

        if (LancementPanelInfo == true && conteur == 0)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
            {
                timer = timer - waitTime;
                ApparitionPanel();
                conteur += 1;
            }
        }

        if (FinPanelInfo == true)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
            {
                timer = timer - waitTime;

                PanelInfo.SetActive(false);
                FadeInScreen.SetTrigger("FirstSpeechDone");
                FinPanelInfo = false;
            }
        }

        void SwitchTexte()
        {
            if (NbMessage == 0)
            {
                TexteOnboarding.text = "Du jour au lendemain, des hommes de 20 à 45 ans abandonnent tout. Ils partent par devoir, dans l'espoir d'une guerre rapide ... Le temps de sortir le fusil puis de rentrer. C'est l'histoire d'une escapade, qui tourne au cauchemar.";

                ImageFond1.SetActive(false);
                ImageFond2.SetActive(true);

                SonDiapo.SetActive(true);
            }

            if (NbMessage == 1)
            {
                TexteOnboarding.text = "Ainsi, depuis 2 ans, nos bifins se battent partout, se sacrifiant pour bloquer la progression des germains : La Marne, Verdun ... Que de grandes batailles qui ont montré notre détermination à dégager l'envahisseur hors de notre patrie !";

                SonDiapo.SetActive(false);


            }

            if (NbMessage == 2)
            {
                TexteOnboarding.text = "Cependant depuis 1915, nous sommes bloqués dans les tranchées et la boue, livrant une sale guerre de position. On reste ici à attendre péniblement le moment de l'assaut ... et du massacre. Celui qui dit qu'il sort hors de la tranchée pour charger sans peur est un menteur.";
            }

            if (NbMessage == 3)
            {
                TexteOnboarding.text = "1916, Verdun tue des miliers d'hommes, français et allemands. Ils voulaient nous saigner à blanc, c'est raté. Malgré tout, une autre bataille est sur le point de se dérouler : celle de la Somme, pour soulager les copains de Verdun. Cette bataille, c'est la mienne.";

                ImageFond2.SetActive(false);
                ImageFond3.SetActive(true);

                SonDiapo.SetActive(true);
            }

            if (NbMessage == 4)
            {
                TexteOnboarding.text = "Je me présente, Anatole Favourier, jeune capitaine du 122eme Régiment d'Infanterie de l'armée française. Je viens tout juste d'arriver sur le front ... Et ceci est mon histoire.";

                SonDiapo.SetActive(false);
            }

            if (NbMessage == 5)
            {
                SupportTexteOnboarding.SetActive(false);
                FirstSpeechDone = true;
                LancementPanelInfo = true;

                ImageFond3.SetActive(false);
                SonDiapo.SetActive(true);

            }
        }

        void ApparitionPanel()
        {
            PanelInfo.SetActive(true);

            FinPanelInfo = true;
        }
    }
}
