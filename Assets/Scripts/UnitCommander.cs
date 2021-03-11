using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitCommander : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GoDestination(_inputHandler.DestinationPoint(),_inputHandler.GetSelectedUnits());
        }
    }
    public void GoDestination(Vector3 destPos, List<Unit> selectedUnits)
    {
        foreach (Unit unit in selectedUnits)
        {
            Debug.Log("hedefe gidiliyor");
            unit.SetDestinationPoint(destPos);
        }
    }
   

    
    
    
    
    
    
    
 

}
