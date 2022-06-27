using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public Transform player;
    public Transform[] checkpointPositions;
    public GameObject[] fights;
    public GameObject bossfight;
    public GameObject DeathScreen;
    public GameObject BossToken;
    public int currentCheckpoint = 0;
    public LayerMask bossLayer;
    void Start()
    {
        fights = GameObject.FindGameObjectsWithTag("Fight");
        bossfight= GameObject.FindGameObjectWithTag("BossFight");
    }
    public void Respawn()
    {
        foreach (GameObject fight in fights)
        {
            if (fight.gameObject.transform.position.x > checkpointPositions[currentCheckpoint].position.x)
            {
                    FightController fightController = fight.GetComponent<FightController>();
                    Debug.Log(fightController);
                    fightController.allEnemiesDied = false;
                    fightController.CountEnemies();
                    fightController.trigger.enabled = true;
                    if (fightController.Wall1 != null)
                    {
                        fightController.Wall1.enabled = fightController.Wall2.enabled = false;
                    }
                foreach (Transform enemy in fight.transform)
                {
                        EnemyAI enemyai = enemy.GetComponent<EnemyAI>();
                        enemy.gameObject.transform.position = enemyai.startPosition;
                        enemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
                        enemy.gameObject.transform.localScale = enemyai.startScale;
                        enemyai.isFlippedHorizontally = enemyai.StartIsFlippedHorizontally;
                        enemyai.isFacingRight = enemyai.StartIsFacingRight;
                        enemyai.isTriggered = false;
                        enemyai.currentState = enemyai.startState;
                        enemyai.isAttacking = false;
                        enemyai.isStunned = false;
                        enemyai.isCatchedBySP = false;
                        enemyai.currentStunTime = enemyai.stunTimer;
                        enemyai.StunState.SetActive(false);
                        enemyai.reactionRange = 0;
                        enemyai.distancePlayer = new Vector3(0f, 0f, 0f);
                        enemyai.recoverAfterAttack = false;
                        enemyai.currentAttackRecoverTime = enemyai.attackRecoverTime;
                        enemyai.BackToLife();
                }
            }
            BossFightController bossFightController = bossfight.GetComponent<BossFightController>();
            bossFightController.allEnemiesDied = false;
            bossFightController.CountEnemies();
            bossFightController.trigger.enabled = true;
            if (bossFightController.Wall1 != null)
            {
                bossFightController.Wall1.enabled = bossFightController.Wall2.enabled = false;
            }
            BossAI enemyboss = bossfight.transform.Find("Boss").GetComponent<BossAI>();
            enemyboss.gameObject.transform.position = enemyboss.startPosition;
            enemyboss.gameObject.transform.localScale = enemyboss.startScale;
            enemyboss.isFlippedHorizontally = enemyboss.StartIsFlippedHorizontally;
            enemyboss.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
            enemyboss.isFacingRight = enemyboss.StartIsFacingRight;
            enemyboss.isTriggered = false;
            enemyboss.currentState = enemyboss.startState;
            enemyboss.isAttacking = false;
            enemyboss.isStunned = false;
            enemyboss.isCatchedBySP = false;
            enemyboss.currentStunTime = enemyboss.stunTimer;
            enemyboss.StunState.SetActive(false);
            enemyboss.reactionRange = 0;
            enemyboss.distancePlayer = new Vector3(0f, 0f, 0f);
            enemyboss.recoverAfterAttack = false;
            enemyboss.currentAttackRecoverTime = enemyboss.attackRecoverTime;
            enemyboss.FireExplosion.SetActive(false);
            enemyboss.FireHand.SetActive(false);
            enemyboss.StormField.SetActive(false);
            enemyboss.currentHP = enemyboss.maxHP;
            enemyboss.respawned = true;
            enemyboss.isDead = false;
            enemyboss.currentPhase = 4 - enemyboss.currentHP;
            enemyboss.movementSpeed = enemyboss.startMovementSpeed;
            enemyboss.timeBetweenStormHit = enemyboss.startTimeBetweenStormHit;
            enemyboss.attackRecoverTime = enemyboss.startAttackRecoverTime;
            enemyboss.isShieldActive = true;
            enemyboss.Shield.SetActive(true);
            enemyboss.currentShieldTime = enemyboss.shieldTimer;
            Debug.Log("Boss zresetowany");
            Debug.Log("Udany reset");
        }
        StopCoroutine("StormShooting");
        BossToken.SetActive(false);
        player.GetComponent<PlayerController>().isDead = false;
        DeathScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.transform.position = checkpointPositions[currentCheckpoint].position;
        Time.timeScale = 1;
    }
}
