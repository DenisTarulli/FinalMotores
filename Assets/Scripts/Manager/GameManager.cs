using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int killCounter;
    public bool eventActive;

    public bool greenRune;
    public bool purpleRune;
    public bool blueRune;

    private void Start()
    {
        eventActive = false;
    }
}
