using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace SuperVolleyball;

public partial class AdminWin : Window
{
    public MyDB _Db = new MyDB();
    public List<Player> _players = new List<Player>();
    public AdminWin()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        try
        {
            _Db.OpenConnection();

            string sql =
                "SELECT surname, weight, height, birthday, startday, teams.teamName, positions.positionName FROM Players " +
                "JOIN teams ON Players.team = teams.teamID " +
                "JOIN positions ON Players.position = positions.teamID";

            MySqlCommand command = new MySqlCommand(sql, _Db.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var current = new Player()
                {
                    surname = reader.GetString("surname"),
                    position = reader.GetString("positionName"),
                    weight = reader.GetDouble("weight"),
                    height = reader.GetDouble("height"),
                    birthday = reader.GetDateTime("birthday"),
                    startday = reader.GetDateTime("startday"),
                    team = reader.GetString("teamName")
                };
                _players.Add(current);
            }

            PlayersGrid.ItemsSource = _players;

            _Db.CloseConnection();
        }
        catch(Exception error)
        {
            Console.WriteLine(error);
            Console.WriteLine("Проблемы с подключением");
        }
    }

    private void DeleteButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _Db.OpenConnection();

            //string deleteSQL = "DELETE FROM Players WHERE PlayerID = " + PlayersGrid.SelectedItem.GetValue("playerID");
            //MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, _Db.GetConnection());
            //deleteCommand.ExecuteScalar();
            
            _Db.CloseConnection();
            MessageBox box = new MessageBox();
            box.Show();

        }
        catch(Exception error)
        {
            Console.WriteLine("Не выбрано значение при удалении");
            Console.WriteLine(error);
        }
    }


    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        EditWindow win = new EditWindow();
        win.Show();
        //win.PlayerIdTextBlock.Text = PlayersGrid.SelectedItem.GetProperty.GetValue("PlayerID");
        this.Close();
    }
}