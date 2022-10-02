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
    public ParticleSystem m_ParticleSystem;
    public AudioSource m_AudioSource;


    public void SetupAppearance()
    {
        m_SpriteRenderer.sprite = m_ValuableObject.m_Sprite;
        m_Light.color = m_ValuableObject.m_LightColor;
        ParticleSystem.EmissionModule particleEmissionModule = m_ParticleSystem.emission;
        ParticleSystem.MainModule particleMainModule = m_ParticleSystem.main;
        particleEmissionModule.enabled = true;
        particleMainModule.startColor = m_ValuableObject.m_LightColor;
    }

    public void DecoupleEffects()
    {
        transform.DetachChildren();

        m_AudioSource.Play();
        m_ParticleSystem.Play();

        Destroy(m_AudioSource.gameObject, 3);
        Destroy(m_ParticleSystem.gameObject, 2);
    }
}
