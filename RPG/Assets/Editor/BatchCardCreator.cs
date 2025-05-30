using UnityEngine;
using UnityEditor;
using System.IO;

public class BatchCardCreator
{
    [MenuItem("Tools/Batch Create 10 Cards (Buff/Debuff)")]
    public static void CreateCards()
    {
        string folderPath = "Assets/Cards";
        Directory.CreateDirectory(folderPath);

        var effectTypes = System.Enum.GetValues(typeof(CardEffectType));
        var statTypes = System.Enum.GetValues(typeof(CardStatType));
        var rarities = System.Enum.GetValues(typeof(ItemRarity));

        for (int i = 1; i <= 10; i++)
        {
            Card card = ScriptableObject.CreateInstance<Card>();
            card.cardID = $"card_{i}";
            card.cardName = $"Card {i}";
            card.cardType = CardType.Buff; // Or set as needed
            card.effectType = (CardEffectType)effectTypes.GetValue(Random.Range(0, effectTypes.Length));
            card.statType = (CardStatType)statTypes.GetValue(Random.Range(0, statTypes.Length));
            card.itemRarity = (ItemRarity)rarities.GetValue(Random.Range(0, rarities.Length));
            card.effectMultiplier = Random.value > 0.5f ? 1.1f : 0.9f; // +10% or -10%
            card.buffDuration = Random.Range(1, 6); // 1-5 turns
            card.isTemporaryBuff = true;
            card.cardDescription = $"{card.effectType} {card.statType} {(card.effectMultiplier > 1f ? "+10%" : "-10%")} for {card.buffDuration} turn(s).";

            AssetDatabase.CreateAsset(card, $"{folderPath}/Card_{i}.asset");
        }
        AssetDatabase.SaveAssets();
        Debug.Log("Created 10 cards with buff/debuff effects!");
    }
}