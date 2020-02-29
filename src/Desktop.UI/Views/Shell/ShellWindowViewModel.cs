using Prism.Mvvm;

namespace Departments.Views.Shell
{
    public class ShellWindowViewModel:BindableBase
    {
        private string _title;

        public ShellWindowViewModel()
        {
            _title = "Departments";
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
