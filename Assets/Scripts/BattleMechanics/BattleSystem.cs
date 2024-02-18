using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YaoLu;

public enum BattleSystemState { PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject mainCam;
    public GameObject battleCam;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;
    FSMCtrl control;
    Enemy enemyControl;

    public GameObject playerHUD;
    public Slider playerHP;
    public Slider enemyHP;

    public Animator animator;
    public Text dialogueText;

    public BattleSystemState state { get; set; }


    private Vector3 playerOriginalPosition;
    private Vector3 enemyOriginalPosition;

    void Start()
    {

        playerHUD.SetActive(false);
        //state = BattleSystemState.START;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Assuming 'SetEncounteredEnemy' was called before starting the battle scene
        StartCoroutine(SetupBattle());
    }

    public void SetEncounteredEnemy(GameObject encounteredEnemyPrefab)
    {
        enemyPrefab = encounteredEnemyPrefab; // Set the encountered enemy prefab
    }
    public void StartBattle()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {


        if (enemyPrefab != null)
        {
            mainCam.tag = "Untagged";
            battleCam.tag = "MainCamera";
            mainCam.SetActive(false);
            battleCam.SetActive(true);

            playerPrefab.transform.position = new Vector3(0, 0, 0);
            GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
            control = playerGO.GetComponent<FSMCtrl>();
            control.enabled = false;
            playerGO.transform.rotation = new Quaternion(0, 180, 0, 0);
            playerUnit = playerGO.GetComponent<Unit>();

            enemyPrefab.transform.position = new Vector3(0, 0, 0);
            NavMeshAgent navMesh = enemyPrefab.GetComponent<NavMeshAgent>();
            navMesh.enabled = false;
            //GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
            

            enemyPrefab.transform.position = enemyBattleStation.transform.position;
            enemyUnit = enemyPrefab.GetComponent<Unit>();
            enemyControl = enemyPrefab.GetComponent<Enemy>();
            enemyControl.enabled = false;
            dialogueText.text = "A wild " + enemyUnit.unitName + " approaches";

            playerHUD.SetActive(true);
            playerHP.maxValue = playerUnit.maxHP;
            enemyHP.maxValue = enemyUnit.maxHP;

            state = BattleSystemState.PLAYERTURN;

            yield return new WaitForSeconds(2f);

            
            PlayerTurn();
        }
        else
        {
            // Handle case where no enemy prefab is set
            dialogueText.text = "Error: No enemy encountered!";
            yield break; // Exit the coroutine early
        }
        
    }

    void PlayerTurn()
    {
        Debug.Log(state);
        dialogueText.text = "Choose an action:";
    }
    public void Run()
    {

    }

    public void OnAttackButton()
    {
        Debug.Log("Attack");
        Debug.Log(state);
        if (state != BattleSystemState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleSystemState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerAttack()
    {
        // Rhythm System attack
        float damage = RhythmAttack(); // Placeholder for method to get multiplier from rhythm system

        // Damage enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage * damage);
        enemyHP.value = enemyUnit.currentHP;
        animator.Play("Kick");
        // Set HUD
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        // Check if enemy is dead
        if (isDead)
        {
            // End battle
            state = BattleSystemState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            // Enemys turn
            state = BattleSystemState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        // Change state accordingly
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);
        // Set HUD
        dialogueText.text = "You were healed!";

        yield return new WaitForSeconds(2f);

        state = BattleSystemState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHP.value = playerUnit.currentHP;
        // Set HUD

        if (isDead)
        {
            state = BattleSystemState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleSystemState.PLAYERTURN;
            PlayerTurn();
        }
    }
    IEnumerator EndBattle()
    {
        if (state == BattleSystemState.WON)
        {
            dialogueText.text = "You won!";
            yield return new WaitForSeconds(2f);
        }
        else if (state == BattleSystemState.LOST)
        {
            dialogueText.text = "You were defeated...";
            yield return new WaitForSeconds(2f);
        }

        //battleCam.enabled = false;
        //mainCam.enabled = true;

        // Deactivate battle system UI or GameObject here
        this.gameObject.SetActive(false); // Assuming this script is attached to the battle system GameObject

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    float RhythmAttack()
    {
        // Implement rhythm attack logic
        return 1f; 
    }
}
