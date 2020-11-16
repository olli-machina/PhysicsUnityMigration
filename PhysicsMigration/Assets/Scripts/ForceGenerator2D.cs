using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator2D : MonoBehaviour
{
    bool shouldEffectAll = true;

    // Start is called before the first frame update
    void Start()
    {

	}


    public virtual void UpdateForce(GameObject obj)
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

    public void Constructor(Vector3 point, float magnitude)
    {
        mPoint = point;
        mMagnitude = magnitude;
    }

    public override void UpdateForce(GameObject obj)
    {
        Vector3 diff = mPoint - gameObject.transform.position;

        float range = 10;
        float rangeSQ = range * range;

        float dist = Vector3.Distance(mPoint, obj.transform.position);
        float distSQ = Mathf.Sqrt(dist);
            
        if(distSQ < rangeSQ)
        {
            dist = Vector3.Distance(mPoint, obj.transform.position);
            float proportionAway = dist / range;
            proportionAway = 1 - proportionAway;
            diff.Normalize();

            addForce(obj, (diff * (mMagnitude * proportionAway)) * Time.deltaTime);
        }
    }

}

public class SpringForceGenerator : ForceGenerator2D
{
    GameObject mObj1, mObj2;
    float mSpringConstant;
    float mRestLength;

    public void Constructor(GameObject obj1, GameObject obj2, float springConst, float restLength)
    {
        mObj1 = obj1;
        mObj2 = obj2;
        mSpringConstant = springConst;
        mRestLength = restLength;
    }

    public override void UpdateForce(GameObject obj)
    {
        if (!mObj1 || !mObj2)
            return;

        Vector3 pos1 = mObj1.transform.position;
        Vector3 pos2 = mObj2.transform.position;


        Vector3 diff = pos1 - pos2;
        float dist = Vector3.Distance(pos1, pos2);

        float magnitude = dist - mRestLength;
        magnitude *= mSpringConstant;

        diff.Normalize();
        diff *= -magnitude;

        Vector3 opposite = new Vector3(-diff.x + 2, -diff.y + 2, 0);

        addForce(mObj1, diff);
        addForce(mObj2, opposite);
    }
}

public class BuoyancyForceGenerator : ForceGenerator2D
{
    private float mMaxDepth, mVolume, mWaterHeight, mDensity;

    public void Constructor(float maxDepth, float volume, float waterHeight, float density)
    {
        mMaxDepth = maxDepth;
        mVolume = volume;
        mWaterHeight = waterHeight;
        mDensity = density;
    }

    public override void UpdateForce(GameObject obj)
    {
        float currentDepth = (obj.transform.position.y);
        Vector3 force = new Vector3(0.0f, 0.0f, 0.0f);

        if(currentDepth >= mWaterHeight)
        {
            obj.GetComponent<Particle2D>().DampingConstant = 0.99f;
            return;
        }

        obj.GetComponent<Particle2D>().DampingConstant = 0.5f;

        if(currentDepth >= (mWaterHeight + mMaxDepth)) //out of water
        {
            force.y = -1 * (mDensity * mVolume);
            return;
        }

        if (currentDepth <= (mWaterHeight - mMaxDepth)) //in water
        {
            force.y = ((mDensity * mVolume));
        }

        else //partially in water
        {
            force.y = mDensity * mVolume * (currentDepth - mMaxDepth - mWaterHeight)/2 * mMaxDepth;
        }

        addForce(obj, force * 0.5f);
        obj.GetComponent<Particle2D>().AccumulatedForces += force;
    }
}
