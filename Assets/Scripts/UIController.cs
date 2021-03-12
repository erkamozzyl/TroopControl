using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text unitCounter;
    [SerializeField] private Selector _selector;

    void Update()
    {
        unitCounter.text = "Soldier : " +  _selector.GetSelectedUnits().Count.ToString() +"x" ;
    }

    public void ChangeSpeed(float newSpeed)
    {
        foreach ( Unit unit in _selector.allUnits)
        {
            unit.agent.speed = newSpeed;
        }
    }
    
    public void ChangeStopDistance(float newDistance)
    {
        foreach ( Unit unit in _selector.allUnits)
        {
            unit.agent.stoppingDistance = newDistance;
        }
        
    }
}
