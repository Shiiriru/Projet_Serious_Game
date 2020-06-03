using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;

public class OnBoarding : MonoBehaviour
{
    [SerializeField] GameObject ImageFond1;
    [SerializeField] GameObject ImageFond2;

    [SerializeField] GameObject PhotoCompagnie;
    [SerializeField] GameObject SpriteHappy;
    [SerializeField] GameObject SpriteSerious;

    [SerializeField] GameObject ButtonHappy;
    [SerializeField] GameObject ButtonSerious;

    public GameObject SonDiapo;

    public Text TexteOnboarding;
    public Animator FadeInScreen;

    public GameObject SupportTexteOnboarding;
    public GameObject PanelDate;

    float NbMessage = 0;

    public static bool FirstSpeechDone = false;
    public static bool LancementScene1 = false;
    private bool LancementPanelDate = false;
    private bool FinPanelDate = false;

    [SerializeField] DialogListManager dialogList;
    [SerializeField] DialogPlayer dialogPlayer;

    // Start is called before the first frame update
    void Start()
    {
        dialogPlayer.SetDialog(dialogList.dialogList[0]);
        dialogPlayer.onDialogFinished += OnboardingFinished;
    }

    private void OnboardingFinished() //fin de dialogue
    {
        SupportTexteOnboarding.SetActive(false);
        FirstSpeechDone = true;
        LancementPanelDate = true;

        PhotoCompagnie.SetActive(false);
        SpriteSerious.SetActive(false);
        SpriteHappy.SetActive(false);


        if (LancementPanelDate == true)
        {
            StartCoroutine(LaunchPanelInfo());
        }

        /*if (FinPanelDate == true)
        {
            Debug.Log("papon");
            StartCoroutine(LaunchEndOnboarding());
        }*/
    }

    IEnumerator LaunchPanelInfo()
    {
        yield return new WaitForSeconds(2f);
        ApparitionPanel();
    }

    void ApparitionPanel()
    {
        PanelDate.SetActive(true);

        FinPanelDate = true;

        if (FinPanelDate == true)
        {
            StartCoroutine(LaunchEndOnboarding());
        }
    }

    IEnumerator LaunchEndOnboarding()
    {
        yield return new WaitForSeconds(2f);

        PanelDate.SetActive(false);
        FadeInScreen.SetTrigger("FirstSpeechDone");
        FinPanelDate = false;

        SonDiapo.SetActive(false);

        LancementScene1 = true;
    }


    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwitchTexte();
            NbMessage += 1;
        }
    }*/

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
            TexteOnboarding.text = "C'était aussi mon cas. J'étais instituteur dans une école de la région de la Marne. Malheuresment, il fallait des cadres pour gérer tout ce petit monde. Mes études m'ont permis d'etre officer, et que ce soit gérer des élèves ou des soldats, je suppose que c'est pareil.";

            SonDiapo.SetActive(false);
        }

        if (NbMessage == 2)
        {
            TexteOnboarding.text = "Trimbalé un peu partout de bureaux en bureaux, j'ai fini par me retrouver  à la tête d'une compagnie en première ligne, dans un coin paumé de la Somme, par un bel été 1916.";
        }

        if (NbMessage == 3)
        {
            TexteOnboarding.text = "Avant de partir pour le front, on nous demandé de poser pour la photo, pour la postérité qu'ils disaient. La photo montre bien mon ressentit, j'étais ...";

            SonDiapo.SetActive(true);
        }

        if (NbMessage == 4)
        {
            TexteOnboarding.text = "Je me présente, Anatole Favourier, jeune capitaine du 366eme Régiment d'Infanterie de l'armée française. Je viens tout juste d'arriver sur le front ... Et ceci est l'histoire de moi et ma compagnie.";

            SonDiapo.SetActive(true);
        }

        if (NbMessage == 5)
        {
            

        }
    }

    public void afficherPhoto(int index)
    {
        ImageFond1.SetActive(false);
        PhotoCompagnie.SetActive(true);

        if (index == 0)
        {
            SpriteHappy.SetActive(true);
        }
        else
        {
            SpriteSerious.SetActive(true);
        }
    }
}
