using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameRunner m_GameManager;

    [Header("Player Properties")] 
    public float m_Speed = 0.5f;

    public int m_Money = 0;

    public TMP_Text m_MoneyText;

    public Transform m_PlayerCamera;
    public Rigidbody2D m_Rigidbody;

    private Vector3 m_CurrentVelocity;

    public int Money => m_Money;
    void Start()
    {
        
    }


    void Update()
    {
        Vector3 cameraTargetPos = new Vector3(transform.position.x, transform.position.y, -10);
        m_PlayerCamera.position =
            Vector3.SmoothDamp(m_PlayerCamera.position, cameraTargetPos, ref m_CurrentVelocity, 0.2f, 15f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector2 currentTransform = this.transform.position;
        Vector2 targetPosition = new Vector2(xAxis * Time.deltaTime * m_Speed, yAxis * Time.deltaTime * m_Speed);

        currentTransform += targetPosition;
        //this.transform.position = currentTransform;
        m_Rigidbody.MovePosition(currentTransform);
        //m_Rigidbody.AddForce(new Vector2(xAxis * Time.deltaTime * m_Speed, yAxis * Time.deltaTime * m_Speed), ForceMode2D.);
        //m_Rigidbody.velocity = targetPosition;

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        Valuable val = col.GetComponent<Valuable>();

        val.DecoupleEffects();

        if (m_GameManager.IsExpectedValuable(val.m_ValuableObject))
        {
            m_GameManager.Collect(val.m_ValuableObject);
            UpdateMoney(m_Money + 120);
        }
        else
        {
            UpdateMoney(m_Money - 200);
        }

        Destroy(col.gameObject);
    }

    public void UpdateMoney(int value)
    {
        m_Money = value;
        m_MoneyText.text = $"$ {m_Money}";
    }
}
