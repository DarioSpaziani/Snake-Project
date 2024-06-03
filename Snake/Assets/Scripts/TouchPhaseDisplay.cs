using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TouchPhaseDisplay : MonoBehaviour
{
    public TextMeshProUGUI directionText;
    private Touch touch;
    private Vector2 touchStartPos, touchEndPos;
    public Vector2 direction;
    public string dir;
    public float xDir;
    public float yDir;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;

                xDir = touchEndPos.x - touchStartPos.x;
                yDir = touchEndPos.y - touchStartPos.y;

                if(Mathf.Abs(xDir) > Mathf.Abs(yDir)) { 
                    direction = xDir > 0 ? Vector2.right : Vector2.left;
                    dir = xDir > 0 ? "right" : "left";
                }
                else
                {
                    direction = yDir > 0 ? Vector2.up : Vector2.down;
                    dir = yDir > 0 ? "up" : "down";
                }
            }
        }
        directionText.text = dir;
        Debug.Log(xDir);
        Debug.Log(yDir);
    }
}
