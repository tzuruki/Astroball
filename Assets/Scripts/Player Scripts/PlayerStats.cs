// This is a bit of a weird hack to keep track of stuff between scenes. not sure if its good yet or not but it seems to work!
using System.Collections.Generic;
using System.Linq;

public static class PlayerStats
{
    public class DoorKey
    {
        public string colour { get; set; }
        public int level { get; set; }
    }

    private static int points, health = 3;
    private static float mouseSens = 1;
    private static List<DoorKey> keyList = new List<DoorKey>();
    private static bool hasCollectedShipPart;

    public static float TestFloatValue = 0;

    public const int STARTING_HEALTH = 3;

    public static int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }

    public static int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public static float MouseSensitivity
    {
        get
        {
            return mouseSens;
        }
        set
        {
            mouseSens = value;
        }
    }

    public static bool HasCollectedShipPart
    {
        get
        {
            return hasCollectedShipPart;
        }
        set
        {
            hasCollectedShipPart = value;
        }
    }

    public static IEnumerable<DoorKey> GetKeys()
    {
        if (keyList == null)
        {
            keyList = new List<DoorKey>();
        }

        return keyList;
    }

    public static void AddKey(DoorKey key)
    {
        keyList.Add(key);
    }

    public static bool hasKey(string colour, int level)
    {
        return keyList.Where(k => ((k.colour == colour) && (k.level == level))).FirstOrDefault() != null;
    }

    public static bool RemoveKeyOfColourForLevel(string colour, int level)
    {
        DoorKey toRemove = keyList.Where(k => ((k.colour == colour) && (k.level == level))).FirstOrDefault();

        if(toRemove != null)
        {
            keyList.Remove(toRemove);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public static void ResetPlayerStats()
    {
        keyList.Clear();
        health = STARTING_HEALTH;
        points = 0;
        hasCollectedShipPart = false;
        MouseSensitivity = 1;
    }



}