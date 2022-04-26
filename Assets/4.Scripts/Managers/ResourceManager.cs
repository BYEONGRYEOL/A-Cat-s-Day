using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class ResourceManager
    {
        public T Load<T>(string filepath) where T: Object
        {
            return Resources.Load<T>(filepath);
        }

        public GameObject Instantiate(string filepath, Transform parent = null)
        {
            GameObject prefab = Load<GameObject>($"Prefabs/{filepath}");
            if (prefab == null)
            {
                Debug.Log($"Failed to load prefab : {filepath}");
                return null;
            }
            GameObject go =  Object.Instantiate(prefab, parent);
            int idx = go.name.IndexOf("(Clone)");
            if (idx > 0)
            {
                go.name = go.name.Substring(0, idx);

            }
            return go;
            
        }

        public void Destroy(GameObject go)
        {
            if(go == null)
            {
                Debug.Log($"Failed to Destroy GameObject : {go.name}");
                return;
            }
            Object.Destroy(go);
        }
    }

}