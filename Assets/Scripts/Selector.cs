using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField]public Camera mainCamera;
    public List<Unit> selectedUnits;
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private Unit[] allUnits;
    private List<Unit> highlightedUnits = new List<Unit>();
    private Unit previouslyHighlightedUnit;
    private float delay = 0.3f;
    private float timeWhenPressedRightMouseButton;
    private Vector3 rectangleStartPos;
    private Vector3 TL, TR, BL, BR;
    private Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    private bool hasClicked, isHoldingDown, hasReleased, isHovering;

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
      public void CheckInput()
    {
        hasClicked = false;
        isHoldingDown = false;
        hasReleased = false;
        isHovering = false;

        if (Input.GetMouseButtonDown(1))
        {
            timeWhenPressedRightMouseButton = Time.time;
            rectangleStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (Time.time - timeWhenPressedRightMouseButton <= delay )
            {
                hasClicked = true;
            }
            hasReleased = true;
        }
        else if (Input.GetMouseButton(1))
        {
            if (Time.time - timeWhenPressedRightMouseButton > delay)
            {
                isHoldingDown = true;
            }
        }
        else 
        {
            isHovering = true;
        }
    }
    public void InteractWithUnits()
    {
        if (hasClicked )
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                for (int i = 0; i < selectedUnits.Count; i++)
                {
                    selectedUnits[i].OnDropped();
                }
                selectedUnits.Clear();
            }
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f))
            {
                if (hit.collider.CompareTag("Unit"))
                {
                    Unit activeUnit = hit.transform.GetComponent<Unit>();
                    activeUnit.OnSelected();
                    if (!selectedUnits.Contains(activeUnit))
                    {
                        selectedUnits.Add(activeUnit);
                    }
                }
            }
        }
        if (isHoldingDown)
        {
            GenerateDisplayRectangleAndSelectionPolygon();
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                highlightedUnits.Clear();
            }
            foreach (Unit unit in allUnits)
            {
                if (IsWithinPolygon(unit.transform.position) )
                {
                    if (!highlightedUnits.Contains(unit))
                    {
                        highlightedUnits.Add(unit);
                    }
                }
                else if (!Input.GetKey(KeyCode.LeftShift))
                {
                   unit.OnDropped();
                }
            }
        }
        if (hasReleased)
        {
            selectionBox.gameObject.SetActive(false);
            if (highlightedUnits.Count > 0 )
            {
                if ( !Input.GetKey(KeyCode.LeftShift))
                {
                    selectedUnits.Clear();
                }
                foreach (Unit unit in highlightedUnits)
                {
                    unit.OnSelected();
                    selectedUnits.Add(unit);
                }
                highlightedUnits.Clear();
            }
        }
        ResetTryHighlightUnit();
        if (isHovering)
        {
            TryHighlightUnit();
        }
    }
    void TryHighlightUnit()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f))
        {
            if (hit.collider.CompareTag("Unit"))
            {
                Unit currentObj = hit.transform.GetComponent<Unit>();
                if (!selectedUnits.Contains(currentObj))
                {
                    previouslyHighlightedUnit = currentObj;
                }
            }
        }
    }
    private void ResetTryHighlightUnit()
    {
        if (previouslyHighlightedUnit != null)
        {
            if (!selectedUnits.Contains(previouslyHighlightedUnit))
            {
                previouslyHighlightedUnit = null;
            }
        }
    }
    bool IsWithinPolygon(Vector3 unitPos)
    {
        bool isWithinPolygon = false;
        //Triangle 1: TL - BL - TR
        if (IsWithinTriangle(unitPos, TL, BL, TR))
        {
            return true;
        }
        //Triangle 2: TR - BL - BR
        if (IsWithinTriangle(unitPos, TR, BL, BR))
        {
            return true;
        }

        return isWithinPolygon;
    }
    bool IsWithinTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        bool isWithinTriangle = false;

        //Need to set z -> y because of other coordinate system
        float denominator = ((p2.z - p3.z) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.z - p3.z));

        float a = ((p2.z - p3.z) * (p.x - p3.x) + (p3.x - p2.x) * (p.z - p3.z)) / denominator;
        float b = ((p3.z - p1.z) * (p.x - p3.x) + (p1.x - p3.x) * (p.z - p3.z)) / denominator;
        float c = 1 - a - b;

        //The point is within the triangle if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
        if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
        {
            isWithinTriangle = true;
        }

        return isWithinTriangle;
    }
    void GenerateDisplayRectangleAndSelectionPolygon()
    {
        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        //Get a corner coordinate of the rectangle, which is where the mouse currently is
        Vector3 rectangleEndPos = Input.mousePosition;

        //Calculate the middle position of the rectangle by using the two corners we have
        Vector3 middle = (rectangleStartPos + rectangleEndPos) / 2f;

        //Set the middle position of the GUI rectangle
        selectionBox.position = middle;

        //Calculate the size of the rectangle
        float sizeX = Mathf.Abs(rectangleStartPos.x - rectangleEndPos.x);
        float sizeY = Mathf.Abs(rectangleStartPos.y - rectangleEndPos.y);

        //Set the size of the square
        selectionBox.sizeDelta = new Vector2(sizeX, sizeY);

        //The problem is that the corners in the 2d rectangle is not the same as in 3d space
        //To get corners, we have to fire 4 rays from the screen and see where they hit the ground
        //These 4 corners will form a polygon, and we will see if a unit is within this polygon 
        float halfSizeX = sizeX * 0.5f;
        float halfSizeY = sizeY * 0.5f;

        Vector3 TL_screenSpace = new Vector3(middle.x - halfSizeX, middle.y + halfSizeY, 0f);
        Vector3 TR_screenSpace = new Vector3(middle.x + halfSizeX, middle.y + halfSizeY, 0f);
        Vector3 BL_screenSpace = new Vector3(middle.x - halfSizeX, middle.y - halfSizeY, 0f);
        Vector3 BR_screenSpace = new Vector3(middle.x + halfSizeX, middle.y - halfSizeY, 0f);

        //From screen to world
        Ray rayTL = mainCamera.ScreenPointToRay(TL_screenSpace);
        Ray rayTR = mainCamera.ScreenPointToRay(TR_screenSpace);
        Ray rayBL = mainCamera.ScreenPointToRay(BL_screenSpace);
        Ray rayBR = mainCamera.ScreenPointToRay(BR_screenSpace);

        float distanceToPlane = 0f;
        
        //Fire ray from camera to get the corners in world space
        if (groundPlane.Raycast(rayTL, out distanceToPlane))
        {
            TL = rayTL.GetPoint(distanceToPlane);
        }
        if (groundPlane.Raycast(rayTR, out distanceToPlane))
        {
            TR = rayTR.GetPoint(distanceToPlane);
        }
        if (groundPlane.Raycast(rayBL, out distanceToPlane))
        {
            BL = rayBL.GetPoint(distanceToPlane);
        }
        if (groundPlane.Raycast(rayBR, out distanceToPlane))
        {
            BR = rayBR.GetPoint(distanceToPlane);
        }

    }
}
