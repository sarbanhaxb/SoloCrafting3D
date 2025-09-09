using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move to Target GameObject", story: "[Agent] moves to [TargetGameObject] .", category: "Action/Navigation", id: "98c9196703f061e908867d3d332a6da1")]
public partial class MoveToTargetGameObjectAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> TargetGameObject;

    private NavMeshAgent agent;

    protected override Status OnStart()
    {
        if (!Agent.Value.TryGetComponent(out agent))
        {
            return Status.Failure;
        }

        Vector3 targetPosition = TargetGameObject.Value.transform.position;
        if (Vector3.Distance(agent.transform.position, targetPosition) <= agent.stoppingDistance)
        {
            return Status.Success;
        }

        agent.SetDestination(targetPosition);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return Status.Success;
        }

        return Status.Running;
    }

}

