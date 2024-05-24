using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
     public interface IExploreService
    {
        void AddExplore(Explore explore);
        void RemoveExplore(int id);
        void UpdateExplore(int id, Explore newexplore);
        Explore GetExplore(Func<Explore, bool>? func=null);
        List<Explore> GetAllExplore(Func<Explore,bool>? func=null);
    }
}
