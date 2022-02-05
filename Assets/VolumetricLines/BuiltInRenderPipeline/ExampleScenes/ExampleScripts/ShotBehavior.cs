using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{
    public GameObject collisionExplosion;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position += step * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            Destroy(gameObject);
        }
    }

}
