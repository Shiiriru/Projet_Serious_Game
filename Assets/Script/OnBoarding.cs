﻿using System.Collections;
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
    public GameObject PanelDate;

    float NbMessage = 0;

    public static bool FirstSpeechDone = false;
    public static bool LancementScene1 = false;
    private bool LancementPanelDate = false;
    private bool FinPanelDate = false;

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

        if (LancementPanelDate == true && conteur == 0)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
            {
                timer = timer - waitTime;
                ApparitionPanel();
                conteur += 1;
            }
        }

        if (FinPanelDate == true)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
            {
                timer = timer - waitTime;

                PanelDate.SetActive(false);
                FadeInScreen.SetTrigger("FirstSpeechDone");
                FinPanelDate = false;

                SonDiapo.SetActive(false);

                LancementScene1 = true;
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
                TexteOnboarding.text = "C'était aussi mon cas. J'étais instituteur dans une école de la région de la Marne. Malheuresment, il fallait des cadres pour gérer tout ce petit monde. Mes études m'ont permis d'etre officer, et que ce soit gérer des élèves ou des soldats, je suppose que c'est pareil.";

                SonDiapo.SetActive(false);
            }

            if (NbMessage == 2)
            {
                TexteOnboarding.text = "Trimbalé un peu partout de bureaux en bureaux, j'ai fini par me retrouver  à la tête d'une compagnie en première ligne, dans un coin paumé de la Somme, par un bel été 1916.";
            }

            if (NbMessage == 3)
            {
                TexteOnboarding.text = "Je me présente, Anatole Favourier, jeune capitaine du 366eme Régiment d'Infanterie de l'armée française. Je viens tout juste d'arriver sur le front ... Et ceci est mon histoire.";

                ImageFond2.SetActive(false);
                ImageFond3.SetActive(true);

                SonDiapo.SetActive(true);
            }

            if (NbMessage == 4)
            {
                SupportTexteOnboarding.SetActive(false);
                FirstSpeechDone = true;
                LancementPanelDate = true;

                ImageFond3.SetActive(false);

            }
        }

        void ApparitionPanel()
        {
            PanelDate.SetActive(true);

            FinPanelDate = true;
        }
    }
}
