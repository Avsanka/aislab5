using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AISLab5
{
    /// <summary>
    /// Логика взаимодействия для RequestWindow.xaml
    /// </summary>
    /// строго типизированные объекты


    public partial class RequestWindow : Window
    {
        private MainWindow mw;
        private string f; //строка для хранения данных
        public RequestWindow(MainWindow MW)
        {
            InitializeComponent();
            mw = MW;
            f = "";
        }

        private void Button_Click_Data(object sender, RoutedEventArgs e)
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.154";
            string method = "account.getProfileInfo";
            f = mw.GET(reqStrTemplate, method, mw.Access_token);
            var user = JsonSerializer.Deserialize<Rootobject>(f).response;
            string[] list =
            {
                "id: " + user.id.ToString(),
                "Фамилия: " + user.last_name,
                "Имя: " + user.first_name,
                "Псевдоним: " + user.screen_name,
                "Дата рождения: " + Convert.ToDateTime(user.bdate).ToLongDateString(),
                "Пол: " + sexFormat(user.sex),
                "Семейное положение: " + relationFormat(user.relation),
                "Номер телефона: +" + user.phone,
                "Город: " + user.home_town,
                "Страна: " + user.country.title

            };
            UserInformationTextBox.Text = string.Join("\n", list);

        }

        private void Button_Click_Friends(object sender, RoutedEventArgs e)
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.154&user_id=" + mw.UserID;
            string method = "friends.getOnline";
            f = mw.GET(reqStrTemplate, method, mw.Access_token);
            var ArrayOfFriends = JsonSerializer.Deserialize<myFriends>(f).response;

            reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.154&user_ids=" + string.Join(",", ArrayOfFriends.Select(x => x.ToString()));
            method = "users.get";
            f = mw.GET(reqStrTemplate, method, mw.Access_token);
            var friends = JsonDocument.Parse(f).RootElement.GetProperty("response");
            var Users = JsonSerializer.Deserialize<User[]>(friends);
            UserInformationTextBox.Text = string.Join("\n", Users.Select(x => x.last_name + " " + x.first_name));
        }

        private string sexFormat (int sex)
        {
            switch (sex)
            {
                case 1:
                    return "Женский";
                case 2:
                    return "Мужской";
                default:
                    return "Не указан";
            }
                  
        }

        private string relationFormat(int relation)
        {
            switch (relation)
            {
                case 1:
                    return "Не женат/не замужем";
                case 2:
                    return "Есть друг/подруга";
                case 3:
                    return "Помолвелн(а)";
                case 4:
                    return "Женат/замужем";
                case 5:
                    return "Всё сложно";
                case 6:
                    return "В активном поиске";
                case 7:
                    return "Влюблён/влюблена";
                case 8:
                    return "В гражданском браке";
                default:
                    return "Не указано";
            }
        }
    }
}
