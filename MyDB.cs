using MySql.Data.MySqlClient;

namespace SuperVolleyball;

public class MyDB
{
    public MySqlConnection _Connection =
        new MySqlConnection("SERVER=SERVER;DATABASE=DATABASE;UID=UID;PASSWORD=PASSWORD;");

    public void OpenConnection()
    {
        _Connection.Open();
    }

    public void CloseConnection()
    {
        _Connection.Close();
    }

    public MySqlConnection GetConnection()
    {
        return _Connection;
    }
}