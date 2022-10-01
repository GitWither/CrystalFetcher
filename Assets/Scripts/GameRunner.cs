using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : MonoBehaviour
{

    private const int RoundDuration = 50 * 10; // 10 seconds
    private const int RoundCooldownDuration = 50 * 4; // 3 seconds

    public Vector2 m_SpawnPosition;



    private int m_ElapsedTicks = 0;
    private int m_RoundCooldown = 0;
    private bool m_RoundRunning = false;
    public Transform m_Player;
    public ValuableObject[] m_ValuablePool;
    private Dictionary<ValuableObject, int> m_CurrentValuables = new Dictionary<ValuableObject, int>();

    public ValuablesSpawner m_Spawner;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (m_RoundRunning)
            m_ElapsedTicks++;
        else
        {
            m_RoundCooldown++;
        }

        if (m_RoundCooldown == RoundCooldownDuration)
        {
            m_RoundCooldown = 0;
            m_RoundRunning = true;

            StartRound();
        }

        if (m_RoundRunning && (m_ElapsedTicks == RoundDuration || m_CurrentValuables.Count == 0))
        {
            m_ElapsedTicks = 0;
            m_RoundRunning = false;

            EndRound();
        }
    }

    void EndRound()
    {
        ResetPlayerPosition();
        ClearValuabes();
    }

    void StartRound()
    {
        PickValuable();

        m_Spawner.Generate(m_ValuablePool, m_CurrentValuables);
    }

    public bool IsExpectedValuable(ValuableObject obj)
    {
        return m_CurrentValuables.ContainsKey(obj);
    }

    void ClearValuabes()
    {
        m_CurrentValuables.Clear();
    }

    void PickValuable()
    {
        ClearValuabes();

        int valuableCount = Random.Range(1, 3);

        for (int i = 0; i < valuableCount; i++)
        {
            int count = Random.Range(1, 3);

            ValuableObject rolledValuableObject = m_ValuablePool[Random.Range(0, m_ValuablePool.Length)];

            while (m_CurrentValuables.ContainsKey(rolledValuableObject))
            {
                rolledValuableObject = m_ValuablePool[Random.Range(0, m_ValuablePool.Length)];
            }

            m_CurrentValuables.Add(rolledValuableObject, count);
        }

    }

    public void Collect(ValuableObject obj)
    {
        if (m_CurrentValuables[obj] - 1 == 0)
        {
            m_CurrentValuables.Remove(obj);
        }
        else
        {
            this.m_CurrentValuables[obj]--;
        }

    }

    void ResetPlayerPosition()
    {
        m_Player.position = m_SpawnPosition;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 150, 150), (m_ElapsedTicks / 50 + 1).ToString());
        GUI.Label(new Rect(0, 25, 150, 150), (m_RoundCooldown / 50 + 1).ToString());

        int yOffset = 50;
        foreach (KeyValuePair<ValuableObject, int> obj in m_CurrentValuables)
        {
            GUI.Label(new Rect(0, yOffset, 150, 150), obj.Key.m_ValuableName + " x" + obj.Value);
            yOffset += 25;
        }
    }
}
