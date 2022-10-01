using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : MonoBehaviour
{
    public Vector2 m_SpawnPosition;

    private int m_ElapsedTicks = 0;
    private const int RoundDuration = 50 * 10;
    public Transform m_Player;
    private ValuableObject[] m_ValuablePool;
    private ValuableObject m_CurrentValuable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_ElapsedTicks++;

        if (m_ElapsedTicks == RoundDuration)
        {
            m_ElapsedTicks = 0;
            ResetPlayerPosition();
            PickValuable();
            print("New round!");
        }
    }

    void PickValuable()
    {
        m_CurrentValuable = m_ValuablePool[Random.Range(0, m_ValuablePool.Length)];
    }

    void ResetPlayerPosition()
    {
        m_Player.position = m_SpawnPosition;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 150, 150), (m_ElapsedTicks / 50).ToString());
        GUI.Label(new Rect(0, 25, 150, 150), m_CurrentValuable.m_ValuableName);
    }
}
