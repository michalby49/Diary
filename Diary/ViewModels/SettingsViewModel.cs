using Diary.Commands;
using Diary.Model.Domains;
using Diary.Model.Wrappers;
using Diary.Properties;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        ServerWrapper serverWrapper = new ServerWrapper();
        
        public SettingsViewModel()
        {
            ServerWrapper = new ServerWrapper();
            
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
        }
 

        private ServerWrapper _serverWrapper;

        public ServerWrapper ServerWrapper
        {
            get { return _serverWrapper; }
            set
            {
                _serverWrapper = value;
                onPropertyChanged();
            }
        }
        private void Confirm(object obj)
        {
            SaveSettings();
            

            CloseWindow(obj as Window);
        }

        private void SaveSettings()
        {
            if(Test())
                Settings.Default.Save();
            else
            {
                _ = ApliactionRestart();
            }

        }

        private bool Test()
        {
            var serverWrapper = new ServerWrapper()
            if (ServerWrapper.Address == )
        }

        private async Task ApliactionRestart()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Restart", $"Aby kontynuować należy zrestartować aplikacje", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            Settings.Default.Save();
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }
        private void CloseWindow(Window window)
        {
            window.Close();
        }
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

    }
}
