using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool canOpen;
    private Animator anim;
    public GameObject canvas;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.name == "Player")
        {
            canvas.SetActive(true);
            canOpen = true;
        }

    } 

    private void Update()
    {
        if (canOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                canvas.SetActive(false);
                anim.SetBool("Open", true);
                StartCoroutine(GameSuccessCoroutine());
            }
        }
    }


    IEnumerator GameSuccessCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Manager.Instance.HandleTimeUp();
        anim.SetBool("Open", false);

    }
}
