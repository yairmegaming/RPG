using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Visuals")]
    public GameObject player;
    
    [Header("Player Combat")]
    public int playerDamage = 1;
    public int playerDefense = 0;
    public int playerHealth = 3;
    
    [Header("Player Score")]
    public int playerScore = 0;

    [Header("Player Items")]
    public GameObject Necklace;
    public GameObject Ring;
    public GameObject Amulet;

    private Necklace equippedNecklace;
    private Ring equippedRing;
    private Amulet equippedAmulet;

    public PlayerEnum playerEnum;
    public PlayerChoiceEnum playerChoiceEnum;
    
    private int playerModifiedDamage;
    private int playerModifiedDefense;
    private int playerModifiedHealth;
    private int playerCurrentScore;

    void Awake()
    {
        equippedNecklace = Necklace.GetComponent<Necklace>();
        equippedRing = Ring.GetComponent<Ring>();
        equippedAmulet = Amulet.GetComponent<Amulet>();
    }

    // Player Stats Section
    public int GetPlayerDamage()
    {
        return playerModifiedDamage = playerDamage + equippedNecklace.itemDamage + equippedRing.itemDamage + equippedAmulet.itemDamage;
    }
    public int GetPlayerDefense()
    {
        return playerModifiedDefense = playerDefense + equippedNecklace.itemDefense + equippedRing.itemDefense + equippedAmulet.itemDefense;
    }
    public int GetPlayerHealth()
    {
        return playerModifiedHealth = playerHealth + equippedNecklace.itemHealth + equippedRing.itemHealth + equippedAmulet.itemHealth;
    }

    // Score Section
    public int GetPlayerScore()
    {
        return playerCurrentScore;
    }
    public void AddToPlayerScore(int _score)
    {
        playerCurrentScore += _score;
    }
}
