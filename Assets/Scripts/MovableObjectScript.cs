using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectScript : MonoBehaviour
{

    public enum Vector
    {
        X,
        Y,
        Z
    }
    public float posXMax;
    public float posXMin;
    public float posYMax;
    public float posYMin;
    public float posZMax;
    public float posZMin;

    public float force;

    public void MoveObject(Vector vector, bool positive) { 
        switch (vector)
        {
            case Vector.X:
                switch (positive)
                {
                    case true:
                        if(this.transform.position.x >= posXMax)
                        {
                            this.transform.position = new Vector3(posXMax, this.transform.position.y, this.transform.position.z);
                            break;
                        }
                        else
                        {
                            this.transform.position = new Vector3(this.transform.position.x + force, this.transform.position.y, this.transform.position.z);
                            break;
                        }
                    case false:
                        if (this.transform.position.x <= posXMin)
                        {
                            this.transform.position = new Vector3(posXMin, this.transform.position.y, this.transform.position.z);
                            break;
                        }
                        else
                        {
                            this.transform.position = new Vector3(this.transform.position.x - force, this.transform.position.y, this.transform.position.z);
                            break;
                        }
                }
                break;
            case Vector.Y:
                switch (positive)
                {
                    case true:
                        if (this.transform.position.y >= posYMax)
                        {
                            this.transform.position = new Vector3(this.transform.position.x, posYMax, this.transform.position.z);
                            break;
                        }
                        else
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + force, this.transform.position.z);
                            break;
                        }
                    case false:
                        if (this.transform.position.y <= posYMin)
                        {
                            this.transform.position = new Vector3(this.transform.position.x, posYMin, this.transform.position.z);
                            break;
                        }
                        else
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - force, this.transform.position.z);
                            break;
                        }
                }
                break;
            case Vector.Z:
                switch (positive)
                {
                    case true:
                        if (this.transform.position.z >= posZMax)
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, posZMax);
                            break;
                        }
                        else
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + force);
                            break;
                        }
                    case false:
                        if (this.transform.position.z <= posZMin)
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, posZMin);
                            break;
                        }
                        else
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - force);
                            break;
                        }
                }
                break;
            default:
                break;       
        }
    }
}
