using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCtrl : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider col)
{
    if (col.CompareTag("Player"))
    {
        anim.SetBool("meet", true);
            Debug.Log("∞Ò¿Œ");
    }
}
}
