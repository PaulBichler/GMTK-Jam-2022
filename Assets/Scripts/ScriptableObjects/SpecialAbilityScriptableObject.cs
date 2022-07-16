using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New SpecialAbility", menuName = "SpecialAbility")]
public class SpecialAbilityScriptableObject : ScriptableObject
{
    public Color color = Color.white;
}