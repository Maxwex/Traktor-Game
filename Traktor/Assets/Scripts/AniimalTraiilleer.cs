using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniimalTraiilleer : MonoBehaviour
{

   [SerializeField] private GameObject[] content;

   private bool empty = true;

   public Trailer Trailer;
   

   private void OnTriggerEnter (Collider other)
   { 
      TargetBox box = other.GetComponent<TargetBox>();
      if (box != null)
      {
        
         if (box.name == "Kuhstall")
         {
            load(9);
         }
         else if (box.name == "Tierhändler" && empty)
         {
            load(0);
         }
         else
         {
            load(9);
         }
      }
   }

   private void load(int number)
   {
      foreach (var gameObject in content)
      {
         gameObject.SetActive(false);
      }

      empty = true;
      if (number > content.Length) return;
         content[number].SetActive(true);
         empty = false;
   }
}
