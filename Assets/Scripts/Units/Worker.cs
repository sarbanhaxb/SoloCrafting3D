using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class Worker : AbstractUnit
{

    public bool HasSupplies
    {
        get
        {
            if (graphAgent != null && graphAgent.GetVariable("SupplyAmountHeld", out BlackboardVariable<int> heldVariable))
            {
                return heldVariable.Value > 0;
            }
            return false;
        }
    }

    protected override void Start()
    {
        base.Start();
        if (graphAgent.GetVariable("GatherSuppliesEvent", out BlackboardVariable<GatherSuppliesEventChannel> eventChannel))
        {
            eventChannel.Value.Event += HandleGatherSupplies;
        }
    }

    public void ReturnSupplies(GameObject warehouse)
    {
        graphAgent.SetVariableValue("Warehouse", warehouse);
        graphAgent.SetVariableValue("Command", UnitCommands.ReturnSupplies);
    }

    public void Gather(GatherableSupply supply)
    {
        graphAgent.SetVariableValue("Supply", supply);
        graphAgent.SetVariableValue("TargetGameObject", supply.gameObject);
        graphAgent.SetVariableValue("Command", UnitCommands.Gather);
    }
    private void HandleGatherSupplies(GameObject self, int amount, SupplySO supply)
    {
        Bus<SupplyEvent>.Raise(new SupplyEvent(amount, supply));
    }
}