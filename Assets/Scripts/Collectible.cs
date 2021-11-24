using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioSource recogerMadera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            recogerMadera.Play();
            StartCoroutine (DontDestroy());
        }
    }

   IEnumerator DontDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        GameManager.instance.woodCollected++;
    }
}
