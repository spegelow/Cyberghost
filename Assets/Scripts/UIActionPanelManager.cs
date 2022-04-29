using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionPanelManager : MonoBehaviour
{
    public GameObject actionButtonPrefab;

    public List<UIActionButton> actionButtons;

    public void Initialize(List<ActionData> actions)
    {
        //Destroy any existing buttons
        actionButtons.ForEach(ab => Destroy(ab.gameObject));
        actionButtons.Clear();
        //Create a new button for each action from the player grid
        foreach(ActionData a in actions)
        {
            UIActionButton newButton = GameObject.Instantiate(actionButtonPrefab, this.transform).GetComponent<UIActionButton>();
            newButton.Initialize(a);
            actionButtons.Add(newButton);
        }


        this.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}
