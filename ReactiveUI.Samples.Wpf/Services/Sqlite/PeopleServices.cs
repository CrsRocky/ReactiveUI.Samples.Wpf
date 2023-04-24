using ReactiveUI.Samples.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Services.Sqlite
{
    public class PeopleServices : IPeopleServices
    {
        public int AddAsync(PeopleModel model)
        {
            
        }

        public IEnumerable<PeopleModel> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public int RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public PeopleModel SearchAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int UpDateAsync(PeopleModel model)
        {
            throw new NotImplementedException();
        }
    }
}
