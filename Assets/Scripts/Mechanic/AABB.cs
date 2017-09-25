using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour {
# region Variables
    List<AABB> currentOverlaps = new List<AABB>();

    public Vector3 halfSize;

    Vector3 min = Vector3.zero;
    Vector3 max = Vector3.zero;

    public Vector3 offset;
#endregion 
    // Update is called once per frame
    void Update () {
        calcEdges();
	}
    /// <summary>
    /// calcEdges
    /// Calculates the edges and halfsize from teh center point of the object
    /// From the Min and Max Edges
    /// </summary>
    public void calcEdges()
    {
        min = transform.position - halfSize;
        max = transform.position + halfSize;
    }

    /// <summary>
    /// Check Overlap
    /// Checks Overlap of the Objects AABB
    /// The objects x,y,z are all checked to another objects 
    /// </summary>
    /// <param AABB = "other"></param>
    /// <returns></returns>
    public bool checkOverlap(AABB other)
    {
        if(other != null)
        {
            if (min.x > other.max.x) return false;
            if (max.x < other.min.x) return false;

            if (min.y > other.max.y) return false;
            if (max.y < other.min.y) return false;

            if (min.z > other.max.z) return false;
            if (max.z < other.min.z) return false;

            return true;
        }
        return true;
    }

    //How far to move this AABB to correct its overlap with other AABB
    /// <summary>
    /// CalculateOverlapFix
    /// Checks the Objects overlap and then moves it right, up, forward, left, down or back,
    /// Solution is the x,y,z of the coordinate away from the object.
    /// </summary>
    /// <param AABB="other"></param>
    /// <returns> Solution of the answer given by the objects X,Y,Z </returns>
    public Vector3 CalculateOverlapFix(AABB other)
    {
        
        float moveRight = other.max.x - min.x;
        float moveUp = other.max.y - min.y;
        float moveForward = other.max.z - min.z;

        float moveLeft = other.min.x - max.x;
        float moveDown = other.min.y - max.y;
        float moveBack = other.min.z - max.z;

        Vector3 solution;
    
        solution.z = Mathf.Abs(moveForward) < Mathf.Abs(moveBack) ? moveForward : moveBack;
        solution.y = Mathf.Abs(moveUp) < Mathf.Abs(moveDown) ? moveUp : moveDown;
        solution.x = Mathf.Abs(moveRight) < Mathf.Abs(moveLeft) ? moveRight : moveLeft;

        if (Mathf.Abs(solution.x) < Mathf.Abs(solution.z) && Mathf.Abs(solution.x) < Mathf.Abs(solution.y))
        {
            solution.z = 0;
            solution.y = 0;

        }

        if (Mathf.Abs(solution.y) < Mathf.Abs(solution.x) && Mathf.Abs(solution.y) < Mathf.Abs(solution.z))
        {
            solution.x = 0;
            solution.z = 0;

        }
        
        if (Mathf.Abs(solution.z) < Mathf.Abs(solution.x) && Mathf.Abs(solution.z) < Mathf.Abs(solution.y))
        {
            solution.x = 0;
            solution.y = 0;

        }
        //if (Mathf.Abs(solution.z) > Mathf.Abs(solution.x) || Mathf.Abs(solution.z) > Mathf.Abs(solution.y)) solution.z = 0;
        //if (Mathf.Abs(solution.y) > Mathf.Abs(solution.x) || Mathf.Abs(solution.y) > Mathf.Abs(solution.z)) solution.y = 0;
        //if (Mathf.Abs(solution.x) > Mathf.Abs(solution.y) || Mathf.Abs(solution.x) > Mathf.Abs(solution.z)) solution.x = 0;

        return solution;

    }
}
