using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppelCommandement : MonoBehaviour
{
    public Text TexteOnboarding;

    public GameObject SupportTexteOnboarding;
    public GameObject Objets_Onboard;
    public GameObject Objets_Active;
    public GameObject MusiqueFond;

    float NbMessage = 0;

    bool CmdMessageStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        SupportTexteOnboarding.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(OnBoarding.FirstSpeechDone == true && CmdMessageStarted == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SwitchTexteCmd();
                NbMessage += 1;
            }
        }
    }

    public void StartTextCmd()
    {
        SwitchTexteCmd();
        NbMessage += 1;
        CmdMessageStarted = true;
    }

    public void SwitchTexteCmd()
    {
        SupportTexteOnboarding.SetActive(true);

        if (NbMessage == 0)
        {
            TexteOnboarding.text = "Capitaine Favourier, de la part du général Huguenot, bienvenue au front ! Vous arrivez à temps pour prendre le commandement de la 4eme compagnie du 122eme avant notre assaut sur Vermandovillers.";
        }

        if (NbMessage == 1)
        {
            TexteOnboarding.text = "Cependant, je dois vous avouez que je n'ai guère confiance envers vos capacités, vous débarquez direct depuis votre l'école et vous n'y connaissez rien au combat ...";
        }

        if (NbMessage == 2)
        {
            TexteOnboarding.text = "Nos troupes ici sont cruciales, et la bataille importante. Cependant, vous avez encore un peu de temps devant vous pour vous familiariser avec votre compagnie.";
        }

        if (NbMessage == 3)
        {
            TexteOnboarding.text = "Pour vous aidez, veillez consulter la carte de bataille puis mettez la à jour, prenez aussi contact avec l'artillerie la plus proche du régiment ! Aussi, si vous sortez, n'oubliez pas de prendre la lettre de renseignement pour nos amis anglais.";
        }

        if (NbMessage == 4)
        {
            TexteOnboarding.text = "Si vous rencontrez un problème, recontactez moi. Bon courage à vous !";
        }

        if (NbMessage == 5)
        {
            SupportTexteOnboarding.SetActive(false);

            Objets_Onboard.SetActive(false);
            Objets_Active.SetActive(true);

            MusiqueFond.SetActive(true);
        }
    }
}
