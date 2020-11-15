using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator2D : MonoBehaviour
{
    bool shouldEffectAll = true;

    // Start is called before the first frame update
    void Start()
    {
        //particle = gameObject.GetComponent<Particle2D>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void UpdateForce(GameObject obj)//&?
    {
    }

    public void addForce(GameObject obj, Vector3 force)
    {
        obj.GetComponent<Particle2D>().AccumulatedForces += force;
    }
}

public class PointForceGenerator : ForceGenerator2D
{
    private Vector3 mPoint;
    private float mMagnitude;
    //public Particle2D particle;

    public void Constructor(Vector3 point, float magnitude)
    {
        mPoint = point;
        mMagnitude = magnitude;
    }

    //void Start()
    //{
    //    //particle = gameObject.GetComponent<Particle2D>();
    //}

    //void Update()
    //{

    //}

    public override void UpdateForce(GameObject obj)
    {
        Vector3 diff = mPoint - gameObject.transform.position; //???

        float range = 10;
        float rangeSQ = range * range;

        float dist = Vector3.Distance(mPoint, obj.transform.position);
        float distSQ = Mathf.Sqrt(dist);
            
        if(distSQ < rangeSQ)
        {
            dist = Vector3.Distance(mPoint, obj.transform.position);
                //GetComponent<Particle2DLink>().getLength(diff);
            float proportionAway = dist / range;
            proportionAway = 1 - proportionAway;
            diff.Normalize();

            //particle.AccumulatedForces += (diff * (mMagnitude * proportionAway));
            addForce(obj, (diff * (mMagnitude * proportionAway)) * Time.deltaTime); //will this work right?
        }
    }

}

//public class SpringForceGenerator : ForceGenerator2D
//{
//   public float springSpringConstant = 1.0f;
//   public float springRestLength = 20.0f;
//   public GameObject springSecondPoint;

//    void Start()
//   {

//   }

//   void Update()
//   {

//   }

//   override public void UpdateForce(GameObject obj, float dt)
//    {
//        float magnitude, dist;

//        Vector3 diff;

//        if (gameObject.activeInHierarchy == false || springSecondPoint.activeInHierarchy == false)//object no longer exists
//        {
//            return;
//        }

//        Vector3 pos = gameObject.transform.position;
//        Vector3 pos2 = springSecondPoint.transform.position;

//        diff = pos - pos2;
//        dist = GetComponent<Particle2DLink>().distanceBetween(pos, pos2);

//        magnitude = dist - springRestLength;
//        if (magnitude < 0.0f)
//            magnitude = -magnitude;

//        magnitude *= springSpringConstant;

//        diff.Normalize();
//        diff *= -magnitude;

//        //PhysicsEngine::addForce(pObj1, diff);
//        //PhysicsEngine::addForce(pObj2, Vector2D::getVectorInOppositeDirection(diff));
//    }
//}

//public class BungeeForceGenerator : ForceGenerator2D
//{
//   Vector3 ZERO_VECTOR3 = new Vector3(0.0f, 0.0f, 0.0f);

//   public Vector3 bungeeJumpOffPoint;
//   public float bungeeSpringConstant = 1.0f;
//   public float bungeeRestLength = 20.0f;

//   void Start()
//   {
//      bungeeJumpOffPoint = ZERO_VECTOR3;
//   }

//   void Update()
//   {

//   }

//   override public void UpdateForce(GameObject obj, float dt)
//   {
//      bungeeJumpOffPoint = gameObject.transform.position;

//      float magnitude, dist;

//      Vector3 diff;


//      if (gameObject.activeInHierarchy == false)//object no longer exists
//      {
//         return;
//      }

//      Vector3 pos = gameObject.transform.position;

//      diff = pos - bungeeJumpOffPoint;
//      dist = GetComponent<Particle2DLink>().distanceBetween(pos, bungeeJumpOffPoint);

//      if (dist <= bungeeRestLength)
//      {
//         return;
//      }

//      magnitude = dist - bungeeRestLength;
//      magnitude *= bungeeSpringConstant;

//      diff.Normalize();
//      diff *= magnitude * -1;

//      //PhysicsEngine::addForce(pObj1, diff);
//   }
//}

public class BuoyancyForceGenerator : ForceGenerator2D
{
    //private Vector3 mPoint;
    private float mMaxDepth, mVolume, mWaterHeight, mDensity;

    public void Constructor(float maxDepth, float volume, float waterHeight, float density)
    {
        Debug.Log("Start");
        mMaxDepth = maxDepth;
        mVolume = volume;
        mWaterHeight = waterHeight;
        mDensity = density;
    }

    public override void UpdateForce(GameObject obj)
    {
        //Vector3 diff = mPoint - gameObject.transform.position; //???
        float currentDepth = (obj.transform.position.y + mMaxDepth - mWaterHeight) / (2 * mMaxDepth);
        Debug.Log(mMaxDepth);
        Vector3 force = new Vector3(0.0f, 0.0f, 0.0f);

        if(currentDepth >= (mWaterHeight + mMaxDepth))
        {
            Debug.Log("Out of Water");
            return;
        }

        //if (currentDepth <= (mWaterHeight - mMaxDepth))
        //{
        //    Debug.Log("In Water");
        //    force.y = -1*((mDensity * mVolume * currentDepth));
        //}

        else
        {
            Debug.Log("Kinda in Water");
            force.y = -1*(mDensity * mVolume * currentDepth);
            //obj.GetComponent<Particle2D>().Acceleration = new Vector3(0.0f, 20.0f, 0.0f);
        }
       // Debug.Log("Force: " + force);

        addForce(obj, force * 0.5f); //* time?
        obj.GetComponent<Particle2D>().AccumulatedForces += force;
    }

    //public float buoyancyMaxDepth = 0.0f;
    //public float buoyancyVolume = 1.0f;
    //public float buoyancyWaterHeight = 0.0f;
    //public float buoyancyLiquidDensity = 1000.0f;
    //Particle2D particle;

    //void Start()
    //{
    //    buoyancyWaterHeight = GetComponent<getheightandwidth>().Get_Height(GameObject.FindGameObjectWithTag("Water"));
    //    particle = gameObject.GetComponent<Particle2D>();
    //}

    //void Update()
    //{

    //}

    //override public void UpdateForce(GameObject obj2, float dt)
    //{
    //    buoyancyMaxDepth = GetComponent<getheightandwidth>().Get_Height(gameObject);

    //    if (gameObject.activeInHierarchy == false)//object no longer exists
    //    {
    //        return;
    //    }
    //    Vector3 pos = gameObject.transform.position;

    //    //// Calculate the submersion depth.
    //    float depth = (pos.y + buoyancyMaxDepth - buoyancyWaterHeight) / (2 * buoyancyMaxDepth);

    //    // Check if we’re out of the water.
    //    if (depth <= 0)
    //    {
    //        return;
    //    }

    //    Vector3 force = new Vector3(0.0f, 0.0f, 0.0f);

    //    // Check if we’re at maximum depth.
    //    if (depth >= 1)
    //    {
    //        force.y = (-1 * (buoyancyLiquidDensity * buoyancyVolume));
    //    }
    //    else
    //    {
    //        force.y = (buoyancyLiquidDensity * buoyancyVolume * depth);
    //    }

    //    gameObject.GetComponent<Particle2D>().setDampingConstant(1.0f - depth);

    //    //// Otherwise we are partly submerged.
    //    //PhysicsEngine::addForce(pObj1, force * 0.5);

    //    particle.AccumulatedForces += force;
    //}
}
