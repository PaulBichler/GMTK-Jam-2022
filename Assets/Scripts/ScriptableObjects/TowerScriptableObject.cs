using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class TowerScriptableObject : ScriptableObject
{
    [Title("IconSprite", bold: false)]
    [HideLabel]
    [PreviewField(100, ObjectFieldAlignment.Left)]
    [HorizontalGroup("row1", 150), VerticalGroup("row1/left")]
    public Sprite iconSprite;

    //[Title("SpecialAbility", bold: false)]
    //[HideLabel]
    //[PreviewField(100, ObjectFieldAlignment.Left)]
    //[HorizontalGroup("row1", 150), VerticalGroup("row1/right")]
    //public SpecialAbilityScriptableObject specialAbility;

    [Title("SpecialAbility", bold: false)]
    [HideLabel]
    public SpecialAbilityScriptableObject specialAbility;
    
    [Title("Type", bold: false)]
    [HideLabel]
    public TowerType type;
    
    [Title("Damage", bold: false)]
    [HideLabel]
    [Range(0.1f, 50.0f)]
    public float damage = 1;

    [Title("AttackRange", bold: false)]
    [HideLabel]
    [Range(0.1f, 50.0f)]
    public float attackRange = 1;

    [Title("AttackSpeed", bold: false)]
    [HideLabel]
    [Range(0.1f, 50.0f)]
    public float attackSpeed = 1;
    
    [Title("Projectile", bold: false)]
    [HideLabel]
    public ProjectileData projectileData;
    
    [Title("Stat Range (for Roll Tower)", bold: false)]
    [HideLabel]
    public StatRangeData statRangeData;

    [Title("ShopWeight", bold: false)]
    [HideLabel]
    [Range(0, 10)]
    public int shopWeight = 5;


    [Title("Description", bold: false)]
    [HideLabel]
    [MultiLineProperty(10)]
    public string description;
}

//public enum SpecialAbilityEnum { Basic, Fire, Ice, Lightning };

public enum TowerType
{
    Normal
}
