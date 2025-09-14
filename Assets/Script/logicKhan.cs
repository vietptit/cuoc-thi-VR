using UnityEngine;

public class YourClassName : MonoBehaviour
{
    [SerializeField] private Material mt;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("dapLua"))
        {
           
            MeshRenderer mr = GetComponentInChildren<MeshRenderer>();

            if (mr != null && mt != null)
            {
                mr.material = mt;
            }

            
        }
    }
}
