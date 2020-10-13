using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnimationTarget : MonoBehaviour, Interactable
{
    public Animator target;
    bool hasPlayed;
    public void InteractAction()
    {
        if(!hasPlayed)
        {
            hasPlayed = true;
            target.Play("Animation");
        }
    }
}
