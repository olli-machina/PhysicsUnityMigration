using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator : MonoBehaviour
{
    Particle2D particle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void integrator(GameObject obj)
    {
        particle = obj.GetComponent<Particle2D>();
        obj.transform.position += (particle.Velocity * Time.deltaTime);
        Vector3 resultingAcc = particle.Acceleration;

        if (!particle.ShouldIgnoreForces)
        {
            resultingAcc += particle.AccumulatedForces * (float)(particle.Mass / 1.0);
        }

        particle.Velocity += (resultingAcc * Time.deltaTime);
        float damping = Mathf.Pow(particle.getDampingConstant(), Time.deltaTime);
        particle.Velocity *= damping;

        particle.ClearAccumulatedForces();
    }
}
