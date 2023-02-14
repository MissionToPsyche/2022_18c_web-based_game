using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCounter : MonoBehaviour
{
    //used to see in inspector view
    //Use the get methods if you want to use the numbers in another script
    public int metal_particle_count = 0;
    public int mantle_particle_count = 0;
    public int crust_particle_count = 0;

    //Used to store the particles 
    public List<GameObject> particles;

    // Start is called before the first frame update
    void Start()
    {
        //Get all active game objects and put them in an array
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));

        //Now parse through the array elements and add the elements that are 
        //of an actual particle type to the particles list
        foreach(object o in obj)
        {
            GameObject g = (GameObject)o;
            //Debug.Log(g.name);
            if (g.tag == "MetalCore_Particle")
            {
                particles.Add(g);
            }

            if (g.tag == "Mantle_Particle")
            {
                particles.Add(g);
            }

            if (g.tag == "Crust_Particle")
            {
                particles.Add(g);
            }
        }

        //Counts the number of each type of particle at start
        foreach (GameObject x in obj)
        {
            if (x.tag == "MetalCore_Particle")
            {
                metal_particle_count++;
            }

            if (x.tag == "Mantle_Particle")
            {
                mantle_particle_count++;
            }

            if (x.tag == "Crust_Particle")
            {
                crust_particle_count++;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }

    //This method will be called from otherAsteroids AfterLaunchTimer.cs
    //It will parse though all the gameobjects and add the ones
    //that have partOfPsyche as true in the PsycheParticleCounter.cs script
    public void countWhatParticlesArePartOfPsyche()
    {
        //reset the counters
        metal_particle_count = 0;
        mantle_particle_count = 0;
        crust_particle_count = 0;

        //Will hold all the particles where partOfPsyche == true
        //List<GameObject> PsycheParticles; 

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            PsycheParticleCounter script = g.GetComponent<PsycheParticleCounter>();

            if (g.tag == "MetalCore_Particle" || g.tag == "OA_MetalCore_Particle" && script.partOfPsyche == true)
            {
                //PsycheParticles.Add(g);
                if(g.tag == "OA_MetalCore_Particle")
                {
                    particles.Add(g);
                }
                metal_particle_count++;
            }

            else if (g.tag == "Mantle_Particle" || g.tag == "OA_Mantle_Particle" && script.partOfPsyche == true)
            {
                //PsycheParticles.Add(g);
                if (g.tag == "OA_Mantle_Particle")
                {
                    particles.Add(g);
                }
                mantle_particle_count++;
            }

            else if (g.tag == "Crust_Particle" || g.tag == "OA_Crust_Particle" && script.partOfPsyche == true)
            {
                //PsycheParticles.Add(g);
                if (g.tag == "OA_Crust_Particle")
                {
                    particles.Add(g);
                }
                crust_particle_count++;
            }
            else
            {
                particles.Remove(g);
            }
        }
    }

    void RemoveParticleCount(GameObject particle)
    {
        if(particle.tag == "MetalCore_Particle")
        {
            metal_particle_count--;
        }

        if(particle.tag == "Mantle_Particle")
        {
            mantle_particle_count--;
        }

        if(particle.tag == "Crust_Particle")
        {
            crust_particle_count--;
        }
    }

    public int getMetal_Particle_Count()
    {
        return metal_particle_count;
    }

    public int getMantle_Particle_Count()
    {
        return mantle_particle_count;
    }

    public int getCrust_Particle_Count()
    {
        return crust_particle_count;
    }
}
