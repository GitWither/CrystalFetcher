using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")] 
    public float m_Speed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 currentTransform = this.transform.position;
        currentTransform += new Vector3(xAxis * Time.deltaTime * m_Speed, yAxis * Time.deltaTime * m_Speed, 0);
        this.transform.position = currentTransform;
    }
}
