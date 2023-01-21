using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyObject : MonoBehaviour
{
    public Transform[] floaters;

    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;

    public float airDrag = 3f;
    public float airAngularDrag = 0.05f;

    public float floatingPower = 15f;
    public float depthForceMultiplier = 1f;

    public float waterHeight = 0f;

    public Rigidbody rigidbody;

    int floatersUnderwater;

    bool underwater = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        floatersUnderwater = 0;
        foreach (Transform floater in floaters)
        {
            float diff = floater.position.y - waterHeight;

            if (diff < 0)
            {
                floatersUnderwater++;
                rigidbody.AddForceAtPosition(Vector3.up * floatingPower * (Mathf.Abs(diff) * depthForceMultiplier), floater.position, ForceMode.Force);
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }

        }

        if (underwater && floatersUnderwater == 0)
        {
            underwater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            rigidbody.drag = underwaterDrag;
            rigidbody.angularDrag = underwaterAngularDrag;
        }
        else
        {
            rigidbody.drag = airDrag;
            rigidbody.angularDrag = airAngularDrag;
        }
    }
}
