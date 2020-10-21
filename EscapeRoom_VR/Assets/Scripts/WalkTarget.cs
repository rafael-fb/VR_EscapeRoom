using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTarget : MonoBehaviour, Interactable
{

    CamPlayer player;
    bool shouldPlayerMove;

    // This function is called when the player look at this and touch the screen
    public void InteractAction()
    {
        player = FindObjectOfType<CamPlayer>();

        if(player.previousWalkTarget != null)
        {
            player.previousWalkTarget.shouldPlayerMove = false;
        }
        player.previousWalkTarget = this;
        shouldPlayerMove = true;
    }

    private void Update()
    {
        if(shouldPlayerMove)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, this.transform.position, 10f * Time.deltaTime);
            if(Mathf.Round(Vector3.Distance(player.transform.position, this.transform.position)) <= 1f && gameObject.activeSelf == true)
            {
                shouldPlayerMove = false;
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }

        }
    }
}
