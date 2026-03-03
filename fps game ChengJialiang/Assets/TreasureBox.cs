using System.Collections;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private bool canOpen;
    private Animator anim;
    public GameObject canvas;
    public bool isTriggered;

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
            if (Input.GetKeyDown(KeyCode.F) && !isTriggered)
            {
                isTriggered = true;
                canvas.SetActive(false);
                anim.SetBool("Open", true);
                StartCoroutine(GameSuccessCoroutine());
            }
        }
    }

    private IEnumerator GameSuccessCoroutine()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        Manager.Instance.AddBoxNum();
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        //anim.SetBool("Open", false);
    }
}