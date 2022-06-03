using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Transform playerMesh;
    public float speed;
    public float maxDistance;
    public float HighStack =0.3f;
    bool canMove = false;
    public bool isMoving = false;
    Vector3 target;
    public Vector3 direction;
    public static PlayerController ins;
    public Animator AnimatorPlayer;
    bool canJump = true;
    public Transform PosStack;
    public GameObject EarnStackPrefab;
    public Stack<GameObject> brickStacks;
    private void Awake()
    {
        HighStack = 0.3f;
        ins = this;
        brickStacks = new Stack<GameObject>();
    }
  
    void Update()
    {
      
        
        if (canMove)
        {
         
            isMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if(Physics.Raycast(transform.position, this.direction, out hit, this.maxDistance, 1))
        {
    
            if (hit.collider.tag!="Wall")
            {
              
                canMove = true;
                target = hit.transform.position;
                canJump = true;
             
            }
            else
            {

          
                canMove =false;
                isMoving = false;
                if (canJump)
                {
                   AnimatorPlayer.Play("Dance");
                    AnimatorPlayer.Play("Idle");
                    canJump = false;
                }
            }
        }
    }
    RaycastHit hit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edible Stack"))
        {
         
           if (!other.GetComponent<EdibleStack>().ate)
            {
                other.GetComponent<EdibleStack>().EarnStack();
                EarnStack();

            }
        }
        if (other.CompareTag("EmptyStack"))
        {
            if (!other.GetComponent<FillStack>().isFill)
            {
                other.GetComponent<FillStack>().isFillStack();
                DeleteStack(other.gameObject);

            }
        }
        if (other.CompareTag("WinPoint"))
        {
            isMoving = false;
            AnimatorPlayer.Play("WinDance");
        }
    }

    public void EarnStack()
    {
        GameObject stackEarn = Instantiate(EarnStackPrefab, PosStack.transform);
        brickStacks.Push(stackEarn);
       
        stackEarn.transform.localPosition = new Vector3(0, (brickStacks.Count - 1) * HighStack, 0);
        playerMesh.transform.localPosition = new Vector3(playerMesh.transform.localPosition.x, 0.3f * brickStacks.Count, playerMesh.transform.localPosition.z);
       
    }
    public void DeleteStack(GameObject brickPosition)
    {
        GameObject stack = brickStacks.Pop();
        stack.transform.position = new Vector3(brickPosition.transform.position.x, playerMesh.position.y - HighStack, brickPosition.transform.position.z);
        Instantiate(EarnStackPrefab, brickPosition.transform);
        Destroy(stack);
        playerMesh.transform.localPosition = new Vector3(playerMesh.transform.localPosition.x, 0.3f * brickStacks.Count, playerMesh.transform.localPosition.z);
    }
 
}
