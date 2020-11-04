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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      integrator();
    }

   static void integrator()
   {
      transform.position += (Velocity * Time.deltaTime);
      Vector3 resultingAcc = Acceleration;

      if(ShouldIgnoreForces == false)
      {
         resultingAcc = AccumulatedForces * (Mass * -1.0f);
      }

      Velocity += resultingAcc * Time.deltaTime;
      float damping = Mathf.Pow(DampingConstant * Time.deltaTime);
      Velocity *= damping;
   }
}
