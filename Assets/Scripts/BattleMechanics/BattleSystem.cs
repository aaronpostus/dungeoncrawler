using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public GameObject playerHUD;
    public Slider playerHP;
    public Slider enemyHP;

    public Animator animator;

    [SerializeField]public Text dialogueText;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = " A wild " + enemyUnit.unitName + " approaches";

        playerHUD.SetActive(true);
        playerHP.maxValue = playerUnit.maxHP;
        enemyHP.maxValue = enemyUnit.maxHP;

        yield return new WaitForSeconds(4f);

        state = BattleState.PLAYERTURN;
        //DEBUG
        Debug.Log("Game setup complete");

        PlayerTurn();
    }

    void PlayerTurn()
    {

        Debug.Log("Player Turn began");
        dialogueText.text = "Choose an action:";


    }

    public void OnRunButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        SceneManager.LoadScene("DunGenTest");
    }

    public void OnAttackButton()
    {
        Debug.Log("Attack");
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
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
        //animator.Play("Kick");
        // Set HUD
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        // Check if enemy is dead
        if (isDead)
        {
            // End battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            // Enemys turn
            state = BattleState.ENEMYTURN;
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

        state = BattleState.ENEMYTURN;
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
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won!";
            SceneManager.LoadScene("DunGenTest");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated...";
            SceneManager.LoadScene("MainMenu");
        }
        yield return new WaitForSeconds(3f);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerHUD.SetActive(false);
    }

    float RhythmAttack()
    {
        float multiplier = 1;
        return multiplier;
    }



}
