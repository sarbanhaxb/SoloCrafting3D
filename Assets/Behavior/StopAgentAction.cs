using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using static AnimationConstants;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stop Agent", story: "[Agent] stops moving.", category: "Action/Navigation", id: "57054d5f0e125f600d146db133fd1fb7")]
public partial class StopAgentAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    protected override Status OnStart()
    {
        if (Agent.Value.TryGetComponent(out NavMeshAgent agent))
        {
            Animator animator = Agent.Value.GetComponentInChildren<Animator>();
            if(animator != null)
            {
                animator.SetFloat(SPEED, 0);
            }

            agent.ResetPath();
            return Status.Success;
        }
        return Status.Failure;
    }
}

