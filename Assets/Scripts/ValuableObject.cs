using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Valuable", menuName = "ScriptableObjects/Valuable Object", order = 1)]
public class ValuableObject : ScriptableObject
{
    public string m_ValuableName;
    public float m_MoneyValue;
}
