using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WpfCascadeApp.Models;

namespace WpfCascadeApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private City _selectedCity;
        private Workshop _selectedWorkshop;
        private Employee _selectedEmployee;
        private string _selectedBrigade;
        private string _shift;

        public ObservableCollection<City> Cities { get; set; }
        public ObservableCollection<Workshop> AllWorkshops { get; set; }
        public ObservableCollection<Workshop> FilteredWorkshops { get; set; }
        public ObservableCollection<Employee> AllEmployees { get; set; }
        public ObservableCollection<Employee> FilteredEmployees { get; set; }
        public ObservableCollection<string> Brigades { get; set; }

        public ICommand SaveCommand { get; set; }

        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
                FilterWorkshops();
            }
        }

        public Workshop SelectedWorkshop
        {
            get => _selectedWorkshop;
            set
            {
                _selectedWorkshop = value;
                OnPropertyChanged(nameof(SelectedWorkshop));
                FilterEmployees();
            }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public string SelectedBrigade
        {
            get => _selectedBrigade;
            set
            {
                _selectedBrigade = value;
                OnPropertyChanged(nameof(SelectedBrigade));
            }
        }

        public string Shift
        {
            get => _shift;
            set
            {
                _shift = value;
                OnPropertyChanged(nameof(Shift));
            }
        }

        public MainViewModel()
        {
            Cities = new ObservableCollection<City>
            {
                new City { Name = "Москва" },
                new City { Name = "Санкт-Петербург" },
                new City { Name = "Новосибирск" }
            };

            AllWorkshops = new ObservableCollection<Workshop>
            {
                new Workshop { Name = "Автосервис Центральный", CityName = "Москва" },
                new Workshop { Name = "Автосервис Северный", CityName = "Москва" },
                new Workshop { Name = "Автосервис Западный", CityName = "Москва" },
                new Workshop { Name = "Автосервис Невский", CityName = "Санкт-Петербург" },
                new Workshop { Name = "Автосервис Василеостровский", CityName = "Санкт-Петербург" },
                new Workshop { Name = "Автосервис Петроградский", CityName = "Санкт-Петербург" },
                new Workshop { Name = "Автосервис Ленинский", CityName = "Новосибирск" },
                new Workshop { Name = "Автосервис Октябрьский", CityName = "Новосибирск" }
            };

            AllEmployees = new ObservableCollection<Employee>
            {
                new Employee { Name = "Иванов И.И.", WorkshopName = "Автосервис Центральный" },
                new Employee { Name = "Петров П.П.", WorkshopName = "Автосервис Центральный" },
                new Employee { Name = "Сидоров С.С.", WorkshopName = "Автосервис Северный" },
                new Employee { Name = "Козлов К.К.", WorkshopName = "Автосервис Северный" },
                new Employee { Name = "Новиков Н.Н.", WorkshopName = "Автосервис Западный" },
                new Employee { Name = "Морозов М.М.", WorkshopName = "Автосервис Западный" },
                new Employee { Name = "Волков В.В.", WorkshopName = "Автосервис Невский" },
                new Employee { Name = "Алексеев А.А.", WorkshopName = "Автосервис Невский" },
                new Employee { Name = "Лебедев Л.Л.", WorkshopName = "Автосервис Василеостровский" },
                new Employee { Name = "Семенов С.С.", WorkshopName = "Автосервис Василеостровский" },
                new Employee { Name = "Егоров Е.Е.", WorkshopName = "Автосервис Петроградский" },
                new Employee { Name = "Павлов П.П.", WorkshopName = "Автосервис Петроградский" },
                new Employee { Name = "Козлов К.К.", WorkshopName = "Автосервис Ленинский" },
                new Employee { Name = "Степанов С.С.", WorkshopName = "Автосервис Ленинский" },
                new Employee { Name = "Николаев Н.Н.", WorkshopName = "Автосервис Октябрьский" },
                new Employee { Name = "Орлов О.О.", WorkshopName = "Автосервис Октябрьский" }
            };

            Brigades = new ObservableCollection<string>
            {
                "A", "B", "C"
            };

            FilteredWorkshops = new ObservableCollection<Workshop>();
            FilteredEmployees = new ObservableCollection<Employee>();

            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        }

        private void FilterWorkshops()
        {
            FilteredWorkshops.Clear();
            
            if (SelectedCity != null)
            {
                var filtered = AllWorkshops.Where(w => w.CityName == SelectedCity.Name);
                foreach (var workshop in filtered)
                {
                    FilteredWorkshops.Add(workshop);
                }
            }
            
            SelectedWorkshop = null;
        }

        private void FilterEmployees()
        {
            FilteredEmployees.Clear();
            
            if (SelectedWorkshop != null)
            {
                var filtered = AllEmployees.Where(e => e.WorkshopName == SelectedWorkshop.Name);
                foreach (var employee in filtered)
                {
                    FilteredEmployees.Add(employee);
                }
            }
            
            SelectedEmployee = null;
        }

        private void ExecuteSave(object parameter)
        {
            string message = $"Выбранные данные:\n" +
                           $"Город: {SelectedCity?.Name ?? "Не выбран"}\n" +
                           $"Автосервис: {SelectedWorkshop?.Name ?? "Не выбран"}\n" +
                           $"Сотрудник: {SelectedEmployee?.Name ?? "Не выбран"}\n" +
                           $"Бригада: {SelectedBrigade ?? "Не выбрана"}\n" +
                           $"Смена: {Shift ?? "Не введена"}";

            System.Windows.MessageBox.Show(message, "Сохранение", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private bool CanExecuteSave(object parameter)
        {
            return true; // Always enabled for this example
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Simple RelayCommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
