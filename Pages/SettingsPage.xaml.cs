namespace TrainingControlPanelDashboard.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Load saved settings (in a real app, these would come from preferences/database)
            DefaultDurationPicker.SelectedIndex = 2; // 1 hour
            TimeFormatPicker.SelectedIndex = 0; // 12-hour
            WeightUnitPicker.SelectedIndex = 0; // Kilograms
        }

        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Edit Profile", "Profile editing functionality will be implemented", "OK");
        }

        private async void OnExportDataClicked(object sender, EventArgs e)
        {
            try
            {
                // In a real app, this would export data to a file
                await DisplayAlert("Export Data", "Data has been exported successfully", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to export data: {ex.Message}", "OK");
            }
        }

        private async void OnImportDataClicked(object sender, EventArgs e)
        {
            try
            {
                // In a real app, this would import data from a file
                await DisplayAlert("Import Data", "Data import functionality will be implemented", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to import data: {ex.Message}", "OK");
            }
        }

        private async void OnBackupSettingsClicked(object sender, EventArgs e)
        {
            try
            {
                // In a real app, this would backup settings to cloud storage
                await DisplayAlert("Backup Settings", "Settings have been backed up successfully", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to backup settings: {ex.Message}", "OK");
            }
        }

        private async void OnPrivacyPolicyClicked(object sender, EventArgs e)
        {
            // In a real app, this would open the privacy policy
            await DisplayAlert("Privacy Policy", "Privacy policy would be displayed here", "OK");
        }

        private async void OnTermsOfServiceClicked(object sender, EventArgs e)
        {
            // In a real app, this would open the terms of service
            await DisplayAlert("Terms of Service", "Terms of service would be displayed here", "OK");
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (confirm)
            {
                // In a real app, this would clear user session and navigate to login
                await DisplayAlert("Logout", "You have been logged out successfully", "OK");
            }
        }
    }
}
