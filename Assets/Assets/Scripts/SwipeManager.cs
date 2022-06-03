
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private bool stopTouch = false;
    public float minSwipeLength = 200f;
    void Update()
    {

        Swipe();
    }
    //kiem tra thao tac vuot la trai phai len hay xuong
    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
         
            firstPressPos = Input.mousePosition;       
        }
        if (Input.GetMouseButtonUp(0))
       {
            secondPressPos = Input.mousePosition;
            currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            if (currentSwipe.magnitude < minSwipeLength)
            {
                //swipeDirection = Swipe.None;
                return;
            }
            currentSwipe.Normalize();
            //up swipe
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
      
                if (!PlayerController.ins.isMoving)
                {
                    PlayerController.ins.direction = Vector3.forward;
          
                    return;
                }
               
            }
        
            else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
              {
                    if (!PlayerController.ins.isMoving)
                    {
                      
                        PlayerController.ins.direction = Vector3.back;
                
                        return;
                    }
            
                }
             }
            else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
              {
                    if (!PlayerController.ins.isMoving)
                    {
                     
                        PlayerController.ins.direction = Vector3.left;
                       
                        return;
                    }
                
                }
            // Swipe right
              } else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
              {
                    if (!PlayerController.ins.isMoving)
                    {
                     
                        PlayerController.ins.direction = Vector3.right;
                       
                        return;
                    }
             
                }
        
        }
    }
   
    }
}

