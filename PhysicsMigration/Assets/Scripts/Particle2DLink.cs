using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DLink : MonoBehaviour
{

    public GameObject mObj1, mObj2;
    public float mMaxLength = 10.0f;
    public Particle2DContact particle1, particle2;
    public Vector3 zeroVector = new Vector3(0.0f, 0.0f, 0.0f);
    //public List<Particle2DContact> mContacts;

    virtual public void CreateContacts(List<Particle2DContact> contacts)//&?
    {

    }

    public float distanceBetween(Vector3 pos1, Vector3 pos2)
    {
        Vector3 temp = (pos1-pos2);
        return getLength(temp); //might be wrong
    }

    public float getLengthSquared(Vector3 temp)
    {
        float lengthSquared = (temp.x * temp.x) + (temp.y + temp.y);
        return lengthSquared;
    }
    
    public float getLength(Vector3 temp)
    {
        float lengthSquared = getLengthSquared(temp);
        return Mathf.Sqrt(lengthSquared);
    }

    public Particle2DLink NewLink(GameObject obj1, GameObject obj2, float maxLength)
    {
        //GameObject newForceGenerator = new GameObject("SpringForceGenerator"); //keep this?
        ParticleRod particleRod = obj1.AddComponent<ParticleRod>();
        particleRod.Constructor(obj1, obj2, maxLength);
        //particleRod.CreateContacts(mContacts);
        return obj1.GetComponent<ParticleRod>();
    }
}

public class ParticleCable : Particle2DLink
{
    float maxLength;
    float restitution = 0.5f;


    override public void CreateContacts(List<Particle2DContact> contacts)
    {
        
        float length = getLength(gameObject.transform.position); //?
        if (length < mMaxLength)
            return;
        Vector3 normal = mObj2.transform.position - gameObject.transform.position;
        normal.Normalize();//?

        float penetration = length - mMaxLength;
        Particle2DContact contact = new Particle2DContact();
        contact.Constructor(mObj1, mObj2, restitution, penetration, normal, zeroVector, zeroVector); //is this correct?
        contacts.Add(contact);
    }
}

public class ParticleRod : Particle2DLink
{
    float mLength;

    override public void CreateContacts(List<Particle2DContact> contacts)
    {
        float penetration;
        float restitution = 0.0f;
        float length = distanceBetween(mObj1.transform.position, mObj2.transform.position);
        if (length == mLength)
            return;

        Vector3 normal = mObj2.transform.position - mObj1.transform.position;
        normal.Normalize();

        if (length > mLength)
            penetration = length - mLength;
        else
        {
            normal *= -1;
            penetration = mLength - length;
        }

        penetration *= 0.001f;
        Debug.Log(length + "Max: " + mLength);
        mObj1.AddComponent<Particle2DContact>();
        Particle2DContact contact = mObj1.GetComponent<Particle2DContact>();
        contact.Constructor(mObj1, mObj2, restitution, penetration, normal, zeroVector, zeroVector);
        contacts.Add(contact);
    }

    public void Constructor(GameObject obj1, GameObject obj2, float length)
    {
        mObj1 = obj1;
        mObj2 = obj2;
        mLength = length;
    }

}

