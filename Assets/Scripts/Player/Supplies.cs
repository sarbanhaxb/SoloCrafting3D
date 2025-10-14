using System;
using TMPro;
using UnityEngine;

public class Supplies : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI stoneText;

    [SerializeField] private SupplySO woodSO;
    [SerializeField] private SupplySO stoneSO;

    public static int Wood { get; private set; }
    public static int Gold { get; private set; }
    public static int Stone { get; private set; }

    private void Awake()
    {
        Bus<SupplyEvent>.OnEvent += HandleSupplyEvent;
    }
    private void OnDestroy()
    {
        Bus<SupplyEvent>.OnEvent -= HandleSupplyEvent;
    }

    private void HandleSupplyEvent(SupplyEvent evt)
    {
        if (evt.Supply.Equals(woodSO))
        {
            Debug.Log(Wood.ToString());

            Wood += evt.Amount;
            woodText.SetText(Wood.ToString());
        }
        else if (evt.Supply.Equals(stoneSO))
        {
            Stone += evt.Amount;
            stoneText.SetText(Stone.ToString());
        }
    }
}
