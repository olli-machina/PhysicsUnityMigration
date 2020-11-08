using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    GameObject obj1, obj2;
    Particle2D obj1Particle, obj2Particle;

    float mRestitutionCoefficient = 0.0f, mPenetration = 0.0f;
    Vector3 mContactNormal = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 mMove1 = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 mMove2 = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        obj1Particle = gameObject.GetComponent<Particle2D>();
        obj2Particle = obj2.GetComponent<Particle2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Resolve(double dt) //?
    {
        ResolveVelocity(dt);
        ResolveInterpenetration(dt);
    }

    float CalculateSeperatingVelocity() //const?
    {
        Vector3 relativeVel = obj1Particle.Velocity;//      obj1.GetComponent<Particle2D>().Velocity;
        if (obj2)
            relativeVel -= obj2Particle.Velocity; //obj2.GetComponent<Particle2D>().Velocity;

        return dotProduct(relativeVel, mContactNormal);
    }

    void ResolveVelocity(double dt)
    {
        float sepVelocity = separatingVelocity();
        if (sepVelocity > 0.0f)
            return;
        float newSepVel = -sepVelocity * mRestitutionCoefficient; //need to set the R Coeff somewhere

        Vector3 velFromAcc = obj1Particle.Acceleration;//   obj1.GetComponent<Particle2D>().Acceleration;

        if (obj2)
            velFromAcc -= obj2Particle.Acceleration;//obj2.GetComponent<Particle2D>().Acceleration;
        float accCausedSepVelocity = dotProduct(velFromAcc, transform.forward) * Time.deltaTime; //can this be time.deltatime?

        if (accCausedSepVelocity < 0.0f)
        {
            newSepVel += mRestitutionCoefficient * accCausedSepVelocity;
            if (newSepVel < 0.0f)
                newSepVel = 0.0f;
        }
        float dataVel = newSepVel - sepVelocity;
        float totalInverseMass = obj1Particle.inverseMass; //obj1.GetComponent<Particle2D>().inverseMass;
        if (obj2)
            totalInverseMass += obj2Particle.inverseMass;//obj2.GetComponent<Particle2D>().inverseMass;
        if (totalInverseMass <= 0)
            return;

        float impulse = dataVel / totalInverseMass;
        Vector3 impulsePerIMass = mContactNormal * impulse;

        Vector3 newVelocity = (obj1Particle.Velocity + impulsePerIMass) * obj1Particle.inverseMass;

        obj1Particle.Velocity = newVelocity; //does this work?

        if (obj2)
        {
            Vector3 newVelocity2 = (obj2.GetComponent<Particle2D>().Velocity + impulsePerIMass) * -obj2.GetComponent<Particle2D>().inverseMass;
            obj2.GetComponent<Particle2D>().Velocity = newVelocity2; //does this work?
        }
    }

    void ResolveInterpenetration(double dt)
    {
        if (mPenetration <= 0.0f)
            return;
        float totalInverseMass = obj1.GetComponent<Particle2D>().inverseMass;
        if (obj2)
            totalInverseMass += obj2.GetComponent<Particle2D>().inverseMass;
        if (totalInverseMass <= 0)
            return;

        Vector3 moverPerIMass = mContactNormal * (mPenetration / totalInverseMass);

        mMove1 = moverPerIMass * obj1.GetComponent<Particle2D>().inverseMass;
        if (obj2)
            mMove2 = moverPerIMass * -obj2.GetComponent<Particle2D>().inverseMass;
        else
            mMove2 *= 0;

        Vector3 newPosition = obj1.transform.position + mMove1;
        obj1.transform.position = newPosition;

        if(obj2)
        {
            Vector3 newPosition2 = obj2.transform.position + mMove2;
            obj2.transform.position = newPosition2;
        }
    }

    public float dotProduct(Vector3 vec1, Vector3 vec2)
    {
        return (vec1.x * vec2.x) + (vec1.y * vec2.y) + (vec1.z * vec2.z);
    }

    public float separatingVelocity()
    {
        Vector3 relativeVel = obj1.GetComponent<Particle2D>().Velocity;
        if (obj2)
        {
            relativeVel -= obj2.GetComponent<Particle2D>().Acceleration;
        }
        return dotProduct(relativeVel, transform.forward);
    }
}
