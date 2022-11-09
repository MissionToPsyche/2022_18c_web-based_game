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
    //public List<GameObject> particles = new List<GameObject>();
    public List<GameObject> particles;

    // Start is called before the first frame update
    void Start()
    {
        //Counts the number of each type of particle at start
        foreach(GameObject x in particles)
        {
            if(x.tag == "MetalCore_Particle")
            {
                metal_particle_count++;
            }

            if(x.tag == "Mantle_Particle")
            {
                mantle_particle_count++;
            }

            if(x.tag == "Crust_Particle")
            {
                crust_particle_count++;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
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
