using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace SuperVolleyball;

public partial class MainWindow : Window
{
    private MyDB _db = new MyDB();
    public MainWindow()
    {
        InitializeComponent();
    }

    public void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _db.OpenConnection();

        string loginSQL = "SELECT Login FROM Players WHERE login = '" + LoginTextBox.Text + "';";
        string passwordSQL = "SELECT Password FROM Players WHERE login = '" + LoginTextBox.Text + "';";
        string roleSQL = "SELECT Role FROM Players WHERE login = '" + LoginTextBox.Text + "';";
        
        MySqlCommand loginCommand = new MySqlCommand(loginSQL, _db.GetConnection());
        MySqlCommand passwordCommand = new MySqlCommand(passwordSQL, _db.GetConnection());
        MySqlCommand roleCommand = new MySqlCommand(roleSQL, _db.GetConnection());

        string login = loginCommand.ExecuteScalar().ToString();
        string password = passwordCommand.ExecuteScalar().ToString();
        int role = Convert.ToInt32(roleCommand.ExecuteScalar());

        if (login != null && LoginTextBox.Text != null && PassTextBox.Text != null)
        {
            if (PassTextBox.Text == password)
            {
                if (role == 1)
                {
                    AdminWin win = new AdminWin();
                    win.Show();
                    this.Close();
                }
                
                if (role == 2)
                {
                    ManagerWin win = new ManagerWin();
                    win.Show();
                    this.Close();
                }
            }
        }
        _db.CloseConnection();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        AdminWin win = new AdminWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick2(object? sender, RoutedEventArgs e)
    {
        ManagerWin win = new ManagerWin();
        win.Show();
        this.Close();
    }
}