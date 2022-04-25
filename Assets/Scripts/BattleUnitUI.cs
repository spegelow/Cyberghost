using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUnitUI : MonoBehaviour
{
    public TMP_Text battleUnitUI;

    public void UpdateUI(UnitManager unitManager)
    {
        battleUnitUI.text = "" + unitManager.data.unitName + " Health " + unitManager.currentHealth + "/" + unitManager.data.baseHealth + "\nAction Cooldown: " + unitManager.actionCooldown;
    }
}
