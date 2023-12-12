using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RuneRocks : MonoBehaviour, IInteractable
{
    [SerializeField] private FenceGate fenceGate;
    [SerializeField] private Spawner spawner;
    [SerializeField] private int eventEnemiesAmount;
    [SerializeField] private GateColour gateColour;

    public enum GateColour {GreenGate, PurpleGate, BlueGate}

    private GameManager gameManager;

    private bool interactable = true;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Interact()
    {
        if (interactable && !gameManager.eventActive)
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

        interactable = false;
        gameManager.eventActive = true;       

        for (int i = 0; i < eventEnemiesAmount; i++)
        {
            spawner.Spawn();
            yield return new WaitForSeconds(15f);
        }
    }

    private void EndEvent()
    {
        gameManager.eventActive = false;
        gameManager.killCounter = 0;
        
        Debug.Log($"Event completed, you got the {gateColour} rune!");
    }
}
