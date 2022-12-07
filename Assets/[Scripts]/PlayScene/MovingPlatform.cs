using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlatformType
{
    FALLING,
    RISING,
    CUSTOM_PATH,
}

public class MovingPlatform : MonoBehaviour
{
    public PlatformType type;

    [Header("Movement Properties")]
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isRising;
    [SerializeField] private bool isCustomMoving;
    [Range(0.0f, 2.0f)]
    public float SpeedFactor = 1.0f;

    [Header("Platform Path Points")]
    public List<Vector2> points;
    

    private float lerpInterp = 0.0f;

    private int currentPointIdx = 0;
    private int nextPointIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentPointIdx = 0;
        nextPointIdx = currentPointIdx + 1 >= points.Count ? 0 : currentPointIdx + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }

    private void CalculCustomInterp4Custom()
    {
        if (lerpInterp > 1.0f)
        {
            lerpInterp = 0.0f;
            currentPointIdx++;
            if (currentPointIdx >= points.Count)
            {
                currentPointIdx = 0;
            }
            nextPointIdx = currentPointIdx + 1 >= points.Count ? 0 : currentPointIdx + 1;
        }
    }


    private void Move()
    {

        switch (type)
        {
            case PlatformType.FALLING:
                {
                    if (isFalling)
                    {
                        lerpInterp += SpeedFactor * Time.deltaTime;
                    }
                    else
                    {
                        lerpInterp -= SpeedFactor * Time.deltaTime * 0.3f;
                    }

                    if (lerpInterp < 0.0f)
                    {
                        lerpInterp = 0.0f;
                    }

                    if (lerpInterp > 1.0f)
                    {
                        lerpInterp = 1.0f;
                    }

                    transform.position = Vector2.Lerp(points[0], points[1], lerpInterp);
                    break;
                }
            case PlatformType.RISING:
                {
                    if (isRising)
                    {
                        lerpInterp += SpeedFactor * Time.deltaTime * 0.3f;
                    }
                    else
                    {
                        lerpInterp -= SpeedFactor * Time.deltaTime;
                    }

                    if (lerpInterp < 0.0f)
                    {
                        lerpInterp = 0.0f;
                    }

                    if (lerpInterp > 1.0f)
                    {
                        lerpInterp = 1.0f;
                    }

                    transform.position = Vector2.Lerp(points[0], points[1], lerpInterp);
                    break;
                }
            
            case PlatformType.CUSTOM_PATH:
                if (isCustomMoving)
                {
                    CalculCustomInterp4Custom();
                    lerpInterp += SpeedFactor * Time.deltaTime;
                    //transform.position = new Vector3(
                    //    Mathf.Lerp(startPoint.x, startPoint.x + points[currentPointIdx].x, lerpInterp4Custom),
                    //    Mathf.Lerp(startPoint.y, startPoint.y + points[currentPointIdx].y, lerpInterp4Custom),
                    //    0.0f);
                    transform.position = Vector2.Lerp(points[currentPointIdx], points[nextPointIdx], lerpInterp);
                }

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().GetIsGround() && transform.position.y < collision.transform.position.y)
            {
                switch (type)
                {
                    case PlatformType.FALLING:
                        isFalling = true;
                        break;
                    case PlatformType.RISING:
                        isRising = true;
                        break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (type)
        {
            case PlatformType.FALLING:
                isFalling = false;
                break;
            case PlatformType.RISING:
                isRising = false;
                break;
            case PlatformType.CUSTOM_PATH:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            switch (type)
            {
                case PlatformType.FALLING:
                    isFalling = true;
                    break;
                case PlatformType.RISING:
                    isRising = true;
                    break;
                case PlatformType.CUSTOM_PATH:
                    isCustomMoving = true;
                    break;
            }
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    switch (type)
    //    {
    //        case PlatformType.FALLING:
    //            isFalling = false;
    //            break;
    //        case PlatformType.RISING:
    //            isRising = false;
    //            break;
    //    }
    //}
}
