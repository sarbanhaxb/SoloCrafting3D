using UnityEngine;

public abstract class ActionBase : ScriptableObject, ICommand
{
    public abstract bool CanHandle(CommandContext context);
    public abstract void Handle(CommandContext context);

}
