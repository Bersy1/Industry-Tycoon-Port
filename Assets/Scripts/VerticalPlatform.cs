using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    //public float platformWaitTime = 0.5f;
    //private float timer;
    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }
    private void Update()
    {
        //if (Input.GetAxisRaw("Vertical") < 0f)
        //{
        //    if (Input.GetAxisRaw("Vertical") >= 0f)
        //    {
        //        timer = platformWaitTime;
        //    }
        //    if (timer <= 0)
        //    {
        //        effector.rotationalOffset = 180f;
        //        timer = platformWaitTime;
        //    }
        //    else
        //    {
        //        timer -= Time.deltaTime;
        //    }
        //}
        if (Input.GetAxisRaw("Vertical") < 0f)
        {
            effector.rotationalOffset = 180f;
        }
        if (Input.GetButton("Jump"))
        {
            effector.rotationalOffset = 0f;
        }
    }
}
