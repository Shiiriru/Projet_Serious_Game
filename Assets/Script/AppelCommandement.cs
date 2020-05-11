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

    // Start is called before the first frame update
    void Start()
    {
        SupportTexteOnboarding.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(OnBoarding.FirstSpeechDone == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SwitchTexteCmd();
                NbMessage += 1;
            }
        }
    }

    public void SwitchTexteCmd()
    {
        SupportTexteOnboarding.SetActive(true);

        if (NbMessage == 0)
        {
            TexteOnboarding.text = "Capitaine Favourier, bienvenue au front !Vous arrivez juste à temps avant la bataille !";
        }

        if (NbMessage == 1)
        {
            TexteOnboarding.text = "Cependant, je dois vous avouez que je ne suis pas forcément confiant envers vos capacités, vous débarquez direct de l'école et vous n'y connaissez rien au combat ...";
        }

        if (NbMessage == 2)
        {
            TexteOnboarding.text = "Nos troupes ici sont cruciales, et la bataille importante. Cependant, nous n'avons aucune visibilité sur votre situation. Il va falloir que vous nous fassiez le bilan vous même, ça va nous permettre de voir si vous êtes au point !";
        }

        if (NbMessage == 3)
        {
            TexteOnboarding.text = "Tout d'abord, veillez consulter la carte de bataille et prendre contact l'artillerie, ce serait dommage de ne pas profiter d'eux n'est ce pas ? Aussi, si vous sortez, n'oubliez pas de prendre la lettre de renseignement pour nos amis anglais.";
        }

        if (NbMessage == 4)
        {
            TexteOnboarding.text = "Quand vous serez prêts, recontactez moi ! Fin de transmission.";
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
