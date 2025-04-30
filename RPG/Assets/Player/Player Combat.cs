using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player Manager")]
    public GameObject playerManagerObject;

    private PlayerManager playerManagerScript;

    private int playerCurrentDamage;   
    private int playerCurrentHealth;
    private int playerCurrentDefense;

    void Awake()
    {
        playerManagerScript = playerManagerObject.GetComponent<PlayerManager>();
    }
    void Start()
    {
        playerCurrentDamage = playerManagerScript.GetPlayerDamage();
        playerCurrentHealth = playerManagerScript.GetPlayerHealth();
        playerCurrentDefense = playerManagerScript.GetPlayerDefense();
        
        playerManagerScript.playerChoiceEnum = PlayerChoiceEnum.none;
    }

    // Used to get the current player stats while in combat
    public int GetCurrentPlayerDamage()
    {
        return playerCurrentDamage;
    }
    public int GetCurrentPlayerDefense()
    {
        return playerCurrentDefense;
    }
    public int GetCurrentPlayerHealth()
    {
        return playerCurrentHealth;
    }
    public int GetCurrentPlayerArmourPenetration()
    {
        return playerManagerScript.playerArmourPenetration;
    }

    public void RockChoice()
    {
        // Rock choice logic here
        playerManagerScript.playerChoiceEnum = PlayerChoiceEnum.Rock;
        Debug.Log("Player chose Rock");
    }
    public void PaperChoice()
    {
        // Paper choice logic here
        playerManagerScript.playerChoiceEnum = PlayerChoiceEnum.Paper;
        Debug.Log("Player chose Paper");
    }
    public void ScissorsChoice()
    {
        // Scissors choice logic here
        playerManagerScript.playerChoiceEnum = PlayerChoiceEnum.Scissors;
        Debug.Log("Player chose Scissors");
    }
    public void ResetChoices()
    {
        // Reset player choice logic here
        playerManagerScript.playerChoiceEnum = PlayerChoiceEnum.none;
        Debug.Log("Player choice reset");
    }

    public void TakeDamage(int _damage)
    {
        if (_damage > playerCurrentHealth)
        {
            // If damage is greater than current health, set health to 0
            playerCurrentHealth = 0;
            Debug.Log("Enemy took " + _damage + " damage. Current health: " + playerCurrentHealth);
        }
        else
        {
            // If damage is less than or equal to current health, subtract damage from health
            playerCurrentHealth -= _damage;
            Debug.Log("Enemy took " + _damage + " damage. Current health: " + playerCurrentHealth);
        }
    }
    public void Heal(int healAmount)
    {
        playerCurrentHealth += healAmount;
        Debug.Log("Enemy healed " + healAmount + " health. Current health: " + playerCurrentHealth);
    }
    public void onDefense(int _damage)
    {
        // Reduce damage taken by the player
        _damage -= Mathf.CeilToInt(playerCurrentDefense / 2f); // Reduce damage by half of the player's defense
        if (_damage <= 0)
        {
            _damage = 1; // Ensure at least 1 damage is taken
        }
        TakeDamage(_damage);
        Debug.Log("Enemy defended " + playerCurrentDefense + " damage.)"); 
    }
}
