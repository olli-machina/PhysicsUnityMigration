using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    private static ForceManager instance; //why?
    List<ForceGenerator2D> listOfGenerators = new List<ForceGenerator2D>();
    List<ForceGenerator2D> deadGenerators = new List<ForceGenerator2D>();
    ForceGenerator2D forceGenerator;
    public static ForceManager PublicInstance { get { return instance; } }
   //public static GameObject springForceSecondObject;

   // Start is called before the first frame update
   private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
        
    }

    void Start()
    {
        NewPointForceGenerator(new Vector3(0, 0, 0), 1);
    }

    private void FixedUpdate()
    {
        //forceGenerator.addForce()
    }


    // Update is called once per frame
    void Update()
    {
        updateall();
    }

    public ForceGenerator2D NewPointForceGenerator(Vector3 point, float magnitude) //look into this
    {
        GameObject newForceGenerator = new GameObject("PointForceGenerator"); //keep this?
        PointForceGenerator pointForceGenerator = newForceGenerator.AddComponent<PointForceGenerator>();
        pointForceGenerator.Constructor(point, magnitude);
        addForceGenerator(pointForceGenerator);
        return newForceGenerator.GetComponent<ForceGenerator2D>();
    }

    public ForceGenerator2D NewBouyancyForceGenerator(GameObject obj, float maxDepth, float volume, float waterHeight, float density)
    {
        //GameObject newForceGenerator = new GameObject()
        BuoyancyForceGenerator newBouyancyGen = obj.AddComponent<BuoyancyForceGenerator>();
        newBouyancyGen.Constructor(maxDepth, volume, waterHeight, density);
        addForceGenerator(newBouyancyGen);
        return obj.GetComponent<ForceGenerator2D>();
    }

   void addForceGenerator(ForceGenerator2D forceGeneratorToAdd)
   {
        Debug.Log(forceGeneratorToAdd);
        listOfGenerators.Add(forceGeneratorToAdd);
   }
   public void removeForceGenerator(ForceGenerator2D forceGeneratorToRemove)
   {
        listOfGenerators.Remove(forceGeneratorToRemove);
   }
   public void updateall()
   {
        Debug.Log("Called");
        Particle2D[] allParticlesActive = (Particle2D[])GameObject.FindObjectsOfType(typeof(Particle2D)); //needs to be typecast

        //for (var i = 0; i < arrayOfGenerators.Count; i++)
        //{
        //    arrayOfGenerators[i].UpdateForce(arrayOfGenerators[i].gameObject, Time.deltaTime);
        //}
        foreach(ForceGenerator2D generator in listOfGenerators)
        {
            if (generator == null)
                deadGenerators.Add(generator);
            else
            {
                foreach (Particle2D particle in allParticlesActive)
                {
                    if (particle.gameObject == null)
                        return;
                    generator.UpdateForce(particle.gameObject);
                }
            }
        }

        foreach (ForceGenerator2D generator in deadGenerators)
            removeForceGenerator(generator);

        deadGenerators.Clear();
   }
}
