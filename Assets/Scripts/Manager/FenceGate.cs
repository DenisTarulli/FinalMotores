using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGate : MonoBehaviour, IInteractable
{
    public Animator anim1;
    public Animator anim2;

    public bool isOpen = false;
    private const string IS_OPEN = "Open";
    public void Interact()
    {
        if (!isOpen)
        {
            anim1.SetBool(IS_OPEN, true);
            anim2.SetBool(IS_OPEN, true);
            isOpen = true;
        }
        else
        {
            anim1.SetBool(IS_OPEN, false);
            anim2.SetBool(IS_OPEN, false);
            isOpen = false;
        }
    }
}
