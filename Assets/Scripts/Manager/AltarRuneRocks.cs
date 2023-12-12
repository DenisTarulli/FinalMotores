using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarRuneRocks : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject fire1;
    [SerializeField] private GameObject fire2;

    public enum RuneRequired {greenRune, purpleRune, blueRune}

    [SerializeField] private RuneRequired runeRequired;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Interact()
    {
        if (runeRequired == RuneRequired.greenRune && gameManager.greenRune && !gameManager.eventActive)
        {
            fire1.SetActive(true);
            fire2.SetActive(true);
        }
        else if (runeRequired == RuneRequired.purpleRune && gameManager.purpleRune && !gameManager.eventActive)
        {
            fire1.SetActive(true);
            fire2.SetActive(true);
        }
        else if (runeRequired == RuneRequired.blueRune && gameManager.blueRune && !gameManager.eventActive)
        {
            fire1.SetActive(true);
            fire2.SetActive(true);
        }
    }
}
