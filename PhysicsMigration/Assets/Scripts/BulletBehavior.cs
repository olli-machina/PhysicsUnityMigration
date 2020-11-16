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
            || gameObject.transform.position.x < -120.0 || gameObject.transform.position.x > 120.0)

            Destroy(gameObject);
    }

    public void SetVariables(GameObject projectile, GameObject gun)
    {
        Particle2D info = projectile.GetComponent<Particle2D>();
        info.speed = 200.0f;
        info.Acceleration = new Vector3(0.0f, -5.0f, 0.0f);
        Vector3 dir = new Vector3((float)Mathf.Cos(gun.transform.rotation.z), (float)Mathf.Sin(gun.transform.rotation.z), 0.0f);
        info.Velocity = dir * info.speed;
        info.Velocity.z = 0.0f;
        Debug.Log(dir);
        info.DampingConstant = 0.99f;
    }
}
