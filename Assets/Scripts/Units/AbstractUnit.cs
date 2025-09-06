using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(NavMeshAgent), typeof(BehaviorGraphAgent))]
public abstract class AbstractUnit : AbstractCommandable, IMoveable
{

    protected const string TARGET_LOCATION = "TargetLocation";
    protected const string COMMAND = "Command";

    public float AgentRadius => agent.radius;
    private NavMeshAgent agent;
    protected BehaviorGraphAgent graphAgent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        graphAgent = GetComponent<BehaviorGraphAgent>();
        graphAgent.SetVariableValue(COMMAND, UnitCommands.Stop);
    }

    protected override void Start()
    {
        base.Start();
        Bus<UnitSpawnEvent>.Raise(new UnitSpawnEvent(this));
    }


    public void MoveTo(Vector3 position)
    {
        graphAgent.SetVariableValue(TARGET_LOCATION, position);
        graphAgent.SetVariableValue(COMMAND, UnitCommands.Move);
    }

    public void Stop()
    {
        graphAgent.SetVariableValue(COMMAND, UnitCommands.Stop);
    }
}