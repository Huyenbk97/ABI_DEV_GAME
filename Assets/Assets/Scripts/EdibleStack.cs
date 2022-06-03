
using UnityEngine;

public class EdibleStack: MonoBehaviour
{
    public GameObject EdibleModel;
    public bool ate = false;
 
    public void EarnStack()
    {
        ate = true;
        EdibleModel.SetActive(false);
    }

}
