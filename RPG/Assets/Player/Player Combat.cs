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

    public void TakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + playerCurrentHealth);
    }
    public void Heal(int healAmount)
    {
        playerCurrentHealth += healAmount;
        Debug.Log("Enemy healed " + healAmount + " health. Current health: " + playerCurrentHealth);
    }
    public void onDefense(int _damage, int _armourPenetration = 0)
    {
        // Reduce damage taken by the player
        _damage -= playerCurrentDefense - _armourPenetration;
        if (_damage <= 0)
        {
            _damage = 1; // Ensure at least 1 damage is taken
        }
        TakeDamage(_damage);
        Debug.Log("Enemy defended " + playerCurrentDefense + " damage.)"); 
    }

    public void BuffDamage(int _buffPercentageAmount)
    {
        playerCurrentDamage += (int)(playerCurrentDamage * _buffPercentageAmount / 100f);
        Debug.Log("Player buffed damage by " + _buffPercentageAmount + "%. Current damage: " + playerCurrentDamage);
    }
    public void BuffDefense(int _buffPercentageAmount)
    {
        playerCurrentDefense += (int)(playerCurrentDefense * _buffPercentageAmount / 100f);
        Debug.Log("Player buffed defense by " + _buffPercentageAmount + "%. Current defense: " + playerCurrentDefense);
    }
    public void DebuffDamage(int _debuffPercentageAmount)
    {
        playerCurrentDamage -= (int)(playerCurrentDamage * _debuffPercentageAmount / 100f);
        Debug.Log("Player debuffed damage by " + _debuffPercentageAmount + "%. Current damage: " + playerCurrentDamage);
    }
    public void DebuffDefense(int _debuffPercentageAmount)
    {
        playerCurrentDefense -= (int)(playerCurrentDefense * _debuffPercentageAmount / 100f);
        Debug.Log("Player debuffed defense by " + _debuffPercentageAmount + "%. Current defense: " + playerCurrentDefense);
    }
}
