using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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

    public void PopupShow()
    {
        animator.SetTrigger("Show");
    }

    public void PopupHide()
    {
        animator.SetTrigger("Hide");
    }
}
