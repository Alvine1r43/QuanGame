﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
   protected static PlayerData instance;

    public static PlayerData GetInstance()
    {
        if(instance == null)
        {
            instance = new PlayerData();
        }
        return instance;
    }

}
