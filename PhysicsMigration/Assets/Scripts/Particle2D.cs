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

    void setMass(float newMass)
    {
      Mass = newMass;
    }
    float getMass()
    {
      return Mass;
    }
    void setVelocity(Vector3 newVelocity)
    {
      Velocity = newVelocity;
    }
    Vector3 getVelocity()
    {
      return Velocity;
    }
    void setAcceleration(Vector3 newAcceleration)
    {
      Acceleration = newAcceleration;
    }
    Vector3 getAcceleration()
    {
      return Acceleration;
    }
    void setAccumulatedForces(Vector3 newAccumulatedForces)
    {
      AccumulatedForces = newAccumulatedForces;
    }
    Vector3 getAccumulatedForces()
    {
      return AccumulatedForces;
    }
    void setDampingConstant(float newDampingConstant)
    {
      DampingConstant = newDampingConstant;
    }
    float getDampingConstant()
    {
      return DampingConstant;
    }

   void integrator()
   {
      transform.position += (Velocity * Time.deltaTime);
      Vector3 resultingAcc = Acceleration;

      if (ShouldIgnoreForces == false)
      {
         resultingAcc = AccumulatedForces * (Mass * -1.0f);
      }

      Velocity += resultingAcc * Time.deltaTime;
      float damping = Mathf.Pow(DampingConstant, Time.deltaTime);
      Velocity *= damping;
   }
}
