using UnityEngine;

public interface IGatherable
{
    public SupplySO Supply { get; }
    public int Amount { get; }
    public bool IsBusy { get; }

    public bool BeginGather();
    public int EndGather();
}
