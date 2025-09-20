using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using static AnimationConstants;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move to Target Location", story: "[Agent] moves to [TargetLocation] .", category: "Action/Navigation", id: "d3d4e1c00c6f415e3d5fffc9abb3a81d")]
public partial class MoveToTargetLocationAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;

    private NavMeshAgent agent;
    private Animator animator;

    protected override Status OnStart()
    {

        if (!Agent.Value.TryGetComponent(out agent))
        {
            return Status.Failure;
        }

        //Agent.Value.TryGetComponent(out animator);
        animator = Agent.Value.GetComponentInChildren<Animator>();



        if (Vector3.Distance(agent.transform.position, TargetLocation.Value) <= agent.stoppingDistance)
        {
            return Status.Success;
        }

        agent.SetDestination(TargetLocation.Value);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(animator != null)
        {
            animator.SetFloat(SPEED, agent.velocity.magnitude);
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        animator.SetFloat(SPEED, 0);
    }
}

