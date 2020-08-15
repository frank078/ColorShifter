using UnityEngine;

public class TowerObjectPulling : MonoBehaviour
{
    public int TowerNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bottom")
        {
            //Back to the top
            transform.position = new Vector3(transform.position.x, transform.position.y + 200, transform.position.z);
            
            GameManager.Instance.ModifyColoredWalls(TowerNumber); // run functions from GameManager
        }
    }
}
