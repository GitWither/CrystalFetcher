using System.Collections;
using System.Collections.Generic;
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

    void Generate()
    {
        for (int i = 0; i < m_SpawnPositions.Length; i++)
        {
            Instantiate(m_ValuablePrefab, m_SpawnPositions[Random.Range(0, m_SpawnPositions.Length)], Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time % 80 == 0)
        {
            Generate();
        }
    }
}
