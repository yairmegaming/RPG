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
    public void onDefense(int _damage)
    {
        _damage -= playerCurrentDefense;
        TakeDamage(_damage);
        Debug.Log("Enemy defended " + playerCurrentDefense + " damage.)"); 
    }
}
