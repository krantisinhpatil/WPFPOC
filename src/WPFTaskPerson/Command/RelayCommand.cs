using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace WPFTaskPerson.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action DoWork;
        public RelayCommand(Action work)
        {
            DoWork = work;
        }

        public bool CanExecute(object? parameter)
        {
           return Validator.TryValidateObject(this, new ValidationContext(this), null);
        }
        //new
        public void RaisedCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
        public void Execute(object? parameter)
        {
            DoWork();
        }
    }
}
