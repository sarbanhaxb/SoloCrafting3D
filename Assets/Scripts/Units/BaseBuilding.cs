using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BaseBuilding : AbstractCommandable
{
    private const int MAX_QUEUE_SIZE = 5;
    private Queue<UnitSO> buildingQueue = new(MAX_QUEUE_SIZE);


    public void BuildUnit(UnitSO unit)
    {
        if (buildingQueue.Count == MAX_QUEUE_SIZE)
        {
            Debug.LogError("BuildUnit called when the queue was already full! This is not supported!");
            return;
        }


        buildingQueue.Enqueue(unit);
        if (buildingQueue.Count == 1)
        {
            StartCoroutine(DoBuildUnits());
        }
    }

    private IEnumerator DoBuildUnits()
    {
        while (buildingQueue.Count > 0)
        {
            UnitSO unit = buildingQueue.Peek();
            yield return new WaitForSeconds(unit.BuildTime);
            Instantiate(unit.Prefab, transform.position, Quaternion.identity);
            buildingQueue.Dequeue();
        }
    }
}