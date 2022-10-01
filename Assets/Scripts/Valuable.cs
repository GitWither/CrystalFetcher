using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : MonoBehaviour
{
    public ValuableObject m_ValuableObject;
    public SpriteRenderer m_SpriteRenderer;


    public void SetupAppearance()
    {
        m_SpriteRenderer.color = m_ValuableObject.m_ValuableColor;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
    }
}
