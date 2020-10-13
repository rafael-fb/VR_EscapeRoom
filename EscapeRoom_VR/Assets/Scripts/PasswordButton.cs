using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordButton : MonoBehaviour, Interactable
{
    public TextMesh textMesh;
    public int btnIndex;
    public enum ButtonType { Adds, Subtracts, Close}
    public ButtonType buttonType;

    public PasswordPanel passwordPanel;
    public void InteractAction()
    {
        if(buttonType == ButtonType.Close)
        {
            // is not working for some reason...
            passwordPanel.gameObject.SetActive(false);
        }
        else
        {
            if (buttonType == ButtonType.Adds)
            {
                if (passwordPanel.numbers[btnIndex] < 9)
                {
                    passwordPanel.numbers[btnIndex]++;
                }
                else
                {
                    passwordPanel.numbers[btnIndex] = 0;
                }
            }
            else
            {
                if (passwordPanel.numbers[btnIndex] > 0)
                {
                    passwordPanel.numbers[btnIndex]--;
                }
                else
                {
                    passwordPanel.numbers[btnIndex] = 9;
                }
            }
            textMesh.text = passwordPanel.numbers[btnIndex].ToString();
        }
    }
}
