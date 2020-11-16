using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

   List<GameObject> listOfParticle2D = new List<GameObject>();
   List<GameObject> deadParticle2D = new List<GameObject>();
   GameManager gameManager;

   private static Particle2D instance;
   public static Particle2D PublicInstance { get { return instance; } }
   // Start is called before the first frame update
   void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
      updateall();
    }

   public void addParticle2D (GameObject particle2DToAdd)
   {
      //Particle2D particleData = particle2DToAdd.GetComponent<Particle2D>();
      listOfParticle2D.Add(particle2DToAdd);
   }
   public void removeParticle2D(GameObject particle2DToRemove)
   {
        if (particle2DToRemove && particle2DToRemove.tag == "Target")
            gameManager.isTarget = false;
        listOfParticle2D.Remove(particle2DToRemove);
        Destroy(particle2DToRemove);
   }

   public void updateall()
   {
        // Particle2D[] allParticlesActive = (Particle2D[])GameObject.FindObjectsOfType(typeof(Particle2D)); //needs to be typecast

        //for (var i = 0; i < arrayOfGenerators.Count; i++)
        //{
        //    arrayOfGenerators[i].UpdateForce(arrayOfGenerators[i].gameObject, Time.deltaTime);
        //}
        foreach (GameObject particle in listOfParticle2D)
      {
         foreach (GameObject particle2 in listOfParticle2D)
         {
            if (particle == null)
               deadParticle2D.Add(particle);

            else if (particle2 == null)
               deadParticle2D.Add(particle2);

            else
            {
               if(particle.tag != particle2.tag &&
                  checkCollision(particle.GetComponent<Particle2D>(), particle2.GetComponent<Particle2D>()) == true)
               {
                  //Destroy(particle);
                  deadParticle2D.Add(particle);
                  //Destroy(particle2);
                  deadParticle2D.Add(particle2);
                  int deleteIndex1 = listOfParticle2D.IndexOf(particle);
                  int deleteIndex2 = listOfParticle2D.IndexOf(particle2);
               }
            }
         }
            
      }

      foreach (GameObject particle in deadParticle2D)
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
