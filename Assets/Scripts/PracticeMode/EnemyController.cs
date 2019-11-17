using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpellManagerForEnemy manager;
    public GameObject target;
    public Animator enemyAnimator;
    public float rotationDamping;
    public float spellFireRate;
    private float nextTimeToCastASpell = 0f;
    private bool isDead = false;
    // Use this for initialization
    void Start()
    {
        manager = GetComponent<SpellManagerForEnemy>();

    }
    private void Update()
    {

        if (isDead == false)
        {
            if (target != null)
            {
                LookAtPlayer();

                if (Time.time >= nextTimeToCastASpell)
                {

                    nextTimeToCastASpell = Time.time + 1f / spellFireRate;
                    // Rolls a random number to decide which spell to fire;
                    float random = Random.Range(0f, 1f);

                    if (random > 0.5f)
                    {
                        enemyAnimator.SetBool("Expelliarmus", true);
                        enemyAnimator.SetBool("Idle", false);
                        Invoke("CastExpelliarmus", 0.95f);
                    }
                    if (random < 0.5f)
                    {
                        enemyAnimator.SetBool("Stupefy", true);
                        enemyAnimator.SetBool("Idle", false);
                        Invoke("CastStupefy", 0.65f);
                    }

                }

            }
        }
    }
    void CastExpelliarmus()
    {

        manager.CastExpelliarmus();
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("Expelliarmus", false);



    }
    void CastStupefy()
    {

        manager.CastStupefy();
        enemyAnimator.SetBool("Idle", true);
        enemyAnimator.SetBool("Stupefy", false);

    }
    void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }
    public void Die()
    {
        enemyAnimator.SetBool("Stupefy", false);
        enemyAnimator.SetBool("Expelliarmus", false);
        enemyAnimator.SetBool("Idle", false);
        enemyAnimator.SetBool("Die", true);
        isDead = true;
        Invoke("DestroySelf", 2f);

    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }



}
