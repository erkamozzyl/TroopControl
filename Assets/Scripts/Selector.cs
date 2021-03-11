using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private Camera mainCamera;
    private List<Unit> selectedUnits;
    [SerializeField] private UnitCommander _unitCommander;

    private void Start()
    {
        selectedUnits = _unitCommander.selectedUnits;
        mainCamera = _unitCommander.mainCamera;
    }

    public void Selection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Unit unit = hit.transform.gameObject.GetComponent<Unit>();
            if (hit.transform.GetComponent<Unit>() != null)
            {
                foreach (Unit _unit in selectedUnits)
                {
                    _unit.OnDropped();
                }

                selectedUnits.Clear();
                unit.OnSelected();
                if (!selectedUnits.Contains(unit))
                {
                    selectedUnits.Add(unit);
                }
            }
            else
            {
                foreach (Unit _unit in selectedUnits)
                {
                    _unit.OnDropped();
                }

                selectedUnits.Clear();
            }

        }
    }
    public void MultipleSelection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Unit unit = hit.transform.gameObject.GetComponent<Unit>();
            if (hit.transform.GetComponent<Unit>() != null)
            {
                   
                unit.OnSelected();
                if (!selectedUnits.Contains(unit))
                {
                    selectedUnits.Add(unit);
                }
                selectedUnits.Add(unit);
            }
            /*    else
                {
                    foreach (Unit _unit in units)
                    {
                        _unit.OnDropped();
                    }
                    units.Clear();
                } */
        }
    }
}
