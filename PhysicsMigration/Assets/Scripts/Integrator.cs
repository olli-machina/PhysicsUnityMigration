using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
    public static Particle2D particle;
    public static GameObject 

    // Start is called before the first frame update
    static void Start()
    {
        particle = gameObject.GetComponent<Particle2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void integrator()
    {
        transform.position += (particle.Velocity * Time.deltaTime);
        Vector3 resultingAcc = particle.Acceleration;

        if (particle.ShouldIgnoreForces == false)
        {
            resultingAcc = particle.AccumulatedForces * (particle.Mass * -1.0f);
        }

        particle.Velocity += resultingAcc * Time.deltaTime;
        float damping = Mathf.Pow(particle.DampingConstant, Time.deltaTime);
        particle.Velocity *= damping;
    }
}
