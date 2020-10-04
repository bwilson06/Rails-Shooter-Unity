using UnityEngine;
 
 [ExecuteInEditMode]
 public class WorldOffset : MonoBehaviour 
 {
     public Vector3 worldOffset;
 
     public void LateUpdate()
     {
         transform.position = transform.parent.position + worldOffset;
     }
 }
