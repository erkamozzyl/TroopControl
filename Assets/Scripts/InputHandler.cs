using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private BoxSelector _boxSelector;
    [SerializeField] private Selector _selector;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift))
        {
            _selector.Selection();
        }
        else if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            _selector.MultipleSelection();
        }
        _boxSelector.CheckInput();
        _boxSelector.InteractWithUnits();
    }
}
