using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

   List<Particle2D> listOfParticle2D = new List<Particle2D>();
   List<Particle2D> deadParticle2D = new List<Particle2D>();

   private static Particle2D instance;
   public static Particle2D PublicInstance { get { return instance; } }
   // Start is called before the first frame update
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void addParticle2D (Particle2D particle2DToAdd)
   {
      //Debug.Log(forceGeneratorToAdd);
      listOfParticle2D.Add(particle2DToAdd);
   }
   public void removeParticle2D(Particle2D particle2DToRemove)
   {
      listOfParticle2D.Remove(particle2DToRemove);
   }

   public void updateall()
   {
      // Debug.Log("Called");
     // Particle2D[] allParticlesActive = (Particle2D[])GameObject.FindObjectsOfType(typeof(Particle2D)); //needs to be typecast

      //for (var i = 0; i < arrayOfGenerators.Count; i++)
      //{
      //    arrayOfGenerators[i].UpdateForce(arrayOfGenerators[i].gameObject, Time.deltaTime);
      //}
      foreach (Particle2D particle in listOfParticle2D)
      {
         foreach (Particle2D particle2 in listOfParticle2D)
         {
            if (particle == null)
               deadParticle2D.Add(particle);

            else if (particle2 == null)
               deadParticle2D.Add(particle2);

            else
            {
               checkCollision(particle, particle2);
            }
         }
            
      }

      foreach (Particle2D particle in deadParticle2D)
         removeParticle2D(particle);

      deadParticle2D.Clear();
   }

   public bool checkCollision(Particle2D obj1, Particle2D obj2)
   {
      Vector3 relPos = obj1.transform.position - obj2.transform.position;
      float dist = relPos.x * relPos.x + relPos.y * relPos.y + relPos.z * relPos.z;
      float minDist = (obj1.transform.localScale.y / 2) + (obj2.transform.localScale.y / 2);
      return dist <= minDist * minDist;
   }
}
