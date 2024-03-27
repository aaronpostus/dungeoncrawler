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
    public Animator enemyAnimator;
    [SerializeField]public Text dialogueText;

    [SerializeField] private GameObject deathUI;

    private RhythmManager rhythmManager;
    private RhythmBattleManager rhythmBattleManager;


    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        rhythmBattleManager = RhythmBattleManager.instance;
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

        StartCoroutine(RhythmAttack(rhythmBattleManager.run));


        //SceneManager.LoadScene("DunGenTest");
    }

    public void OnAttackButton()
    {
        //Debug.Log(rhythmBattleManager.attack);

        /**if (state != BattleState.PLAYERTURN)
        {
            return;
        }*/
    

        StartCoroutine(RhythmAttack(rhythmBattleManager.attack));
    }

    public void OnStrongAttackButton()
    {
        /**if (state != BattleState.PLAYERTURN)
        {
            return;
        }*/

        StartCoroutine(RhythmAttack(rhythmBattleManager.strongAttack));
    }

    public void OnHealButton()
    {
        /**if (state != BattleState.PLAYERTURN)
        {
            return;
        }*/


        //StartCoroutine(PlayerHeal());
        StartCoroutine(RhythmAttack(rhythmBattleManager.heal));
    }

    IEnumerator PlayerAttack(bool strong)
    {
        //takes current score as damage
        float damage = rhythmManager.getDamage();
        Debug.Log("Damage = " + damage);
        animator.Play("Kick");
        yield return new WaitForSeconds(1.2f);

        // Damage enemy
        if (strong)
        {
            damage *= 2;
        }
        //Debug.Log(damage);
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
        rhythmBattleManager.DeactivateRhythmUI();


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

    IEnumerator RhythmAttack(string attackType)
    {
        if (!QueueManager.instance.queuePaused)
        {
            QueueManager.instance.addToQueue(attackType);
            if (!rhythmBattleManager.isRhythmUIActive())
            {
                //activates UI
                rhythmBattleManager.ActivateRhythmUI();

                //waits for one second for UI to load in
                while (rhythmManager == null)
                {
                    rhythmManager = RhythmManager.instance;
                    yield return null;
                }
            }
            else
            {
                //when starting, makes sure that the first note pressed is the first in the queue
                while (!rhythmManager.continuePlaying)
                {
                    yield return null;
                }
            }

            //creates notes based on difficulty - to be changed later
            rhythmManager.createAttack(attackType);


            yield return new WaitForSeconds(3f);

            while (rhythmManager.continuePlaying && !(state == BattleState.LOST || state == BattleState.WON))
            {
                if (attackType == rhythmManager.lastActionInQueue() && rhythmManager.isCurrentSequenceDone())
                {
                    Debug.Log("In while loop");
                    doAttack(attackType);
                }
                yield return null;
            }

            //added as rhythm section is complete so a full attack must have completed
            if (attackType == rhythmManager.lastActionInQueue() && rhythmManager.isCurrentSequenceDone())
            {
                Debug.Log("Out of while loop");
                doAttack(attackType);
            }

            //QueueManager.instance.Reset();
        }

    }

    private void doAttack(string attackType)
    {
        Debug.Log("Current attack is " + attackType);

        switch (attackType)
        {
            case var value when value == rhythmBattleManager.attack:
                StartCoroutine(PlayerAttack(false));
                break;
            case var value when value == rhythmBattleManager.strongAttack:
                StartCoroutine(PlayerAttack(true));
                break;
            case var value when value == rhythmBattleManager.heal:
                StartCoroutine(PlayerHeal());
                break;
            case var value when value == rhythmBattleManager.run:
                if (rhythmManager.getDamage() > 0)
                {
                    SceneManager.LoadScene("DunGenTest");
                }
                else
                {
                    Debug.Log("Queue Resumed");
                    QueueManager.instance.Reset();
                }
                break;
            default:
                break;
        }
    }
}
