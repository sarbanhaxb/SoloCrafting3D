using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class Worker : AbstractUnit, IBuildingBuilder
{
    private const string WAREHOUSE = "Warehouse";
    private const string BUILDINGSO = "BuildingSO";
    private const string GHOST = "Ghost";
    private const string TARGETLOCATION = "TargetLocation";
    private new const string COMMAND = "Command";

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
        graphAgent.SetVariableValue(WAREHOUSE, warehouse);
        graphAgent.SetVariableValue(COMMAND, UnitCommands.ReturnSupplies);
    }

    public void Gather(GatherableSupply supply)
    {
        graphAgent.SetVariableValue("Supply", supply);
        graphAgent.SetVariableValue("TargetGameObject", supply.gameObject);
        graphAgent.SetVariableValue(COMMAND, UnitCommands.Gather);
    }

    public GameObject Build(BuildingSO building, Vector3 targetLocation)
    {
        GameObject instance = Instantiate(building.Prefab, targetLocation, Quaternion.identity);
        if(instance.TryGetComponent(out BaseBuilding baseBuilding))
        {
            baseBuilding.ShowGhostVisuals();
        }
        else
        {
            Debug.LogError($"Missing BaseBuildng on Prefab for BuildingSO \"{building.name}\"! Cannot build!");
            return null;
        }

        graphAgent.SetVariableValue(BUILDINGSO, building);
        graphAgent.SetVariableValue(TARGETLOCATION, targetLocation);
        graphAgent.SetVariableValue(GHOST, instance);
        graphAgent.SetVariableValue(COMMAND, UnitCommands.BuildBuilding);

        return instance;
    }
    private void HandleGatherSupplies(GameObject self, int amount, SupplySO supply)
    {
        Bus<SupplyEvent>.Raise(new SupplyEvent(amount, supply));
    }
}