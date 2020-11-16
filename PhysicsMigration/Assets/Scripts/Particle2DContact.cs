using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    GameObject mObj1, mObj2 = null;
    Particle2D obj1Particle, obj2Particle;

    public float mRestitutionCoefficient = 0.0f, mPenetration = 0.0f;
    public Vector3 mContactNormal = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 mMove1 = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 mMove2 = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {

    }
    public void Constructor(GameObject obj1, GameObject obj2, float restitutionCoeff, float penetration, Vector3 contactNormal, Vector3 move1, Vector3 move2)
    {
        mObj1 = obj1;
        mObj2 = obj2;
        mRestitutionCoefficient = restitutionCoeff;
        mPenetration = penetration;
        mContactNormal = contactNormal;
        mMove1 = move1;
        mMove2 = move2;

        obj1Particle = mObj1.GetComponent<Particle2D>();
        if (mObj2)
            obj2Particle = mObj2.GetComponent<Particle2D>();
    }

    void Resolve(double dt)
    {
        ResolveVelocity(dt);
        ResolveInterpenetration(dt);
    }

    float CalculateSeperatingVelocity()
    {
        Vector3 relativeVel = obj1Particle.Velocity;
        if (mObj2)
            relativeVel -= obj2Particle.Velocity;

        return dotProduct(relativeVel, mContactNormal);
    }

    void ResolveVelocity(double dt)
    {
        float sepVelocity = separatingVelocity();
        if (sepVelocity > 0.0f)
            return;
        float newSepVel = -sepVelocity * mRestitutionCoefficient;

        Vector3 velFromAcc = obj1Particle.Acceleration;

        if (mObj2)
            velFromAcc -= obj2Particle.Acceleration;
        float accCausedSepVelocity = dotProduct(velFromAcc, transform.forward) * Time.deltaTime;

        if (accCausedSepVelocity < 0.0f)
        {
            newSepVel += mRestitutionCoefficient * accCausedSepVelocity;
            if (newSepVel < 0.0f)
                newSepVel = 0.0f;
        }
        float dataVel = newSepVel - sepVelocity;
        float totalInverseMass = obj1Particle.inverseMass;
        if (mObj2)
            totalInverseMass += obj2Particle.inverseMass;
        if (totalInverseMass <= 0)
            return;

        float impulse = dataVel / totalInverseMass;
        Vector3 impulsePerIMass = mContactNormal * impulse;

        Vector3 newVelocity = (obj1Particle.Velocity + impulsePerIMass) * obj1Particle.inverseMass;

    }

    void ResolveInterpenetration(double dt)
    {
        if (mPenetration <= 0.0f)
            return;
        float totalInverseMass = obj1Particle.inverseMass;
        if (mObj2)
            totalInverseMass += obj2Particle.inverseMass;
        if (totalInverseMass <= 0)
            return;

        Vector3 moverPerIMass = mContactNormal * (mPenetration / totalInverseMass);

        mMove1 = moverPerIMass * obj1Particle.inverseMass;
        if (mObj2)
            mMove2 = moverPerIMass * -obj2Particle.inverseMass;
        else
            mMove2 *= 0;

        Vector3 newPosition = gameObject.transform.position + mMove1;
        gameObject.transform.position = newPosition;

        if(mObj2)
        {
            Vector3 newPosition2 = mObj2.transform.position + mMove2;
            mObj2.transform.position = newPosition2;
        }
    }

    public float dotProduct(Vector3 vec1, Vector3 vec2)
    {
        return (vec1.x * vec2.x) + (vec1.y * vec2.y) + (vec1.z * vec2.z);
    }

    public float separatingVelocity()
    {
        Vector3 relativeVel = obj1Particle.Velocity;
        if (mObj2)
        {
            relativeVel -= obj2Particle.Acceleration;
        }
        return dotProduct(relativeVel, transform.forward);
    }

   public void resolveContacts(List<Particle2DContact> contacts, double dt)
   {
      int mIterationsUsed = 0;
      while (mIterationsUsed < 10)
      {
         float max = 3.402823466e+38F;
         int numContacts = contacts.Count;
         int maxIndex = numContacts;
         for (int i = 0; i < numContacts; i++)
         {
            float sepVel = contacts[i].CalculateSeperatingVelocity();
            if (sepVel < max && (sepVel < 0.0f || contacts[i].mPenetration > 0.0f))
            {
               max = sepVel;
               maxIndex = i;
            }
         }
         if (maxIndex == numContacts)
            break;

         contacts[maxIndex].Resolve(dt);

         for (int i = 0; i < numContacts; i++)
         {
            if (contacts[i].mObj1 == contacts[maxIndex].mObj1)
            {
               contacts[i].mPenetration -= dotProduct(contacts[maxIndex].mMove1, contacts[i].mContactNormal);
            }
            else if (contacts[i].mObj1 == contacts[maxIndex].mObj2)
            {
               contacts[i].mPenetration -= dotProduct(contacts[maxIndex].mMove2, contacts[i].mContactNormal);
            }

            if (contacts[i].mObj2)
            {
               if (contacts[i].mObj2 == contacts[maxIndex].mObj1)
               {
                  contacts[i].mPenetration += dotProduct(contacts[maxIndex].mMove1, contacts[i].mContactNormal);
               }
               else if (contacts[i].mObj2 == contacts[maxIndex].mObj2)
               {
                  contacts[i].mPenetration -= dotProduct(contacts[maxIndex].mMove2, contacts[i].mContactNormal);
               }
            }
         }
         mIterationsUsed++;
      }
   }
}