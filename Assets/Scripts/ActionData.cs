using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionData", menuName = "Scriptable Objects/Action", order = 1)]
public class ActionData : ScriptableObject
{
    public string actionName;
    public int damage;
    public int timeUnits;
}
