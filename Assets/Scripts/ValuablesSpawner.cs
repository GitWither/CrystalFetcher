using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ValuablesSpawner : MonoBehaviour
{
    public GameObject m_ValuablePrefab;
    public Vector2[] m_SpawnPositions;
    void Start()
    {
        
    }

    void Clear()
    {

    }

    public void Generate(ValuableObject[] valuablesPool, Dictionary<ValuableObject, int> requiredValuables)
    {
        GameObject[] previousValuables = GameObject.FindGameObjectsWithTag("Valuable");
        foreach (GameObject obj in previousValuables)
        {
            Destroy(obj);
        }

        int totalCount = Random.Range(1, 5) + CountRequiredValuables(requiredValuables);


        List<Vector2> defaultPositions = m_SpawnPositions.ToList();
        Queue<Vector2> availableSpawnPositions = new Queue<Vector2>(totalCount);
        for (int i = 0; i < totalCount; i++)
        {
            int rolledIndex = Random.Range(0, defaultPositions.Count - 1);
            availableSpawnPositions.Enqueue(defaultPositions[rolledIndex]);
            defaultPositions.Remove(defaultPositions[rolledIndex]);
        }

        print(availableSpawnPositions.Count);
        foreach (KeyValuePair<ValuableObject, int> pair in requiredValuables)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                SpawnValuable(pair.Key, availableSpawnPositions.Dequeue());
            }
        }
        print(availableSpawnPositions.Count);

        for (int i = 0; i < availableSpawnPositions.Count; i++)
        {
            SpawnValuable(valuablesPool[Random.Range(0, valuablesPool.Length - 1)], availableSpawnPositions.Dequeue());
        }
    }

    void SpawnValuable(ValuableObject obj, Vector2 position)
    {
        GameObject valuable = Instantiate(m_ValuablePrefab, position, Quaternion.identity);
        Valuable valuableComp = valuable.GetComponent<Valuable>();
        valuableComp.m_ValuableObject = obj;
        valuableComp.SetupAppearance();
    }

    int CountRequiredValuables(Dictionary<ValuableObject, int> valuables)
    {
        int result = 0;
        foreach (KeyValuePair<ValuableObject, int> valuable in valuables)
        {
            result += valuable.Value;
        }

        return result;
    }
}
