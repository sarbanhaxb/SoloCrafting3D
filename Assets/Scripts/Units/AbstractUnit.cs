using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(NavMeshAgent), typeof(BehaviorGraphAgent))]
public abstract class AbstractUnit : AbstractCommandable, IMoveable
{
    public float AgentRadius => agent.radius;
    private NavMeshAgent agent;
    private BehaviorGraphAgent agentGraph;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agentGraph = GetComponent<BehaviorGraphAgent>();
        MoveTo(transform.position);
    }

    protected override void Start()
    {
        base.Start();
        Bus<UnitSpawnEvent>.Raise(new UnitSpawnEvent(this));
        MoveTo(transform.position);
    }


    public void MoveTo(Vector3 position)
    {
        agentGraph.SetVariableValue("TargetLocation", position);
    }
}