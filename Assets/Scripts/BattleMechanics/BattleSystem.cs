using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public enum StatusType { BURN, PARALYSIS, SLEEP }

public enum Level { FIRST, SECOND, THIRD, FOURTH }

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
    public Animator enemyAnimator;
    [SerializeField]public Text dialogueText;

    [SerializeField] private GameObject deathUI;

    public BattleState state;
    public StatusType attack1Type = StatusType.BURN;
    public StatusType attack2Type = StatusType.PARALYSIS;
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
        //GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerPrefab.GetComponent<Unit>();

       // GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyPrefab.GetComponent<Unit>();

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

    public void enemyScale(Level level)
    {
        switch (level)
        {
            case Level.SECOND:
                enemyUnit.damage = (int) (enemyUnit.damage * 1.2);
                enemyUnit.defense = (int)(enemyUnit.defense * 1.2);
                enemyUnit.maxHP = (int)(enemyUnit.maxHP * 1.2);
                enemyUnit.currentHP = (int)(enemyUnit.currentHP * 1.2);
                break;
            case Level.THIRD:
                enemyUnit.damage = (int)(enemyUnit.damage * 1.4);
                enemyUnit.defense = (int)(enemyUnit.defense * 1.4);
                enemyUnit.maxHP = (int)(enemyUnit.maxHP * 1.4);
                enemyUnit.currentHP = (int)(enemyUnit.currentHP * 1.4);
                break;
            case Level.FOURTH:
                enemyUnit.damage = (int)(enemyUnit.damage * 1.6);
                enemyUnit.defense = (int)(enemyUnit.defense * 1.6);
                enemyUnit.maxHP = (int)(enemyUnit.maxHP * 1.6);
                enemyUnit.currentHP = (int)(enemyUnit.currentHP * 1.6);
                break;
        }
    }

    void PlayerTurn()
    {

        Debug.Log("Player Turn began");
        dialogueText.text = "Choose an action:";


    }

    public void OnRunButton()
    {
        /**if (state != BattleState.PLAYERTURN)
        {
            return;
        }*/

        QueueManager.instance.addToQueue("Run");

        SceneManager.LoadScene("DunGenTest");
    }

    public void OnAttackButton()
    {
        Debug.Log("Attack");

        /**if (state != BattleState.PLAYERTURN)
        {
            return;
        }*/

        QueueManager.instance.addToQueue("Attack");

        StartCoroutine(RhythmAttack(false));
    }

    public void OnStrongAttackButton()
    {
        /**if (state != BattleState.PLAYERTURN)
        {
            return;
        }*/
        QueueManager.instance.addToQueue("Strong Attack");

        StartCoroutine(RhythmAttack(true));
    }

    public void OnHealButton()
    {
        /**if (state != BattleState.PLAYERTURN)
        {
            return;
        }*/

        QueueManager.instance.addToQueue("Heal");

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerAttack(bool strong, StatusType status)
    {
        System.Random rnd = new System.Random();
        //takes current score as damage
        float damage = RhythmManager.instance.damage;
        Debug.Log("Damage = " + damage);
        animator.Play("Kick");
        yield return new WaitForSeconds(1.2f);

        // Damage enemy
        if (strong)
        {
            damage *= 2;
        }
        Debug.Log(damage);
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage * damage);
        int rand = rnd.Next(100);
        if (enemyUnit.currentStatus == Status.HEALTHY)
        {
            switch (status)
            {
                case StatusType.BURN:
                    if (rand < 33)
                    {
                        enemyUnit.currentStatus = Status.BURN;
                    }
                    break;
                case StatusType.PARALYSIS:
                    if (rand < 33)
                    {
                        enemyUnit.currentStatus = Status.PARALYSIS;
                    }
                    break;
                case StatusType.SLEEP:
                    if (rand < 10)
                    {
                        enemyUnit.currentStatus = Status.SLEEP;
                    }
                    break;

            }
        }
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

        else if (state != BattleState.ENEMYTURN) 
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
        enemyAnimator.Play("Kick");
        dialogueText.text = enemyUnit.unitName + " attacks!";

        //good to test for now but should be changed later
        yield return new WaitForSeconds(2f);
        
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHP.value = playerUnit.currentHP;
        isDead = false;
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
        RhythmBattleManager.instance.DeactivateRhythmUI();


        if (state == BattleState.WON)
        {
            dialogueText.text = "You won!";
            enemyAnimator.Play("Die");
            SceneManager.LoadScene("DunGenTest");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated...";
            animator.Play("Die");
            if (playerHUD.activeSelf)
            {
                playerHUD.SetActive(false);
                Instantiate(deathUI);
            }
        }
        yield return new WaitForSeconds(3f);
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //playerHUD.SetActive(false);
    }

    IEnumerator RhythmAttack(bool difficulty)
    {
        if (!RhythmBattleManager.instance.isRhythmUIActive())
        {
            //activates UI
            RhythmBattleManager.instance.ActivateRhythmUI();

            //waits for one second for UI to load in
            yield return new WaitForSeconds(1f);
        }
        else
        {
            //when starting, makes sure that the first note pressed is the first in the queue
            while (!RhythmManager.instance.continuePlaying)
            {
                yield return null;
            }
        }

        //creates notes based on difficulty - to be changed later
        RhythmManager.instance.createDifficulty(difficulty);


        yield return new WaitForSeconds(3f);

        while (RhythmManager.instance.continuePlaying && !(state == BattleState.LOST || state == BattleState.WON))
        {
            if (RhythmManager.instance.isCurrentSequenceDone())
            {
                Debug.Log("Player is doing damage!");
                StartCoroutine(PlayerAttack(difficulty, attack1Type));
            }
            yield return null;
        }

        //added as rhythm section is complete so a full attack must have completed
        StartCoroutine(PlayerAttack(difficulty, attack1Type));

        yield return new WaitForSeconds(1f);

        if (RhythmBattleManager.instance.isRhythmUIActive())
        {
            RhythmBattleManager.instance.DeactivateRhythmUI();
        }

    }
}
