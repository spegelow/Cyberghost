using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public UnitData data;
    public int currentHealth;
    public int actionCooldown;

    public bool isPlayer;

    public void Initialize(UnitData data)
    {
        this.data = data;
        currentHealth = data.baseHealth;
        actionCooldown = data.startingActionCooldown;
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
