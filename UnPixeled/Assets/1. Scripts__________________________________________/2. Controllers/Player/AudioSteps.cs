//Copyright Ex/IO 2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSteps : MonoBehaviour
{
    public void PlayerStep()
    {
        EventAudio.playerStepSound.Invoke();
    }
}
