using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public Mechanics Mechanic = Mechanics.Launching;
    public float launchingForce = 35f;

    [System.Serializable]
    public enum Mechanics
    {
        Lerping,
        Launching
    }
}

