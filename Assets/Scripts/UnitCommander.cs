using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitCommander : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<Unit> units;
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift))
        {
            Selection();
        }
        else if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            MultipleSelection();
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            GoDestination();
           
        }
    }

    public void Selection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Unit unit = hit.transform.gameObject.GetComponent<Unit>();
            if (hit.HasComponent<Unit>())
            {
                foreach (Unit _unit in units)
                {
                    _unit.OnDropped();
                }

                units.Clear();
                unit.OnSelected();
                units.Add(unit);
                Debug.Log("listeye eklendi");
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
            if (hit.HasComponent<Unit>())
            {
                   
                unit.OnSelected();
                units.Add(unit);
                Debug.Log("listeye eklendi");
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

    public void GoDestination()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            foreach (Unit unit in units)
            {
                Debug.Log("hedefe gidiliyor");
                unit.SetDestinationPoint(hit.point);
            }
        }
    }
}
