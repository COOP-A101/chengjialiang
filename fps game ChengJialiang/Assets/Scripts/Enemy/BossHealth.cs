using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public GameObject box;
    protected override IEnumerator DeActivate()
    {
        agent.isStopped = true;
        agent.speed = 0;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        Manager.Instance.MonsterList.Remove(gameObject);
        GameObject.Instantiate(box,transform.position,Quaternion.identity);
    }
}
