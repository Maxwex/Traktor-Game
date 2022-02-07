using UnityEngine;

public class BigTrailer : MonoBehaviour
{

   [SerializeField] private GameObject[] content;

   private bool empty = true;

   public Trailer Trailer;
   

   private void OnTriggerEnter (Collider other)
   { 
      
      TargetBox box = other.GetComponent<TargetBox>();
      if (box != null)
      {

         if (box.name == "Silo")
         {
            load(0);
         }
         else if (box.name == "Hühnerstall")
         {
            if (empty)
            {
               load(1);
            }
            else
            {
               load(9);
            }
         }
         else if (box.name == "Gewächshaus")
         {
            load(2);
         }
         else if (box.name == "Tierhändler")
         {
            load(1);
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
