using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitCommander : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    [SerializeField] public List<Unit> selectedUnits;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GoDestination();
        } 
        
    }
   
    public void GoDestination()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            foreach (Unit unit in selectedUnits)
            {
                Debug.Log("hedefe gidiliyor");
                unit.SetDestinationPoint(hit.point);
            }
        }
    }
   

    
    
    
    
    
    
    
 

}
