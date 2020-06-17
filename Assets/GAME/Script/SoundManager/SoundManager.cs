using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [EventRef] public String OpenInventoryEvent = "";
    [EventRef] public String CloseInventoryEvent = "";
    [EventRef] public String PutInInventoryEvent = "";
    [EventRef] public String NoteEvent = "";
    [EventRef] public String TelephoneEvent = "";
    [EventRef] public String ShowPanelInfoEvent = "";
    [EventRef] public String ChangeChapter = "";
    [EventRef] public String EndChapter = "";

    [EventRef] public String TelephoneRingEvent = "";
    [EventRef] public String DocEvent = "";
    [EventRef] public String PaperEvent = "";
    [EventRef] public String RapportEvent = "";
    [EventRef] public String OfficeThemeEvent = "";

    [EventRef] public String MaskEvent = "";
    [EventRef] public String BirdEvent = "";
    [EventRef] public String WatchEvent = "";
    [EventRef] public String CardGameEvent = "";
    [EventRef] public String TrenchThemeEvent = "";
}
