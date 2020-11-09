using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator2D : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //particle = gameObject.GetComponent<Particle2D>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void UpdateForce(GameObject obj1, float dt)//&?
    {
    }
}

public class SpringForceGenerator : ForceGenerator2D
{
   public float springSpringConstant = 1.0f;
   public float springRestLength = 20.0f;
   Particle2D particle;

    void Start()
   {

   }

   void Update()
   {
      
   }

   override public void UpdateForce(GameObject obj, float dt)
    {
        float magnitude, dist;

        Vector3 diff;

        if (gameObject.activeInHierarchy == false || obj.activeInHierarchy == false)//object no longer exists
        {
            return;
        }

        Vector3 pos = gameObject.transform.position;
        Vector3 pos2 = obj.transform.position;

        diff = pos - pos2;
        dist = GetComponent<Particle2DLink>().distanceBetween(pos, pos2);

        magnitude = dist - springRestLength;
        if (magnitude < 0.0f)
            magnitude = -magnitude;

        magnitude *= springSpringConstant;

        diff.Normalize();
        diff *= -magnitude;

        //PhysicsEngine::addForce(pObj1, diff);
        //PhysicsEngine::addForce(pObj2, Vector2D::getVectorInOppositeDirection(diff));
    }
   // void SpringupdateForce(GameObject pObj1, GameObject pObj2, float time)
   //{
   //   float magnitude, dist;

   //   Vector3 diff;

   //   if (pObj1.activeInHierarchy == false || pObj2.activeInHierarchy == false)//object no longer exists
   //   {
   //      return;
   //   }

   //   Vector3 pos = pObj1.transform.position;
   //   Vector3 pos2 = pObj2.transform.position;

   //   diff = pos - pos2;
   //   dist = GetComponent<Particle2DLink>().distanceBetween(pos, pos2);

   //   magnitude = dist - springRestLength;
   //   if (magnitude < 0.0f)
   //      magnitude = -magnitude;

   //   magnitude *= springSpringConstant;

   //   diff.Normalize();
   //   diff *= -magnitude;

   //   //PhysicsEngine::addForce(pObj1, diff);
   //   //PhysicsEngine::addForce(pObj2, Vector2D::getVectorInOppositeDirection(diff));
   //}
}

public class BungeeForceGenerator : ForceGenerator2D
{
   Vector3 ZERO_VECTOR3 = new Vector3(0.0f, 0.0f, 0.0f);

   public Vector3 bungeeJumpOffPoint;
   public float bungeeSpringConstant = 1.0f;
   public float bungeeRestLength = 20.0f;

   void Start()
   {
      bungeeJumpOffPoint = ZERO_VECTOR3;
   }

   void Update()
   {

   }

   void BungeeupdateForce(GameObject pObj1, float time)
   {
      bungeeJumpOffPoint = pObj1.transform.position;

      float magnitude, dist;

      Vector3 diff;


      if (pObj1.activeInHierarchy == false)//object no longer exists
      {
         return;
      }

      Vector3 pos = pObj1.transform.position;

      diff = pos - bungeeJumpOffPoint;
      dist = GetComponent<Particle2DLink>().distanceBetween(pos, bungeeJumpOffPoint);

      if (dist <= bungeeRestLength)
      {
         return;
      }

      magnitude = dist - bungeeRestLength;
      magnitude *= bungeeSpringConstant;

      diff.Normalize();
      diff *= magnitude * -1;

      //PhysicsEngine::addForce(pObj1, diff);
   }
}

public class BuoyancyForceGenerator : ForceGenerator2D
{
   public float buoyancyMaxDepth = 0.0f;
   public float buoyancyVolume = 1.0f;
   public float buoyancyWaterHeight = 0.0f;
   public float buoyancyLiquidDensity = 1000.0f;
   Particle2D particle;

    void Start()
   {
      buoyancyWaterHeight = GetComponent<getheightandwidth>().Get_Height(GameObject.FindGameObjectWithTag("Water"));
      particle = gameObject.GetComponent<Particle2D>();
    }

   void Update()
   {

   }

   public void BuoyancyForceGeneratorupdateForce(GameObject pObj1, float time)
   {
      buoyancyMaxDepth = GetComponent<getheightandwidth>().Get_Height(pObj1);

      if (pObj1.activeInHierarchy == false)//object no longer exists
      {
         return;
      }
      Vector3 pos = pObj1.transform.position;

      //// Calculate the submersion depth.
      float depth = (pos.y + buoyancyMaxDepth - buoyancyWaterHeight) / (2 * buoyancyMaxDepth);

      // Check if we’re out of the water.
      if (depth <= 0)
      {
         return;
      }

      Vector3 force = new Vector3(0.0f, 0.0f, 0.0f);

      // Check if we’re at maximum depth.
      if (depth >= 1)
      {
         force.y = (-1 * (buoyancyLiquidDensity * buoyancyVolume));
      }
      else
      {
         force.y = (buoyancyLiquidDensity * buoyancyVolume * depth);
      }

      pObj1.GetComponent<Particle2D>().setDampingConstant(1.0f - depth);

        //// Otherwise we are partly submerged.
        //PhysicsEngine::addForce(pObj1, force * 0.5);

        particle.AccumulatedForces += force;
   }
}
