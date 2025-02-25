using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float lifeTime = 5; // The time before the ball is destroyed

    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
