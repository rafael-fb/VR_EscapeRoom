using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class PasswordPanel : MonoBehaviour
{
    public int[] numbers;
    public int[] password;

    public Animator unloackable;
    bool isUnlocked;


    private void Update()
    {
        if(gameObject.activeSelf && !isUnlocked)
        {
            if(numbers[0] == password[0])
            {
                if(numbers[1] == password[1])
                {
                    if(numbers[2] == password[2])
                    {
                        if(numbers[3] == password[3])
                        {
                            isUnlocked = true;

                            unloackable.Play("Open");
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
