using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public  float moveSpeed; // chasing speed
    private float originalMoveSpeed; // used to reset creature's movement speed when done chasing
    public  float chaseDistance = 10f;
    private bool  ischasing;
    
    public  float stoppingDistance;
    public  float nextWaypointDistance = 3;
    private int   currentWaypoint = 0;
    public  bool  reachedEndOfPath;
    
    public  float repathRate = 0.5f;
    private float lastRepath = float.NegativeInfinity;
    
    public  Transform target;
    public  Path path;
    private PlayerController Player;
    private Seeker seeker;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        originalMoveSpeed = moveSpeed;
    }

     void Update()
    {
        #region Chasing
        chaseDistance = Vector3.Distance(rigidbody2d.position, target.position);
        
        if (chaseDistance >= 10 && ischasing) // monster is no longer chasing
        {
            moveSpeed = 2;
            ischasing = false;
            Debug.Log("Is no longer chasing!");
        }
        else if (chaseDistance <= 10 && !ischasing) // monster is chasing!
        {
            moveSpeed = originalMoveSpeed;
            ischasing = true;
        }
        #endregion

        if (Time.time > lastRepath + repathRate && seeker.IsDone())
        {
            lastRepath = Time.time;

            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }

        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * moveSpeed * speedFactor;

        // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
        transform.position += velocity * Time.deltaTime;
    }


    // Update is called once per frame
    /*void FixedUpdate()
    {
       if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
       {
           float xDifference = Math.Abs(transform.position.x - target.position.x);
           float yDifference = Math.Abs(transform.position.y - target.position.y);

           if (xDifference > yDifference)
           {
                Vector2 xTarget = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, xTarget, moveSpeed * Time.deltaTime);
           }
           else if (yDifference > xDifference)
           {
                Vector2 yTarget = new Vector2(transform.position.x, target.position.y);
                transform.position = Vector2.MoveTowards(transform.position, yTarget, moveSpeed * Time.deltaTime);
           }
       }
       
       Vector2 position = rigidbody2d.position;
       position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
       position.y = position.y + moveSpeed * vertical * Time.deltaTime;
       
       rigidbody2d.MovePosition(position);
    } */

    void OnCollisionEnter2D(Collision2D other)
    {
      PlayerController player = other.gameObject.GetComponent<PlayerController>();
      if (player != null)
      {
          player.TakeDamage(1);
      }
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);

        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }
}