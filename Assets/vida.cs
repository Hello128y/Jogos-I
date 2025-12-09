using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class vida : MonoBehaviour

{
    public float boaa;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHeath>().RecuperarVida(boaa);
            Destroy(this.gameObject);
        }
    }
}
