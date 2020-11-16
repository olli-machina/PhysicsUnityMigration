using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public ForceGenerator2D forceGen = null;
    public Particle2DLink particleLink = null;
    public bool isForceGen = false, isParticleLink = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isForceGen)
            forceGen = gameObject.GetComponent<ForceGenerator2D>();
        if(isParticleLink)
            particleLink = gameObject.GetComponent<Particle2DLink>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > 70.0 || gameObject.transform.position.y < -70.0
            || gameObject.transform.position.x < -120.0 || gameObject.transform.position.x > 120.0) //change to screen height

            Destroy(gameObject);
    }

    public void SetVariables(GameObject projectile)
    {
        Particle2D info = projectile.GetComponent<Particle2D>();
        info.speed = 800.0f;
        info.Acceleration = new Vector3(100.0f, 0.0f, 0.0f);
        info.Velocity = projectile.transform.forward * info.speed;
        info.Velocity.z = 0.0f;
        info.DampingConstant = 0.99f;
    }
}
