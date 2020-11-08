using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DLink : MonoBehaviour
{

    public GameObject obj2;
    public float mMaxLength;
    public Particle2DContact particle1, particle2;
    public Vector3 zeroVector = new Vector3(0.0f, 0.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void CreateContacts(List<Particle2DContact> contacts)//&?
    {
    }

    public float distanceBetween(Vector3 pos1, Vector3 pos2)
    {
        Vector3 temp = (pos1-pos2);
        return getLength(); //might be wrong
    }

    public float getLengthSquared()
    {
        float lengthSquared = (gameObject.transform.position.x * obj2.transform.position.x) + (gameObject.transform.position.y + obj2.transform.position.y);
        return lengthSquared;
    }
    
    public float getLength()
    {
        float lengthSquared = getLengthSquared();
        return Mathf.Sqrt(lengthSquared);
    }
}

public class ParticleCable : Particle2DLink
{
    float maxLength;
    float restitution;


    override public void CreateContacts(List<Particle2DContact> contacts)
    {
        float length = getLength();
        if (length < mMaxLength)
            return;
        Vector3 normal = obj2.transform.position - gameObject.transform.position;
        normal.Normalize();//?

        float penetration = length - mMaxLength;
        Particle2DContact contact = new Particle2DContact(obj2, restitution, normal, penetration, zeroVector, zeroVector); //is this correct?
        contacts.Add(contact);
    }
}

public class ParticleRod : Particle2DLink
{
    float mLength;

    override public void CreateContacts(List<Particle2DContact> contacts)
    {
        float penetration = 0.0f;
        float restitution = 0.0f;
        float length = getLength();
        if (length == mLength)
            return;

        Vector3 normal = obj2.transform.position - gameObject.transform.position;
        normal.Normalize();

        if (length > mLength)
            penetration = length - mLength;
        else
        {
            normal *= -1;
            penetration = mLength - length;
        }

        penetration *= 0.001f;
        Particle2DContact contact = new Particle2DContact(obj2, restitution, normal, penetration, zeroVector, zeroVector); //is this correct?
        contacts.Add(contact);
    }

}