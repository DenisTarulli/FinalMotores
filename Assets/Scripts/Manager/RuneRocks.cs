using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneRocks : MonoBehaviour, IInteractable
{
    [SerializeField] private FenceGate fenceGate;
    [SerializeField] private GameObject fenceGateObj;

    public void Interact()
    {
        fenceGateObj.GetComponent<BoxCollider>().enabled = false;

        if (fenceGate.isOpen)
        {
            fenceGate.anim1.SetBool("Open", false);
            fenceGate.anim2.SetBool("Open", false);
        }
    }
}
