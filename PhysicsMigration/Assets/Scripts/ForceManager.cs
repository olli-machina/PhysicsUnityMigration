using UnityEngine;
using System.Collections.Generic;

static class ForceManager
{
   public static List<ForceGenerator2D> arrayOfGenerators;
   public static GameObject springForceSecondObject;

   // Start is called before the first frame update
   static void Start()
    {
        
    }

    // Update is called once per frame
    static void Update()
    {
        
    }

   static public void addForceGenerator(ForceGenerator2D forceGeneratorToAdd)
   {
      Debug.Log(forceGeneratorToAdd);
      arrayOfGenerators.Add(forceGeneratorToAdd);
   }
   static public void removeForceGenerator(ForceGenerator2D forceGeneratorToAdd)
   {
      arrayOfGenerators.Remove(forceGeneratorToAdd);
   }
   static public void updateall()
   {
      for (var i = 0; i < arrayOfGenerators.Count; i++)
      {
         arrayOfGenerators[i].UpdateForce(arrayOfGenerators[i].gameObject, Time.deltaTime);
      }
   }
}
