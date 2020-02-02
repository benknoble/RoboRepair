using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnhancementController : MonoBehaviour
{
    public Camera camera;
    public Rigidbody2D player;
    public bool isRocketLauncherEnabled;
    public GameObject rocketObject;
    public int ammo;
    public float rocketSpeed;
    public float rocketLauncherCooldown;
    private bool isRocketLauncherReady = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isRocketLauncherEnabled && isRocketLauncherReady && Input.GetMouseButtonDown(1) && ammo > 0)
        {
            var mp = Input.mousePosition;
            mp.z = 10;
            var force = (camera.ScreenToWorldPoint(mp) - player.transform.position).normalized * rocketSpeed;

            var zRotation = Math.Atan2(force.y, force.x) * 180/Math.PI;
            print(zRotation);
            var playerRotation = player.transform.rotation;
            var rotation = Quaternion.Euler(0, 0, (float) zRotation);

            var rocket = Instantiate(rocketObject, player.transform.position, Quaternion.Euler(0, 0, (float)zRotation));
            rocket.SetActive(true);
            rocket.GetComponent<Rigidbody2D>().AddForce(force);
            Physics2D.IgnoreCollision(rocket.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            ammo -= 1;
            StartCoroutine(RocketCooldown());
        }
    }

    IEnumerator RocketCooldown()
    {
        isRocketLauncherReady = false;
        yield return new WaitForSeconds(rocketLauncherCooldown);
        isRocketLauncherReady = true;
    }
}
