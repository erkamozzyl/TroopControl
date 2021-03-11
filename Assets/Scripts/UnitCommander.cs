using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitCommander : MonoBehaviour
{
    public void GoDestination(Vector3 destPos, List<Unit> selectedUnits)
    {
        foreach (Unit unit in selectedUnits)
        {
            unit.SetDestinationPoint(destPos);
        }
    }
   

    
    
    
    
    
    
    
 

}
