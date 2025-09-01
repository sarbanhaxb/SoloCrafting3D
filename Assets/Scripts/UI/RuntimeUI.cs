using System;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeUI : MonoBehaviour
{
    [SerializeField] private ActionsUI actionsUI;
    private HashSet<AbstractCommandable> selectedUnits = new(12);
    private void Awake()
    {
        Bus<UnitSelectedEvent>.OnEvent += HandleUnitSelected;
        Bus<UnitDeselectedEvent>.OnEvent += HandleUnitDeselected;
    }

    private void HandleUnitDeselected(UnitDeselectedEvent evt)
    {
        if (evt.Unit is AbstractCommandable commandable)
        {
            selectedUnits.Remove(commandable);

            if (selectedUnits.Count > 0)
            {
                actionsUI.EnableFor(selectedUnits);
            }
            else
            {
                actionsUI.Disable();
            }
        }
    }


    private void HandleUnitSelected(UnitSelectedEvent evt)
    {
        if (evt.Unit is AbstractCommandable commandable)
        {
            selectedUnits.Add(commandable);
            actionsUI.EnableFor(selectedUnits);
        }
    }


    private void OnDestroy()
    {
        Bus<UnitSelectedEvent>.OnEvent -= HandleUnitSelected;
        Bus<UnitDeselectedEvent>.OnEvent -= HandleUnitDeselected;
    }
}
