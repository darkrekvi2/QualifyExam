using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace SuperVolleyball;

public partial class EditWindow : Window
{
    private MyDB _db = new MyDB();
    public EditWindow()
    {
        InitializeComponent();
    }

    private void SavebButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _db.OpenConnection();

        string editSQL = "UPDATE Players SET surname = '" + SurnameTextBox.Text + "',  position = '" + PositionComboBox.SelectedItem + "', weight = '" + WeightTextBox.Text + "'," +
                         " height = '" + HeightTextBox.Text + "', birthday = " + BirthdayPicker.SelectedDate + ", startday = " + StartdayPicker.SelectedDate + "," +
                         "team = '" + TeamComboBox.SelectedItem.ToString() + "'" +
                         "WHERE playerID = " + Convert.ToInt32(PlayerIdTextBlock.Text) + ";";

        MySqlCommand command = new MySqlCommand(editSQL, _db.GetConnection());
        command.ExecuteScalar();
        
        _db.CloseConnection();
        
        MessageBox box = new MessageBox();
        box.Show();
        
        AdminWin win = new AdminWin();
        win.Show();
        this.Close();
    }
}