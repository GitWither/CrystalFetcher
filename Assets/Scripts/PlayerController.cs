using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameRunner m_GameManager;

    [Header("Player Properties")] 
    public float m_Speed = 0.5f;

    public int m_Money = 0;

    public Transform m_PlayerCamera;

    private Vector3 m_CurrentVelocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector2 currentTransform = this.transform.position;
        Vector2 targetPosition = new Vector2(xAxis * Time.deltaTime * m_Speed, yAxis * Time.deltaTime * m_Speed);

        currentTransform += targetPosition;
        this.transform.position = currentTransform;

        Vector3 cameraTargetPos = new Vector3(currentTransform.x, currentTransform.y, -10);
        m_PlayerCamera.position =
            Vector3.SmoothDamp(m_PlayerCamera.position, cameraTargetPos, ref m_CurrentVelocity, 0.5f, 5f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        Valuable val = col.GetComponent<Valuable>();
        if (m_GameManager.IsExpectedValuable(val.m_ValuableObject))
        {
            m_GameManager.Collect(val.m_ValuableObject);
            m_Money += 120;
        }
        else
        {
            m_Money -= 50;
        }

        Destroy(col.gameObject);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 75, 150, 150), $"{m_Money} $");
    }
}
