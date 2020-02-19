using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour, KartGame.KartSystems.IInput
{
    [SerializeField]
    [Tooltip("Distance to check for a wall to accelerate or not")]
    protected float checkWall = 1;

    [SerializeField]
    [Tooltip("Acceleration if a wall is in front of me")]
    protected float accelerationIfWall = -1;


    [Range(0, 0.5f)]
    [Tooltip("Screen percentage to hop")]
    protected float screenToHop = 0.2f;
    
    [Range(0, 0.5f)]
    [Tooltip("Screen percentage to hop")]
    protected float deadArea = 0.2f;

    protected Vector2 deadAreaLimits;

    public float Acceleration { get; protected set; }

    public float Steering  {get; protected set;}

    public bool BoostPressed { get; protected set; }

    public bool FirePressed { get; protected set; }

    public bool HopPressed { get; protected set; }

    public bool HopHeld { get; protected set; }

    protected List<Touch> startTouches = new List<Touch>();
    protected List<Vector2> diffPosList = new List<Vector2>();
    protected int lastFingerDrift = -1;

    private void Start()
    {
        var half = Screen.width / 2f;
        var deadAreaPixels = Screen.width * deadArea;
        deadAreaLimits = new Vector2(half - deadAreaPixels, half + deadAreaPixels);
    }
    public void Update()
    {
        ManageAcceleration();
        ManageTouches();
    }

    /// <summary>
    /// Manage the touches list
    /// </summary>
    private void ManageTouches()
    {
        ManageSteering();

        ClearNonUsedTouches();
        CheckForDrifting();
    }

    private void ManageSteering()
    {
        bool steeringManaged = false;
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Touch touch = Input.GetTouch(i);
            bool isNew = IsNewTouch(touch);
            //if there is a new touch we want to drive with it
            if ((isNew && !steeringManaged) || Input.touchCount == 1)
            {
                if (touch.position.x < deadAreaLimits.x)
                {
                    //on left of the dead area
                    Steering = -1;
                    steeringManaged = true;
                }
                else
                {
                    //on right of dead area
                    if (touch.position.x > deadAreaLimits.y)
                    {
                        Steering = 1;
                        steeringManaged = true;
                    }
                }
            }
        }
        //Restart steering if no inputs
        if (Input.touchCount == 0)
        {
            Steering = 0;
        }
    }

    private void CheckForDrifting()
    {
        for(int i = 0; i < diffPosList.Count; ++i)
        {
            float screenY = diffPosList[i].y / Screen.height;
            if(screenY > screenToHop)
            {
                HopHeld = true;
                HopPressed = lastFingerDrift != startTouches[i].fingerId;
                lastFingerDrift = startTouches[i].fingerId;
                return;
            }
        }
        HopHeld = false;
        HopPressed = false;
        lastFingerDrift = -1;
    }

    private void ClearNonUsedTouches()
    {
        for(int i = 0; i < startTouches.Count; ++i)
        {
            for(int j = 0; j < Input.touchCount; ++j)
            {
                //touch is in use
                if(startTouches[i].fingerId == Input.GetTouch(j).fingerId)
                {
                    break;
                }
                //non used touch
                if(j == Input.touchCount - 1)
                {
                    startTouches.RemoveAt(i);
                    diffPosList.RemoveAt(i--);
                }
            }
        }
    }

    /// <summary>
    /// Store new touch if is new
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    private bool IsNewTouch(Touch touch)
    {
        for(int i = 0; i < startTouches.Count; ++i)
        {
            var savedTouch = startTouches[i];
            if (touch.fingerId == savedTouch.fingerId)
            {
                diffPosList.Insert(i, touch.position - savedTouch.position);
                diffPosList.RemoveAt(i + 1);
                return false;
            }
        }
        startTouches.Add(touch);
        diffPosList.Add(Vector2.zero);
        return true;
    }

    /// <summary>
    /// Will manage acceleration checking for obstacles
    /// </summary>
    private void ManageAcceleration()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(!Physics.Raycast(ray, checkWall))
        {
            Acceleration = 1;
        }
        else
        {
            Acceleration = accelerationIfWall;
        }
    }

}
