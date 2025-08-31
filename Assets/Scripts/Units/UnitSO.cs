using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "Scriptable Units/UnitSO")]
public class UnitSO : ScriptableObject
{
    [field: SerializeField] public int Health { get; private set; } = 100;
}
