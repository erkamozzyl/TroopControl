using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Selector _selector;
    [SerializeField] private UnitCommander _unitCommander;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift))
        {
            _selector.SelectionByClick();
        }
        else if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            _selector.MultipleSelectionByClick();
        }
        if (Input.GetMouseButtonDown(0))
        {
            _unitCommander.GoDestination(DestinationPoint(),GetSelectedUnits());
        }
        
        _selector.CheckInput();
        _selector.InteractWithUnits();
    }

     public List<Unit> GetSelectedUnits()
     {
         return _selector.selectedUnits;
     }

     public Vector3 DestinationPoint()
     {
         Vector3 destPos = default;
         Ray ray = _selector.mainCamera.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;
         if (Physics.Raycast(ray, out hit))
         {
             destPos = hit.point;
         }

         return destPos;
     }
}
