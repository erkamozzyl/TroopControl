
    using UnityEngine;

    public static class Extensions
    {
      
         public static bool HasComponent<T>(this RaycastHit r) where T : Component
        {
            return r.transform.GetComponent<T>() != null;
        }
        // extension methods , generic, static.
        
    }
