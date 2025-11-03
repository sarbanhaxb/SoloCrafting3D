using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Build Building", story: "[Self] builds [BuildingSO] at [TargetLocation] .", category: "Action/Units", id: "c96b376d9e7b94e12804b098bee30c0e")]
public partial class BuildBuildingAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<BuildingSO> BuildingSO;
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;

    private float startBuildTime;
    private BaseBuilding completeBuilding;
    private Vector3 startPosition;

    protected override Status OnStart()
    {
        if(!HasValidInputs()) return Status.Failure;


        startBuildTime = Time.time;
        GameObject building = GameObject.Instantiate(BuildingSO.Value.Prefab);
        completeBuilding = building.GetComponent<BaseBuilding>();
        Renderer buildingRenderer = completeBuilding.MainRenderer;

        startPosition = TargetLocation.Value - Vector3.up * buildingRenderer.bounds.size.y;
        completeBuilding.transform.position = startPosition;
        return Status.Running;
    }

    private bool HasValidInputs()
    {
        return Self.Value != null && BuildingSO.Value != null && BuildingSO.Value.Prefab != null;
    }

    protected override Status OnUpdate()
    {
        float normalizedTime = (Time.time - startBuildTime) / BuildingSO.Value.BuildTime;
        completeBuilding.transform.position = Vector3.Lerp(startPosition, TargetLocation.Value, normalizedTime);
        return normalizedTime >= 1 ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        if(CurrentStatus == Status.Success)
        {
            completeBuilding.enabled = true;
        }
    }
}

