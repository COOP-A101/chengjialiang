using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    protected Animator animator;
    protected NavMeshAgent agent;

    public float health = 100;
    public float maxHealth;

    protected bool dead = false;

    public float Health { get => health; set => health = value; }
    public bool Dead { get => dead; set => dead = value; }

    public Image healthBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        maxHealth = health; 
        healthBar.fillAmount = health / maxHealth;
    }

    
    void Update()
    {
        if (health == 0 && !Dead)
        {
            Dead = true;
            animator.SetTrigger("Death");
            agent.isStopped = true;
            agent.speed = 0;
           Vector3 pos = gameObject.transform.position;
            pos.y = 0.8f;
            gameObject.transform.position = pos;
            StartCoroutine(DeActivate());
        }
    }

    public void ReduceHealth(float count)
    {
        if (health > 0)
        {
            float healthTemp = health - count;
            healthBar.fillAmount = healthTemp/ maxHealth;
            if (healthTemp < 0)
            {
                health = 0;
                agent.isStopped = true;
                agent.speed = 0;
               
            }
            else
            {
                health = healthTemp;
            }
        }
    }

    protected virtual IEnumerator DeActivate()
    {
        agent.isStopped = true;
        agent.speed = 0;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        Manager.Instance.MonsterList.Remove(gameObject);

        if (Manager.Instance.MonsterList.Count <= 0)
        {
            Manager.Instance.SwapMonster();
        }
    }
}
