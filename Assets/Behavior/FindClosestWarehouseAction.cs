using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find Closest Warehouse", story: "[Unit] finds nearest [Warehouse] .", category: "Action/Units", id: "dc4aae7dbaa0f767c93a7ada357b2427")]
public partial class FindClosestWarehouseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Unit;
    [SerializeReference] public BlackboardVariable<GameObject> Warehouse;
    [SerializeReference] public BlackboardVariable<float> SearchRadius = new(10);
    [SerializeReference] public BlackboardVariable<UnitSO> WarehouseBuilding;

    protected override Status OnStart()
    {
        Collider[] colliders = Physics.OverlapSphere(Unit.Value.transform.position, SearchRadius.Value, LayerMask.GetMask("Buildings"));

        List<BaseBuilding> nearbyWarehouses = new();
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out BaseBuilding building)
                    && building.UnitSO.Equals(WarehouseBuilding.Value))
            {
                nearbyWarehouses.Add(building);
            }
        }
        if (nearbyWarehouses.Count == 0)
        {
            return Status.Failure;
        }

        Warehouse.Value = nearbyWarehouses[0].gameObject;

        return Status.Success;
    }
}

