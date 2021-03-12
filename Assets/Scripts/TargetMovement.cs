using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
 
   
 
    void OnMouseDown()
    {
        var position = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(position);
 
        offset = position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z ));
     


    }
 
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
 
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(curPosition.x, 1, curPosition.z);
        

    }

    private void OnMouseExit()
    {
        GetDestinationPos();
        
    }

    public Vector3 GetDestinationPos()
    {
        return transform.position;
        
    }
}
