using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleProjection : MonoBehaviour
{
    /*LineRenderer lineRenderer;
    
    //Used to determine the number of points on the projection line
    public int numPoints = 30;

    //Used to determine the distance between points on the projection line
    public float distanceBetweenPoints = 0.1f;

    //Used to stop the particles from being drawn
    public LayerMask CollidableLayers;

    void start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        
        GameObject otherAsteroid = GameObject.Find("OtherAsteroid");
        Vector3 startingPosition = otherAsteroid.transform.position;
    }
    */
    public float distance = 2f;
    public int angle;

    void DrawProjection()
    {
        GameObject otherAsteroid = GameObject.Find("OtherAsteroid");
        GameObject userInput = GameObject.Find("UserInput");
        UserInputToAsteroid script = userInput.GetComponent<UserInputToAsteroid>();
        angle = script.angleInput;

        float x = otherAsteroid.transform.position.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = otherAsteroid.transform.position.y + distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        
        Vector3 finalPos = new Vector3(x, y, 0f);
        Debug.DrawLine(otherAsteroid.transform.position, finalPos, Color.blue);
    }
}
