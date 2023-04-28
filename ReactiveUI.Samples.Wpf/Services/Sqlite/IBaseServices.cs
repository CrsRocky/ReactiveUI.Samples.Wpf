using ReactiveUI.Samples.Wpf.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Services.Sqlite
{
    public interface IBaseServices<T> where T : class
    {
        Task<int> AddAsync(PeopleModel model);

        Task<IEnumerable<PeopleModel>> GetAllAsync();

        Task<int> RemoveAsync(int id);

        Task<PeopleModel> SearchAsync(int id);

        Task<int> UpDateAsync(PeopleModel model);
    }
}