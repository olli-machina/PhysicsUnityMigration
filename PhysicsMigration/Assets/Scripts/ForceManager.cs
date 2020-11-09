using UnityEngine;
using System.Collections.Generic;

static class ForceManager
{
   static List<ForceGenerator2D> arrayOfGenerators;
   public static GameObject springForceSecondObject;

   // Start is called before the first frame update
   static void Start()
    {
        
    }

    // Update is called once per frame
    static void Update()
    {
        
    }

   static void addForceGenerator(ForceGenerator2D forceGeneratorToAdd)
   {
      arrayOfGenerators.Add(forceGeneratorToAdd);
   }
   static void removeForceGenerator(ForceGenerator2D forceGeneratorToAdd)
   {
      arrayOfGenerators.Remove(forceGeneratorToAdd);
   }
   static void updateall()
   {
      for (var i = 0; i < arrayOfGenerators.Count; i++)
      {
         arrayOfGenerators[i].UpdateForce(arrayOfGenerators[i].gameObject, Time.deltaTime);
      }
   }
}
