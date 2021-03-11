using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<Unit> units;
    [SerializeField] private UnitCommander _unitCommander;

    private void Start()
    {
        units = _unitCommander.units;
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
                foreach (Unit _unit in units)
                {
                    _unit.OnDropped();
                }

                units.Clear();
                unit.OnSelected();
                if (!units.Contains(unit))
                {
                    units.Add(unit);
                }
            }
            else
            {
                foreach (Unit _unit in units)
                {
                    _unit.OnDropped();
                }

                units.Clear();
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
                if (!units.Contains(unit))
                {
                    units.Add(unit);
                }
                units.Add(unit);
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
