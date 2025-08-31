using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ActionsUI : MonoBehaviour
{
    [SerializeField] private UIActionButton[] ActionButtons;
    private HashSet<AbstractCommandable> selectedUnits = new(12);

    private void Awake()
    {
        Bus<UnitSelectedEvent>.OnEvent += HandleUnitSelected;
        Bus<UnitDeselectedEvent>.OnEvent += HandleUnitDeselected;
    }

    private void Start()
    {
        foreach (UIActionButton actionButton in ActionButtons)
        {
            actionButton.Disable();
        }
    }

    private void HandleUnitSelected(UnitSelectedEvent evt)
    {
        if (evt.Unit is AbstractCommandable commandable)
        {
            selectedUnits.Add(commandable);
            RefreshButtons();
        }
    }

    private void HandleUnitDeselected(UnitDeselectedEvent evt)
    {
        if (evt.Unit is AbstractCommandable commandable)
        {
            selectedUnits.Remove(commandable);
            RefreshButtons();
        }
    }

    private void RefreshButtons()
    {
        HashSet<ActionBase> availableCommands = new(9);

        foreach (AbstractCommandable commandable in selectedUnits)
        {
            availableCommands.AddRange(commandable.AvailableCommands);
        }

        for (int i = 0; i < ActionButtons.Length; i++)
        {
            ActionBase actionForSlot = availableCommands.Where(action => action.Slot == i).FirstOrDefault();

            if (actionForSlot != null)
            {
                ActionButtons[i].EnableFor(actionForSlot, HandleClick(actionForSlot));
            }
            else
            {
                ActionButtons[i].Disable();
            }
        }
    }

    private UnityAction HandleClick(ActionBase action)
    {
        return () => Bus<ActionSelectedEvent>.Raise(new ActionSelectedEvent(action));
    }

    private void OnDestroy()
    {
        Bus<UnitSelectedEvent>.OnEvent -= HandleUnitSelected;
        Bus<UnitDeselectedEvent>.OnEvent -= HandleUnitDeselected;
    }
}