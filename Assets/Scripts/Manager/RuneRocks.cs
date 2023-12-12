using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneRocks : MonoBehaviour, IInteractable
{
    [SerializeField] private FenceGate fenceGate;
    [SerializeField] private GameObject fenceGateObj;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Spawner spawner;
    [SerializeField] private int eventEnemiesAmount;

    private bool eventCompleted = false;

    public void Interact()
    {
        if (!eventCompleted)
            StartCoroutine(StartEvent());
    }

    private void Update()
    {
        if (gameManager.killCounter == eventEnemiesAmount)
            EndEvent();
    }

    private IEnumerator StartEvent()
    {
        Debug.Log("Event started!");

        fenceGateObj.GetComponent<BoxCollider>().enabled = false;
        gameManager.eventActive = true;

        if (fenceGate.isOpen)
        {
            fenceGate.anim1.SetBool("Open", false);
            fenceGate.anim2.SetBool("Open", false);
        }

        for (int i = 0; i < eventEnemiesAmount; i++)
        {
            spawner.Spawn();
            yield return new WaitForSeconds(15f);
        }
    }

    private void EndEvent()
    {
        gameManager.eventActive = false;
        eventCompleted = true;
        gameManager.killCounter = 0;

        fenceGate.anim1.SetBool("Open", true);
        fenceGate.anim2.SetBool("Open", true);

        Debug.Log("Event completed!");
    }


}
