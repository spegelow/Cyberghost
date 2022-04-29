using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public UnitData data;
    public int currentHealth;
    public int actionCooldown;

    public bool isPlayer;
    public int teamID;

    private bool isAlive;

    public BattleUnitUI unitUI;

    public void Initialize(UnitData data)
    {
        this.data = data;
        currentHealth = data.baseHealth;
        isAlive = true;
        actionCooldown = data.startingActionCooldown;
        unitUI.UpdateUI(this);
    }

    public void AdjustHealth(int amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, data.baseHealth);

        unitUI.UpdateUI(this);
    }

    public void AdjustActionCooldown(int timeUnits)
    {
        actionCooldown += timeUnits;
        unitUI.UpdateUI(this);
    }

    public void OnMouseDown()
    {
        BattleManager.UnitClicked(this);
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void HandleDeath()
    {
        isAlive = false;
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
