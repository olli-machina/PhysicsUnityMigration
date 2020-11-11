using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{

   public float Mass = 0.0f;
  
   public Vector3 Velocity = new Vector3 (0.0f, 0.0f, 0.0f);
   public Vector3 Acceleration = new Vector3(0.0f, 0.0f, 0.0f);
   public Vector3 AccumulatedForces = new Vector3(0.0f, 0.0f, 0.0f);
   public float DampingConstant = 0.0f;//MUST BE NORMALIZED
   public bool ShouldIgnoreForces = true;
    public float inverseMass;

    // Start is called before the first frame update
    void Start()
    {
      float InverseMass = Mathf.Sqrt(Mass * -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
      //integrator();
    }

    public void setMass(float newMass)
    {
      Mass = newMass;
    }
    public float getMass()
    {
      return Mass;
    }
    public void setVelocity(Vector3 newVelocity)
    {
      Velocity = newVelocity;
    }
    public Vector3 getVelocity()
    {
      return Velocity;
    }
    public void setAcceleration(Vector3 newAcceleration)
    {
      Acceleration = newAcceleration;
    }
    public Vector3 getAcceleration()
    {
      return Acceleration;
    }
    public void setAccumulatedForces(Vector3 newAccumulatedForces)
    {
      AccumulatedForces = newAccumulatedForces;
    }
    public Vector3 getAccumulatedForces()
    {
      return AccumulatedForces;
    }
    public void setDampingConstant(float newDampingConstant)
    {
      DampingConstant = newDampingConstant;
    }
    public float getDampingConstant()
    {
      return DampingConstant;
    }
}
