using Microsoft.Maui.Controls;
using System;

namespace IMProWater
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnSectionsButtonClicked(object sender, EventArgs e)
        {
            // Przekazujemy domy�lny numer sekcji, np. "1"
            await Navigation.PushAsync(new SectionsPage("1"));
        }

        private async void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            // Przejd� do strony Ustawienia
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void OnStatusButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatusPage());
        }

        // Logika wylogowania
        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            // Przekierowanie u�ytkownika do MainPage
            await Navigation.PushAsync(new MainPage());
        }

        // Funkcja obs�uguj�ca zamkni�cie aplikacji
        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
    }
}
