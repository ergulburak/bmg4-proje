﻿using System.Collections;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public GameObject player;
    [Header("LineRenderer Variables")] public LineRenderer line;
    [Range(6, 60)] public int resolution;

    public float timeSensitivity = 0.1f;
    private float _timeStamp;

    public LayerMask canHit;

    public void UpdateDots(Vector3 playerPos, Vector2 forceApplied)
    {
        int temp = 0;
        Vector3[] lineArray = new Vector3[resolution];
        lineArray[0] = playerPos;
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i].x = (playerPos.x + forceApplied.x * i / 20);
            lineArray[i].y = (playerPos.y + forceApplied.y * i / 20) -
                             (Physics2D.gravity.magnitude * i / 20 * i / 20) / 2f;
            if (CheckForCollision(lineArray[i]))
            {
                temp = i;
                break;
            }
        }

        line.positionCount = temp;


        line.SetPositions(lineArray);
    }

    private bool CheckForCollision(Vector2 position)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Walkable") | 1 << LayerMask.NameToLayer("Wall");
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.SetLayerMask(mask);
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, 0.05f /*collisionCheckRadius*/, filter2D.layerMask);
        if (hits.Length > 0)
        {
            //We hit something 
            //check if its a wall or seomthing
            //if its a valid hit then return true
            return true;
        }

        return false;
    }

    public void Show()
    {
        line.enabled = true;
    }

    public void Hide()
    {
        line.enabled = false;
    }
}