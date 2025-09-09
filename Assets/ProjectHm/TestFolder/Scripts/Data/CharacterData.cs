using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public int price;

    // StatTyprList와 StatValueList의 순서를 동일하게 맞춰놓을 것
    public List<StatType> bonusStatType;
    public List<float> bonusStatValue;

    public Sprite charSprite;
    public AnimatorOverrideController animator;
}