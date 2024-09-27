using Microsoft.Maui.Controls;
using System;
using IMProWater.Data;

namespace IMProWater
{
    public partial class RegisterPage : ContentPage
    {
        private bool _isPasswordVisible = false; // Flaga widoczno�ci has�a

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            // Sprawdzanie czy has�o spe�nia wymogi
            if (!IsPasswordValid(password))
            {
                await DisplayAlert("B��d", "Has�o musi zawiera� co najmniej 5 znak�w, w tym du�� liter�, znak specjalny oraz cyfr�.", "OK");
                return;
            }

            // Sprawdzenie, czy email ju� istnieje w bazie danych
            var existingUser = await Database.DatabaseInstance.Table<User>()
                                  .Where(u => u.Email == email)
                                  .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                await DisplayAlert("B��d", "Podany email istnieje w bazie danych", "OK");
            }
            else
            {
                var user = new User { Email = email, Password = password };
                await Database.DatabaseInstance.InsertAsync(user);

                await DisplayAlert("Sukces", "Rejestracja zako�czona pomy�lnie!", "OK");
                await Navigation.PushAsync(new MainPage());
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

        // Funkcja sprawdzaj�ca poprawno�� has�a
        private bool IsPasswordValid(string password)
        {
            if (password.Length < 5) return false;

            bool hasUpperCase = System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]");
            bool hasSpecialChar = System.Text.RegularExpressions.Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]");
            bool hasDigit = System.Text.RegularExpressions.Regex.IsMatch(password, @"\d");

            return hasUpperCase && hasSpecialChar && hasDigit;
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
