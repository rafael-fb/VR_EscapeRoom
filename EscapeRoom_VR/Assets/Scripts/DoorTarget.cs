using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : MonoBehaviour, Interactable
{
    public GameObject passwordPanel;

    public void InteractAction()
    {
            passwordPanel.SetActive(true);
    }
}
