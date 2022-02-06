using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using Newtonsoft.Json.Converters;


public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;
    // Start is called before the first frame update
    void Start()
    {
        //
        discord = new Discord.Discord(898522522520289322, (UInt64)Discord.CreateFlags.NoRequireDiscord);
        var activityManager = discord.GetActivityManager();
        
        var activity = new Discord.Activity
        {
            Assets =
            {
                LargeImage = "trakki",
            },
        };
        activityManager.UpdateActivity(activity, (result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("Success!");
            }

        });
    }

   
    // Update is called once per frame
    void Update()
    {
        
            discord.RunCallbacks();
        
        
    }
}
