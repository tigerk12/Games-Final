using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject manager;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }

    Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

    //Flock moves towards destination
    void applyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);

        if(force.magnitude > manager.GetComponent<FlockSpawn>().maxForce)
        {
            force = force.normalized;
            force *= manager.GetComponent<FlockSpawn>().maxForce;
        }
        this.GetComponent<Rigidbody2D>().AddForce(force);

        if(this.GetComponent<Rigidbody2D>().velocity.magnitude > manager.GetComponent<FlockSpawn>().maxVelocity)
        {
            this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized;
            this.GetComponent<Rigidbody2D>().velocity *= manager.GetComponent<FlockSpawn>().maxVelocity;
        }
    }

    //Flock can change how aligned they are between one another
    Vector2 align()
    {
        float neighbourDist = manager.GetComponent<FlockSpawn>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.GetComponent<FlockSpawn>().units)
        {
            if (other == this.gameObject)
                continue;

            float d = Vector2.Distance(location, other.GetComponent<Enemy>().location);

            if(d < neighbourDist)
            {
                sum += other.GetComponent<Enemy>().velocity;
                count++;
            }
        }
        if(count > 0)
        {
            sum /= count;
            Vector2 steer = sum - velocity;
            return steer;
        }

        return Vector2.zero;
    }

    //Can also change the level of cohesion
    Vector2 cohesion()
    {
        float neighbourDist = manager.GetComponent<FlockSpawn>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (GameObject other in manager.GetComponent<FlockSpawn>().units)
        {
            if (other == this.gameObject)
                continue;

            float d = Vector2.Distance(location, other.GetComponent<Enemy>().location);

            if (d < neighbourDist)
            {
                sum += other.GetComponent<Enemy>().location;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            return seek(sum);
        }

        return Vector2.zero;
    }

    void flock()
    {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        if(manager.GetComponent<FlockSpawn>().obedient && Random.Range(0,50) <= 1)
        {
            Vector2 ali = align();
            Vector2 coh  =cohesion();
            Vector2 gl;

            if (manager.GetComponent<FlockSpawn>().seekGoal)
            {
                gl = seek(goalPos);
                currentForce = gl + ali + coh;
            }
            else                
                currentForce = currentForce.normalized;
        }

        if(manager.GetComponent<FlockSpawn>().willful && Random.Range(0, 50) <= 1)
        {
            if (Random.Range(0, 50) < 1)
                currentForce = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));

        }
        applyForce(currentForce);
    }

    // Update is called once per frame
    void Update()
    {
        flock();
        goalPos = manager.transform.position;
    }

}
