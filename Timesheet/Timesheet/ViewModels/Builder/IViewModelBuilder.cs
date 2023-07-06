using System.Threading.Tasks;

namespace Timesheet.ViewModels.Builder
{
    public interface IViewModelBuilder<T>
    {
        Task<T> BuildViewModel();
        Task<T> BuildViewModel(string data);
    }
}