using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopUp : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private int timer;
    public void Update()
    {
        Debug.Log(isActive + " == current state of the bool");
        if (isActive)
        {
            isActive = false;
            StartCoroutine(AutomaticallyClosePopUp());
        }
        //Debug.Log("Coroutine started");
    }

    public IEnumerator AutomaticallyClosePopUp()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
        isActive = true;
    }
    public void CloseButton()
    {
        gameObject.SetActive(false);
        if(!isActive)
            isActive = true;
    }
}
