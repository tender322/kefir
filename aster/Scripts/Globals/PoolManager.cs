using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
   public static PoolManager Instance;
   private void Awake()=> Instance = this;

   class Pool
   {
      private List<GameObject> deactivated = new List<GameObject>();
      private GameObject fab;
      public Pool(GameObject fab) => this.fab = fab;
      
      public GameObject Spawn(Vector2 pos, Quaternion rot)
      {
         GameObject obj;
         if (deactivated.Count == 0)
         {
            obj = Instantiate(fab, pos, rot);
            obj.name = fab.name;
            obj.transform.SetParent(Instance.transform);
         }
         else
         {
            obj = deactivated[deactivated.Count - 1];
            deactivated.RemoveAt(deactivated.Count-1);
            obj.transform.position = pos;
            obj.transform.rotation = rot;
         }
         obj.SetActive(true);
         return obj;
      }

      public void Despawn(GameObject obj)
      {
         obj.SetActive(false);
         deactivated.Add(obj);
      }
   }

   private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

   void Init(GameObject prefab)
   {
      if (prefab != null && pools.ContainsKey(prefab.name) == false)
      {
         pools[prefab.name] = new Pool(prefab);
      }
   }
   public GameObject Spawn(GameObject prefab, Vector2 pos, Quaternion rot)
   {
      Init(prefab);
      return pools[prefab.name].Spawn(pos, rot);
   }

   public void Despawn(GameObject obj)
   {
      if (pools.ContainsKey(obj.name))
      {
         pools[obj.name].Despawn(obj);
      }
      else
      {
         Destroy(obj);
      }
   }

   public void Restart()
   {
      foreach (Transform child in Instance.transform)
      {
         Destroy(child.gameObject);
      }
      pools.Clear();
   }

}


