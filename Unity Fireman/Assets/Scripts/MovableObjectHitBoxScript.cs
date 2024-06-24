using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectHitBoxScript : MonoBehaviour
{
    public MovableObjectScript movableObjectParentScript;
    public enum HitBoxPosition
    {
        Front,
        Back,
        Top,
        Bottom,
        Right,
        Left
    }

    public HitBoxPosition hitBoxPosition;
    public void GetsWater(float waterQuantity)
    {
        //Revisa en que posicion esta la hitbox, para saber hacia que direccion moverse
        switch (hitBoxPosition)
        {
            case HitBoxPosition.Front:
                movableObjectParentScript.MoveObject(MovableObjectScript.Vector.Z, true);
                break;
            case HitBoxPosition.Back:
                movableObjectParentScript.MoveObject(MovableObjectScript.Vector.Z, false);
                break;
            case HitBoxPosition.Left:
                movableObjectParentScript.MoveObject(MovableObjectScript.Vector.X, true);
                break;
            case HitBoxPosition.Right:
                movableObjectParentScript.MoveObject(MovableObjectScript.Vector.X, false);
                break;
            case HitBoxPosition.Top:
                movableObjectParentScript.MoveObject(MovableObjectScript.Vector.Y, false);
                break;
            case HitBoxPosition.Bottom:
                movableObjectParentScript.MoveObject(MovableObjectScript.Vector.Y, true);
                break;
            default:
                break;
        }
    }
}
