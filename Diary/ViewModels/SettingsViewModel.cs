using Diary.Commands;
using Diary.Model.Domains;
using Diary.Model.Wrappers;
using Diary.Properties;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        
        public SettingsViewModel(bool needRestart)
        {
            ServerWrapper = new ServerWrapper();
            ServerWrapper.NeedRestart = needRestart;

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
                
            }
        }
        private void Confirm(object obj)
        {
            if (ServerWrapper.NeedRestart)
            {
                Settings.Default.Save();
                ApliactionRestart();
            }
            else
                Settings.Default.Save();
            
            CloseWindow(obj as Window);
        }

        
        private async Task ApliactionRestart()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Restart", $"Aby kontynuować należy zrestartować aplikacje", MessageDialogStyle.Affirmative);

            Settings.Default.Save();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();

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
