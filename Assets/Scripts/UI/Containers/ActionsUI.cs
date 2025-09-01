using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ActionsUI : MonoBehaviour, IUIElement<HashSet<AbstractCommandable>>
{
    [SerializeField] private UIActionButton[] ActionButtons;

    public void EnableFor(HashSet<AbstractCommandable> selectedUnits)
    {
        RefreshButtons(selectedUnits);
    }

    public void Disable()
    {
        foreach (UIActionButton button in ActionButtons)
        {
            button.Disable();
        }
    }

    private void RefreshButtons(HashSet<AbstractCommandable> selectedUnits)
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
}