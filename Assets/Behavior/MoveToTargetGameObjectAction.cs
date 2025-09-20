using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using static AnimationConstants;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move to Target GameObject", story: "[Agent] moves to [TargetGameObject] .", category: "Action/Navigation", id: "f07a8fab1fc459315f3380eef35b2aa0")]
public partial class MoveToTargetGameObjectAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> TargetGameObject;

    private NavMeshAgent agent;
    private Animator animator;

    protected override Status OnStart()
    {
        if (!Agent.Value.TryGetComponent(out agent) || TargetGameObject.Value == null)
        {
            return Status.Failure;
        }

        animator = Agent.Value.GetComponentInChildren<Animator>();

        Vector3 targetPosition = GetTargetPosition();

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

        if (animator != null)
        {
            animator.SetFloat(SPEED, agent.velocity.magnitude);
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (animator != null)
        {
            animator.SetFloat(SPEED, 0);
        }
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition;
        if (TargetGameObject.Value.TryGetComponent(out Collider collider))
        {
            targetPosition = collider.ClosestPoint(agent.transform.position);
        }
        else
        {
            targetPosition = TargetGameObject.Value.transform.position;
        }

        return targetPosition;
    }
}


