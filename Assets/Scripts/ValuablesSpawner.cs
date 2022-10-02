using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ValuablesSpawner : MonoBehaviour
{
    public GameObject m_ValuablePrefab;
    public Transform[] m_SpawnTransforms;

    public int m_MinValuables;
    public int m_MaxValuables;

    public void Clear()
    {
        GameObject[] previousValuables = GameObject.FindGameObjectsWithTag("Valuable");
        foreach (GameObject obj in previousValuables)
        {
            Destroy(obj);
        }
    }

    public void Generate(ValuableObject[] valuablesPool, Dictionary<ValuableObject, int> requiredValuables)
    {
        int totalCount = Random.Range(m_MinValuables, m_MaxValuables) + CountRequiredValuables(requiredValuables);


        List<Vector2> defaultPositions = m_SpawnTransforms.ToList().ConvertAll(c => new Vector2(c.position.x, c.position.y));
        Queue<Vector2> availableSpawnPositions = new Queue<Vector2>(totalCount);
        for (int i = 0; i < totalCount; i++)
        {
            int rolledIndex = Random.Range(0, defaultPositions.Count);
            availableSpawnPositions.Enqueue(defaultPositions[rolledIndex]);
            defaultPositions.Remove(defaultPositions[rolledIndex]);
        }

        foreach (KeyValuePair<ValuableObject, int> pair in requiredValuables)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                SpawnValuable(pair.Key, availableSpawnPositions.Dequeue());
            }
        }

        for (int i = 0; i < availableSpawnPositions.Count; i++)
        {
            SpawnValuable(valuablesPool[Random.Range(0, valuablesPool.Length)], availableSpawnPositions.Dequeue());
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
