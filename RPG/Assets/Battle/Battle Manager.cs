using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    [Header("Battle Managers")]
    public GameObject playerManagerObject;
    public GameObject enemyManagerObject;
    
    [Header("Battle Units")]
    public GameObject player;
    public GameObject enemy;

    private PlayerManager playerManagerScript;
    private EnemyManager enemyManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
