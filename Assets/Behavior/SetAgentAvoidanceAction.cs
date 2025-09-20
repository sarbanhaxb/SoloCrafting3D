using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Agent Avoidance", story: "Set [Agent] avoidance quailty to [AvoidanceQuality] .", category: "Action/Navigation", id: "f7d7ea9bc3199b09b24949503c8a866c")]
public partial class SetAgentAvoidanceAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<int> AvoidanceQuality;

    protected override Status OnStart()
    {
        if (!Agent.Value.TryGetComponent(out NavMeshAgent agent) || AvoidanceQuality > 4 || AvoidanceQuality < 0)
        {
            return Status.Failure;
        }

        agent.obstacleAvoidanceType = (ObstacleAvoidanceType)AvoidanceQuality.Value;

        return Status.Success;
    }
}

