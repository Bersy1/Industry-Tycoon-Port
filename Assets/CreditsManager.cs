using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{

    [SerializeField]
    private GameObject creditImage;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float creditMoveSpeed = 100;

    private void Update()
    {
        if (creditImage.activeInHierarchy == true)
        {
            CreditsActive();
        }


    }

    void CreditsActive()
    {
        if (creditImage.activeInHierarchy && creditImage.transform.position.y < 12.50f)
        {
            rb.velocity = new Vector2(rb.velocity.x, creditMoveSpeed * Time.deltaTime);
        }
        else if (creditImage.activeInHierarchy && creditImage.transform.position.y > 12.50f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            StartCoroutine(CreditsCounter());
        }
    }

    IEnumerator CreditsCounter()
    {
        yield return new WaitForSeconds(5f);
        creditImage.transform.position = new Vector2(0.0282f, -12.88f);
        StopCoroutine(CreditsCounter());
    }
}




