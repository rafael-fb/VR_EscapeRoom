using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTarget : MonoBehaviour, Interactable
{

    public DoorTarget unloackable;
    bool isConditionTrue;
    public enum KeyName { senhaNumerica, tocha, runasElementais }
    public KeyName keyName;

    private int[] password = new int[] { 0, 1, 2, 3 };
    public int[] insertedPassword;

    // This function is called when the player look at this and touch the screen
    public void InteractAction()
    {
        #region Condition
        switch(keyName)
        {
            case KeyName.senhaNumerica:
                isConditionTrue = true;
                for (int i = 0; i < password.Length; i++)
                {
                    if(insertedPassword[i] != password[i])
                    {
                        isConditionTrue = false;
                    }
                }
                break;


            case KeyName.tocha:
                isConditionTrue = true;
                break;


            case KeyName.runasElementais:
                break;
        }
        #endregion

        if (isConditionTrue)
        {
            unloackable.UnloackAndOpen();
        }
    }
}
