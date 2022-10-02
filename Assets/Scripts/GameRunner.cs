using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameRunner : MonoBehaviour
{

    private const int RoundDuration = 50 * 10; // 10 seconds
    private const int RoundCooldownDuration = 50 * 4; // 3 seconds

    public Vector2 m_SpawnPosition;


    public PlayerController m_PlayerController;


    private int m_CurrentRound = 0;
    private int m_ElapsedTicks = 0;
    private int m_RoundCooldown = 0;
    private bool m_RoundRunning = false;
    private bool m_GameEnded = false;
    public Transform m_Player;
    public ValuableObject[] m_ValuablePool;
    private Dictionary<ValuableObject, int> m_CurrentValuables = new Dictionary<ValuableObject, int>();
    private Dictionary<ValuableObject, GameObject> m_PanelObjects = new Dictionary<ValuableObject, GameObject>();

    public ValuablesSpawner m_Spawner;

    public AudioSource m_RoundFinishedSource;
    public AudioSource m_RoundFailedSource;
    public AudioSource m_RoundStartSource;

    [Header("UI")]
    public Slider m_ProgressSlider;

    public TMP_Text m_CurrentRoundText;
    public TMP_Text m_CurrentSecondsText;

    public Image m_ProgressFill;
    public Sprite m_ProgressRound;
    public Sprite m_ProgressCooldown;

    public GameObject m_InfoPrefab;
    public GameObject m_NeededCrystalsPanel;
    public GameObject m_EndGameScreen;

    public Animator m_EndGameAnimation;



    void Start()
    {
        
    }

    void Update()
    {
        m_CurrentSecondsText.text = m_RoundRunning ? (10 - (m_ElapsedTicks / 50f)).ToString(CultureInfo.InvariantCulture) : Math.Floor(4 - m_RoundCooldown / 50f).ToString(CultureInfo.InvariantCulture);

        m_ProgressSlider.value = m_RoundRunning ? 1.0f - (m_ElapsedTicks / (float)RoundDuration) : 1.0f - (m_RoundCooldown / (float)RoundCooldownDuration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_PlayerController.Money < 0)
        {
            m_RoundRunning = false;
            m_GameEnded = true;
            m_EndGameScreen.SetActive(true);
            m_EndGameAnimation.SetTrigger("ShouldStart");
        }

        if (m_RoundRunning)
            m_ElapsedTicks++;
        else if (!m_RoundRunning && !m_GameEnded)
        {
            m_RoundCooldown++;
        }

        if (m_RoundCooldown == RoundCooldownDuration)
        {
            m_RoundCooldown = 0;
            m_RoundRunning = true;

            StartRound();

            m_ProgressFill.sprite = m_ProgressRound;
        }

        if (m_RoundRunning && (m_ElapsedTicks == RoundDuration || m_CurrentValuables.Count == 0))
        {
            m_ElapsedTicks = 0;
            m_RoundRunning = false;

            EndRound();

            m_ProgressFill.sprite = m_ProgressCooldown;
        }
    }

    void EndRound()
    {
        if (m_CurrentValuables.Count == 0)
        {
            m_RoundFinishedSource.Play();
        }
        else
        {
            m_PlayerController.UpdateMoney(m_PlayerController.Money - (200 * m_CurrentValuables.Count));
            m_RoundFailedSource.Play();
        }
        ResetPlayerPosition();
        ClearValuables();
        ClearValuablePanel();
    }

    void StartRound()
    {
        m_CurrentRound++;
        m_CurrentRoundText.text = $"ROUND: {m_CurrentRound}";
        PickValuable();
        m_RoundStartSource.Play();

        m_Spawner.Generate(m_ValuablePool, m_CurrentValuables);
    }

    public bool IsExpectedValuable(ValuableObject obj)
    {
        return m_CurrentValuables.ContainsKey(obj);
    }

    void ClearValuables()
    {
        m_CurrentValuables.Clear();
        m_Spawner.Clear();
    }

    void PickValuable()
    {
        ClearValuables();

        int valuableCount = Random.Range(1, Mathf.CeilToInt(m_CurrentRound / 2f));

        for (int i = 0; i < valuableCount; i++)
        {
            int count = Random.Range(1, 2);

            ValuableObject rolledValuableObject = m_ValuablePool[Random.Range(0, m_ValuablePool.Length)];

            while (m_CurrentValuables.ContainsKey(rolledValuableObject))
            {
                rolledValuableObject = m_ValuablePool[Random.Range(0, m_ValuablePool.Length)];
            }

            m_CurrentValuables.Add(rolledValuableObject, count);
            AddValuableToPanel(rolledValuableObject, count);
        }

    }

    void ClearValuablePanel()
    {
        foreach (var pair in m_PanelObjects)
        {
            Destroy(pair.Value);
        }

        m_PanelObjects.Clear();
    }

    void AddValuableToPanel(ValuableObject obj, int count)
    {
        GameObject info = Instantiate(m_InfoPrefab, m_NeededCrystalsPanel.transform);
        info.GetComponentInChildren<Image>().sprite = obj.m_Sprite;
        info.GetComponentInChildren<TMP_Text>().text = $"x{count}";
        m_PanelObjects.Add(obj, info);
    }

    public void Collect(ValuableObject obj)
    {
        if (m_CurrentValuables[obj] - 1 == 0)
        {
            m_CurrentValuables.Remove(obj);
            Destroy(m_PanelObjects[obj]);
            m_PanelObjects.Remove(obj);
        }
        else
        {
            this.m_CurrentValuables[obj]--;

            m_PanelObjects[obj].GetComponentInChildren<TMP_Text>().text = $"x{m_CurrentValuables[obj]}";
        }

    }

    void ResetPlayerPosition()
    {
        m_Player.position = m_SpawnPosition;
    }
}
