using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerTypes
{
    public enum TrailerType
    {
         MilkTrailer, CowTrailer, FoodTrailer
    }

    public static string names(TrailerType trailerType)
    {
        switch (trailerType)
        {
            case TrailerType.MilkTrailer :
                return "den Milchtank";
            case TrailerType.CowTrailer :
                return "den Tiertransportanhänger";
            case  TrailerType.FoodTrailer:
                return " den großen Anhänger";
            default: return "";
        }
    }

    
}
