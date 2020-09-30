using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : MonoBehaviour, Interactable
{
    bool isUnlocked;

    public void UnloackAndOpen()
    {
        isUnlocked = true;
        InteractAction();
    }

    // This function is called when the player look at this and touch the screen
    public void InteractAction()
    {
        if(isUnlocked)
        {
            // Unloack... SetActive(false)?... PlayAnimation()?...
        }
        else
        {
            // Feedback de que essa porta está trancada
        }
    }
}
