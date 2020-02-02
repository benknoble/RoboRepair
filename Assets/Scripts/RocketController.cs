using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float impactRadius;
    public float impactForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var colliders = Physics2D.OverlapCircleAll(this.transform.position, impactRadius);

        foreach (var collider in colliders)
        {
            print(collider);
            if(collider?.gameObject == null)
            {
                continue;
            }
            
            if(collider.gameObject.tag.Equals("Player") || collider.gameObject.tag.Equals("Enemy"))
            {
                print("launching");
                var colliderRigidBody = collider.gameObject.GetComponent<Rigidbody2D>();
                var force = (colliderRigidBody.transform.position - this.transform.position).normalized * impactForce / Vector2.Distance(colliderRigidBody.transform.position, this.transform.position);
                if (collider.gameObject.tag.Equals("Enemy"))
                {
                    force *= 2f;
                }
                if (force.magnitude > impactForce)
                {
                    force = force.normalized * impactForce;
                    if (collider.gameObject.tag.Equals("Enemy"))
                    {
                        force *= 2f;
                    }
                }
                colliderRigidBody.AddForce(force);
            }
        }

        var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.zero;

        GetComponent<Animator>().SetTrigger("Hit");
        StartCoroutine(waitToDestroy());
    }

    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(.75f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
