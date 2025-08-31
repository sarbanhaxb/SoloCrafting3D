using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AbstractUnit : AbstractCommandable, IMoveable
{
    private NavMeshAgent agent;
    public float AgentRadius { get => agent.radius; }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void Start()
    {
        base.Start();
        Bus<UnitSpawnEvent>.Raise(new UnitSpawnEvent(this));
    }


    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
