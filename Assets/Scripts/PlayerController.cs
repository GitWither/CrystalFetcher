using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")] 
    public float m_Speed = 0.5f;

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
}
