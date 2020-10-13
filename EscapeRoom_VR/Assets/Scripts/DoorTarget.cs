using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : MonoBehaviour, Interactable
{
    public GameObject passwordPanel;
    bool isPanelOpen;

    public void InteractAction()
    {
        if(!isPanelOpen)
        {
            isPanelOpen = true;
            passwordPanel.SetActive(true);
        }
    }
}
