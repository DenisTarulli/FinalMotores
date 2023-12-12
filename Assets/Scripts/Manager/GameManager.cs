using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int killCounter;
    public bool eventActive;

    private void Start()
    {
        eventActive = false;
    }
}
