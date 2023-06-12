using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopUp : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private int timer;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        Debug.Log(isActive + " == current state of the bool");
        if (isActive)
        {
            isActive = false;
            animator.SetBool("Visible", true);
            StartCoroutine(AutomaticallyClosePopUp());
        }
        //Debug.Log("Coroutine started");
    }

    public IEnumerator AutomaticallyClosePopUp()
    {
        yield return new WaitForSeconds(timer);
        Invoke("DisablePopup", 1);
        animator.SetBool("Visible", false);
    }
    public void CloseButton()
    {
        Invoke("DisablePopup", 1);
        animator.SetBool("Visible", false);
    }

    void DisablePopup()
    {
        gameObject.SetActive(false);
        isActive = true;
    }
}
