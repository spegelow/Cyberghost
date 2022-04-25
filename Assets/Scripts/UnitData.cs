using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/Unit", order = 1)]
public class UnitData : ScriptableObject
{
    public string unitName;
    public int baseHealth;
    public int startingActionCooldown;

    public List<ActionData> actions;
}
