using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class Worker : AbstractUnit
{
    private const string SUPPLY = "Supply";
    public void Gather(GatherableSupply supply)
    {
        graphAgent.SetVariableValue(SUPPLY, supply);
        graphAgent.SetVariableValue(TARGET_LOCATION, supply.transform.position);
        graphAgent.SetVariableValue(COMMAND, UnitCommands.Gather);
    }
}
