using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(NavMeshAgent), typeof(BehaviorGraphAgent))]
public abstract class AbstractUnit : AbstractCommandable, IMoveable
{

    private const string TARGET_LOCATION = "TargetLocation";
    private const string COMMAND = "Command";

    public float AgentRadius => agent.radius;
    private NavMeshAgent agent;
    private BehaviorGraphAgent agentGraph;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agentGraph = GetComponent<BehaviorGraphAgent>();
        agentGraph.SetVariableValue(COMMAND, UnitCommands.Stop);
    }

    protected override void Start()
    {
        base.Start();
        Bus<UnitSpawnEvent>.Raise(new UnitSpawnEvent(this));
    }


    public void MoveTo(Vector3 position)
    {
        agentGraph.SetVariableValue(TARGET_LOCATION, position);
        agentGraph.SetVariableValue(COMMAND, UnitCommands.Move);
    }

    public void Stop()
    {
        agentGraph.SetVariableValue(COMMAND, UnitCommands.Stop);
    }
}