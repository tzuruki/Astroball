// This is a bit of a weird hack to keep track of stuff between scenes. not sure if its good yet or not but it seems to work!
public static class PlayerStats
{
    private static int kills, deaths, assists, points;

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
}