using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    private static ForceManager instance; //Static Object
    List<ForceGenerator2D> listOfGenerators = new List<ForceGenerator2D>();
    List<ForceGenerator2D> deadGenerators = new List<ForceGenerator2D>();
    ForceGenerator2D forceGenerator;
    public static ForceManager PublicInstance { get { return instance; } }

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

    // Update is called once per frame
    void Update()
    {
        updateall();
    }

    public ForceGenerator2D NewPointForceGenerator(Vector3 point, float magnitude)
    {
        GameObject newForceGenerator = new GameObject("PointForceGenerator");
        PointForceGenerator pointForceGenerator = newForceGenerator.AddComponent<PointForceGenerator>();
        pointForceGenerator.Constructor(point, magnitude);
        addForceGenerator(pointForceGenerator);
        return newForceGenerator.GetComponent<ForceGenerator2D>();
    }

    public ForceGenerator2D NewBouyancyForceGenerator(GameObject obj, float maxDepth, float volume, float waterHeight, float density)
    {
        BuoyancyForceGenerator newBouyancyGen = obj.AddComponent<BuoyancyForceGenerator>();
        newBouyancyGen.Constructor(maxDepth, volume, waterHeight, density);
        addForceGenerator(newBouyancyGen);
        return obj.GetComponent<ForceGenerator2D>();
    }

    public ForceGenerator2D NewSpringForceGenerator(GameObject obj1, GameObject obj2, float springConst, float restLength)
    {
        SpringForceGenerator springForceGenerator = obj1.AddComponent<SpringForceGenerator>();
        springForceGenerator.Constructor(obj1, obj2, springConst, restLength);
        addForceGenerator(springForceGenerator);
        return obj1.GetComponent<ForceGenerator2D>();
    }

   public void addForceGenerator(ForceGenerator2D forceGeneratorToAdd)
   {
        listOfGenerators.Add(forceGeneratorToAdd);
   }
   public void removeForceGenerator(ForceGenerator2D forceGeneratorToRemove)
   {
        listOfGenerators.Remove(forceGeneratorToRemove);
   }
   public void updateall()
   {
        Particle2D[] allParticlesActive = (Particle2D[])GameObject.FindObjectsOfType(typeof(Particle2D)); 

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
