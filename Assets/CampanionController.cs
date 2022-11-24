using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampanionController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject projectile;

    public int          attackDelay;
    public int          movementSpeed;
    public int          standOffDistance;
    public float        wiggleRoom;
    private float       cAttackDelay;
    private float       mVecMag;
    private Vector2     mVector;
    private Rigidbody2D rB;
    private Rigidbody2D rBP;

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        rBP = player.GetComponent<Rigidbody2D>();   
        cAttackDelay= attackDelay;
    }

    
    void Update()
    {
        if (player != null ) 
        {
            mVector = rBP.position - rB.position;
            mVecMag = mVector.magnitude;
            mVector.Normalize();

            if (mVecMag < standOffDistance - wiggleRoom)
            {
                rB.MovePosition(rB.position + (mVector * movementSpeed * Time.fixedDeltaTime) * -1);
            }
            else if (mVecMag > standOffDistance + wiggleRoom)
            {
                rB.MovePosition(rB.position + (mVector * movementSpeed * Time.fixedDeltaTime));
            }

        }

        if (cAttackDelay > 0)
        { 
            cAttackDelay--; 
        }

        if (Input.GetMouseButtonDown(0) && cAttackDelay <= 0)
        {        
            Instantiate(projectile, this.transform.position, Quaternion.identity);
            cAttackDelay = attackDelay;
        }
    }
}
