using ReactiveUI.Samples.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Services.Sqlite
{
    public interface IBaseServices<T> where T : class
    {
        int AddAsync(T model);

        int RemoveAsync(int id);

        T SearchAsync(int id);

        int UpDateAsync(T model);

        IEnumerable<T> GetAllAsync();
    }
}
