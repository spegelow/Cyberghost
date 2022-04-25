using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionButton : MonoBehaviour
{
    public ActionData action;
    public TMPro.TMP_Text buttonText;

    public void Initialize(ActionData action)
    {
        this.action = action;
        buttonText.text = action.actionName;
    }

    public void ButtonClicked()
    {
        BattleManager.ActionSelected(action);
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
