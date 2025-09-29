using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/GatherSuppliesEventChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "GatherSuppliesEventChannel", message: "[Self] gathers [Amount] [Supplies] .", category: "Events", id: "e47f298c9d6ad5f530fdbf4c6245e547")]
public sealed partial class GatherSuppliesEventChannel : EventChannel<GameObject, int, SupplySO> { }

