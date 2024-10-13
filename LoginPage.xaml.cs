using Microsoft.Maui.Controls;
using System;
using IMProWater.Data;

namespace IMProWater
{
    public partial class LoginPage : ContentPage
    {
        private bool _isPasswordVisible = false; // Flaga widoczno�ci has�a

        public LoginPage()
        {
            InitializeComponent(); // Musi by� wywo�ane, aby zainicjowa� elementy XAML
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            var user = await Database.DatabaseInstance.Table<User>()
                             .Where(u => u.Email == email && u.Password == password)
                             .FirstOrDefaultAsync();

            if (user != null)
            {
                await DisplayAlert("Sukces", "Zalogowano pomy�lnie!", "OK");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("B��d", "Nieprawid�owy email lub has�o", "OK");
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }

        // Funkcja obs�uguj�ca widoczno�� has�a
        private void OnTogglePasswordVisibilityClicked(object sender, EventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;
            PasswordEntry.IsPassword = !_isPasswordVisible;

            ((ImageButton)sender).Source = _isPasswordVisible ? "eye_off.png" : "eye.png";
        }
    }
}
