using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughResourcesPopup : MonoBehaviour
{
    ClosePopUp closePopUp;
    public GameObject insufficientResourcesPU;

    private void Update()
    {
        closePopUp.AutomaticallyClosePopUp();
    }
}
