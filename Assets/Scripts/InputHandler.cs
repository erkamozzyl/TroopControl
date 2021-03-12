using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Selector _selector;
    [SerializeField] private UnitCommander _unitCommander;
    [SerializeField] private TargetMovement _targetMovement;
    public bool isTargetSpecific;
    
    

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

        if (Input.GetMouseButton(0))
        {
            Ray ray = _selector.mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Target"))
            {
                _targetMovement = hit.transform.GetComponent<TargetMovement>();
                isTargetSpecific = true;

            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isTargetSpecific)
            {
                _unitCommander.GoDestination(_targetMovement.GetDestinationPos(),_selector.GetSelectedUnits());
                
                isTargetSpecific = false;
            }
            
        }

        _selector.CheckInput();
        _selector.InteractWithUnits();
    }

     

}
