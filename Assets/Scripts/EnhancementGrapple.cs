using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnhancementGrapple : MonoBehaviour
{
    private Camera camera;
    private Rigidbody2D player;
    private SpringJoint2D joint;
    private LineRenderer line;
    //public Transform transformSource;
    public LayerMask mask;
    public bool isGrappleEnabled;
    public float grappleCooldown;
    public float grappleLength;

    RaycastHit2D hit;
    private bool isGrappleReady = true;
    private bool grappleActive = false;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        player = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        if (isGrappleEnabled && isGrappleReady && !grappleActive && Input.GetMouseButtonDown(0))
        {
            Vector3 mp = Input.mousePosition;
            mp.z = 10;
            Vector3 force = (camera.ScreenToWorldPoint(mp) - player.transform.position).normalized * grappleLength;

            hit = Physics2D.Raycast(transform.position, force, grappleLength, mask);
            if (hit.collider != null && hit.collider.CompareTag("Environment"))
            {
                float len = (hit.point - (Vector2)transform.position).magnitude;
                joint.connectedBody = hit.collider.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point;
                joint.distance = .95f * len;

                line.SetPosition(1, hit.point);
                line.enabled = true;
                grappleActive = true;
                isGrappleReady = false;
                joint.enabled = true;
            }

        } 
        else if (grappleActive && Input.GetMouseButtonDown(0))
            {
                grappleActive = false;
                joint.enabled = false;
                line.enabled = false;
                StartCoroutine(GrappleCooldown());
            }
    }

    IEnumerator GrappleCooldown()
    {
        isGrappleReady = false;
        yield return new WaitForSeconds(grappleCooldown);
        isGrappleReady = true;
    }

}
