using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Sons system
    [EventRef] public String OpenInventoryEvent = "";
    [EventRef] public String CloseInventoryEvent = "";
    [EventRef] public String PutInInventoryEvent = "";
    [EventRef] public String NoteEvent = "";
    [EventRef] public String TelephoneEvent = "";
    [EventRef] public String ShowPanelInfoEvent = "";
    [EventRef] public String ChangeSceneEvent = "";
    [EventRef] public String EndChapterEvent = "";

    //Sons Scene 1 Bureau
    [EventRef] public String TelephoneRingEvent = "";
    [EventRef] public String DocEvent = "";
    [EventRef] public String PaperEvent = "";
    [EventRef] public String RapportEvent = "";
    [EventRef] public String OfficeThemeEvent = "";

    //Sons Scene 2 Tranchée
    [EventRef] public String MaskEvent = "";
    [EventRef] public String BirdEvent = "";
    [EventRef] public String WatchEvent = "";
    [EventRef] public String CardGameEvent = "";
    [EventRef] public String GazWarningEvent = "";
    [EventRef] public String GazEvent = "";
    [EventRef] public String TrenchThemeEvent1 = "";
    [EventRef] public String TrenchThemeEvent2 = "";
    [EventRef] public String TrenchThemeEvent3 = "";

    //Sons voix soldats
    [EventRef] public String SoldierInteractionEvent = "";
    [EventRef] public String SoldierValidationEvent = "";
    [EventRef] public String SoldierRefuseEvent = "";
    [EventRef] public String SoldierExclamationEvent = "";
    [EventRef] public String SoldierSleepingEvent = "";
    [EventRef] public String SoldierWoundedEvent = "";
    [EventRef] public String SoldierSongEvent = "";

    //Sons End Game
    [EventRef] public String DeadByBulletEvent = "";
    [EventRef] public String BattlefieldThemeEvent = "";
    [EventRef] public String EndGameThemeEvent = "";

}
