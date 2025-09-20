using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class Worker : AbstractUnit
{
    private const string SUPPLY = "Supply";
    private const string TARGET_GAME_OBJECT = "TargetGameObject";
    public void Gather(GatherableSupply supply)
    {
        graphAgent.SetVariableValue(SUPPLY, supply);
        graphAgent.SetVariableValue(TARGET_GAME_OBJECT, supply.gameObject);
        graphAgent.SetVariableValue(COMMAND, UnitCommands.Gather);
    }
}
