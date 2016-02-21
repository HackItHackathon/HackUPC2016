[System.Serializable]
public class Getuserid
{
    public int id;
}

[System.Serializable]
public class DBUser
{
    public float distance;
	public int id;
	public string nom;
	public float latitude;
	public float longitude;
	public int punt;
}

[System.Serializable]
public class Game
{
    public int gameId;
}

[System.Serializable]
public class Getnearid
{
    public DBUser[] table;
}

[System.Serializable]
public class Getuserinfo
{
    public int id;
    public int punt;
    public string nom;
    public float latitude;
    public float longitude;
    public int ida;
}


