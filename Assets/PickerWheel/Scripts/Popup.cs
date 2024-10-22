using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public void PopupOne()
    {
        transform.localScale = Vector3.one;
    }

    public void PopupZero()
    {
        transform.localScale = Vector3.zero;
    }

    public void PopupOpen()
    {
        gameObject.SetActive(true);
    }

    public void PopupClose()
    {
        gameObject.SetActive(false);
    }
}
