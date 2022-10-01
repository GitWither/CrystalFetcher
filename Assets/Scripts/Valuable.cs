using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;

public class Valuable : MonoBehaviour
{
    public ValuableObject m_ValuableObject;
    public SpriteRenderer m_SpriteRenderer;
    public Light2D m_Light;


    public void SetupAppearance()
    {
        m_SpriteRenderer.sprite = m_ValuableObject.m_Sprite;
        m_Light.color = m_ValuableObject.m_LightColor;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
    }
}
