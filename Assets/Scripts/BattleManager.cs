using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public UIActionPanelManager uiActionPanel;

    public List<UnitData> playerUnits;
    public List<UnitData> enemyUnits;
    public GameObject unitPrefab;
    public Transform unitParent;

    public List<UnitManager> units;


    public UnitManager activeUnit;


    public void Initialize()
    {
        //Instantiate and initialize all the player units
        foreach(UnitData ud in playerUnits)
        {
            UnitManager newUnit = GameObject.Instantiate(unitPrefab, this.unitParent).GetComponent<UnitManager>();
            newUnit.transform.position = new Vector3(units.Count * 5 - 2, -3, 0);
            newUnit.gameObject.name = (units.Count + 1) + "-" + ud.unitName;
            newUnit.Initialize(ud);
            newUnit.isPlayer = true;
            newUnit.teamID = 0;
            units.Add(newUnit);
        }

        foreach (UnitData ud in enemyUnits)
        {
            UnitManager newUnit = GameObject.Instantiate(unitPrefab, this.unitParent).GetComponent<UnitManager>();
            newUnit.transform.position = new Vector3((units.Count - playerUnits.Count)*5 - 2, 2, 0);
            newUnit.gameObject.name = (units.Count + 1 - playerUnits.Count) + "-" + ud.unitName; 
            newUnit.Initialize(ud);
            newUnit.teamID = 1;
            units.Add(newUnit);
        }

    }


    public IEnumerator StartRound()
    {
        //Debug.Log("Round Start");
        yield return new WaitForEndOfFrame();
        StartCoroutine(ResolveNextTurn());
    }



    public IEnumerator EndRound()
    {
        //Debug.Log("Round End");
        yield return new WaitForEndOfFrame();
        //Reduce the action cooldown for every unit
        foreach (UnitManager u in units)
        {
            u.AdjustActionCooldown(-1);
        }

        yield return new WaitForSeconds(1);
        //Start the next round???
        StartCoroutine(StartRound());
    }

    public IEnumerator ResolveNextTurn()
    {
        //Get any units that are able to act this turn
        List<UnitManager> unitsAbleToAct = units.FindAll(u => u.actionCooldown <= 0);

        //There is a unit, so have them take their turn
        if (unitsAbleToAct.Count > 0)
        {
            activeUnit = unitsAbleToAct[0];
            unitsAbleToAct.RemoveAt(0);

            //Have this unit take their turn
            yield return ResolveTurn(activeUnit);
        }
        else //No units left so end the round
        {

            StartCoroutine(EndRound());
        }
    }


    public IEnumerator ResolveTurn(UnitManager unit)
    {
        Debug.Log("Turn Start-" + unit.data.unitName);
        yield return new WaitForSeconds(1);

        //Check whether this is a player controlled or AI unit
        if (unit.isPlayer)
        {
            uiActionPanel.Initialize(unit.data.actions);

        }
        else //AI Controlled
        {
            //Pick a random action to use
            ActionData actionToUse = unit.data.actions[Random.Range(0, unit.data.actions.Count)];
            instance.StartCoroutine(instance.ResolveTurnAction(actionToUse));
        }
    }

    public static void ActionSelected(ActionData action)
    {
        //Hide UI Action Panel
        instance.uiActionPanel.HidePanel();

        //Should we make sure the action is valid???

        instance.StartCoroutine(instance.ResolveTurnAction(action));
    }

    public IEnumerator ResolveTurnAction(ActionData action)
    {
        //Pick a random target
        UnitManager target = GetRandomOpponent(activeUnit);

        //Apply damage from the attack
        yield return action.ResolveAction(activeUnit, target);

        //Adjust the action cooldown
        activeUnit.AdjustActionCooldown(action.timeUnits);

        //Clear active unit
        activeUnit = null;

        //Then start the next turn
        StartCoroutine(ResolveNextTurn());
    }


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Initialize();
        StartCoroutine(StartRound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public UnitManager GetRandomOpponent(UnitManager unit)
    {
        List<UnitManager> enemies = units.FindAll(u => u.teamID != unit.teamID);

        return enemies[Random.Range(0, enemies.Count)];
    }
}
