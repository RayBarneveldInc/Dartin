namespace Dartin.ViewModels
{
    public interface IViewModel
    {
        public string ViewName { get; }

        public void OnExit();
    }
}