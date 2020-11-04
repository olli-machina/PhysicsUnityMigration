using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    GameObject obj1, obj2;

    float mRestitutionCoefficient = 0.0f, mPenetration = 0.0f;
    Vector3 mContactNormal = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 mMove1 = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 mMove2 = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 previousPos1, previousPos2, velocity1, velocity2,
        acc1, acc2;

    // Start is called before the first frame update
    void Start()
    {
        previousPos1 = new Vector3(0.0f, 0.0f, 0.0f);
        previousPos2 = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        updateForces();
    }

    void updateForces()
    {
        velocity1 = (obj1.transform.position - previousPos1) / Time.deltaTime;
        velocity2 = (obj2.transform.position - previousPos2) / Time.deltaTime;

        previousPos1 = obj1.transform.position;
        previousPos2 = obj2.transform.position;

        acc1 = velocity1 / Time.deltaTime;
        acc2 = velocity2 / Time.deltaTime;
    }

    void Resolve(double dt) //?
    {
        float sepVelocity = separatingVelocity();
        if (sepVelocity > 0.0f)
            return;
        float newSepVel = -sepVelocity * mRestitutionCoefficient; //need to set the R Coeff somewhere

        Vector3 velFromAcc = acc1;

        if (obj2)
            velFromAcc -= acc2;
        float accCausedSepVelocity = dotProduct(velFromAcc, transform.forward)* Time.deltaTime; //can this be time.deltatime?

        if(accCausedSepVelocity < 0.0f)
        {
            newSepVel += mRestitutionCoefficient * accCausedSepVelocity;
            if (newSepVel < 0.0f)
                newSepVel = 0.0f;
        }
        float dataVel = newSepVel - sepVelocity;
        //float totalInverseMass = 

    }

    float CalculateSeperatingVelocity() //const?
    {
        return 5;
    }

    void ResloveVelocity(double dt)
    {

    }

    void ResolveInterpenetration(double dt)
    {

    }

    public float dotProduct(Vector3 vec1, Vector3 vec2)
    {
        return (vec1.x * vec2.x) + (vec1.y * vec2.y) + (vec1.z * vec2.z);
    }

    public float separatingVelocity()
    {
        Vector3 relativeVel = velocity1;
        if (obj2)
        {
            relativeVel -= velocity2;
        }
        return dotProduct(relativeVel, transform.forward);
    }
}
