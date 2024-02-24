using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceUIScript : MonoBehaviour
{
    PlantPartFam origin;                 
    public void SetOrigin(PlantPartFam o) 
    { 
        origin = o;
        GrowButtonScript[] buttons = GetComponentsInChildren<GrowButtonScript>();
        foreach (GrowButtonScript button in buttons)
        {
            button.SetOrigin(o);
        }
    }

    [SerializeField] GrowButtonScript[] buttons;


    private void Start()
    {
        buttons[buttons.Length - 1].SetVisible(true);       //prunning button
    }

    void Update()
    {
        List<bool> affordances = new List<bool>(origin.GetAffordances());
        for(int i = 0; i<affordances.Count; i++)
        {
            buttons[i].SetVisible(affordances[i]);
        }
    }
}
