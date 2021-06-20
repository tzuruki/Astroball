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

    private static int kills, deaths, assists, points, health;
    private static List<DoorKey> keyList;

    public static int Kills
    {
        get
        {
            return kills;
        }
        set
        {
            kills = value;
        }
    }

    public static int Deaths
    {
        get
        {
            return deaths;
        }
        set
        {
            deaths = value;
        }
    }

    public static int Assists
    {
        get
        {
            return assists;
        }
        set
        {
            assists = value;
        }
    }

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

}