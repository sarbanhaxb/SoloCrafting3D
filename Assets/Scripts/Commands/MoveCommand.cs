using UnityEngine;

[CreateAssetMenu(fileName = "Move Action", menuName = "AI/Actions/Move", order = 100)]
public class MoveCommand : ActionBase
{
    [SerializeField] private float radiusMultipier = 3.5f;

    private int unitsOnLayer = 0;
    private int maxUnitsOnLayer = 1;
    private float circleRadius = 0;
    private float radialOffset = 0;

    public override bool CanHandle(CommandContext context)
    {
        return context.Commandable is AbstractUnit;
    }

    public override void Handle(CommandContext context)
    {
        AbstractUnit unit = (AbstractUnit)context.Commandable;

        if (context.UnitIndex == 0)
        {
            unitsOnLayer = 0;
            maxUnitsOnLayer = 1;
            circleRadius = 0;
            radialOffset = 0;
        }


        Vector3 targetPosition = new(
            context.Hit.point.x + circleRadius * Mathf.Cos(radialOffset * unitsOnLayer),
            context.Hit.point.y,
            context.Hit.point.z + circleRadius * Mathf.Sin(radialOffset * unitsOnLayer)
            );

        unit.MoveTo(targetPosition);
        unitsOnLayer++;

        if (unitsOnLayer >= maxUnitsOnLayer)
        {
            unitsOnLayer = 0;
            circleRadius += unit.AgentRadius * radiusMultipier;
            maxUnitsOnLayer = Mathf.FloorToInt(2 * Mathf.PI * circleRadius / (unit.AgentRadius * 2));
            radialOffset = 2 * Mathf.PI / maxUnitsOnLayer;
        }
    }
}
