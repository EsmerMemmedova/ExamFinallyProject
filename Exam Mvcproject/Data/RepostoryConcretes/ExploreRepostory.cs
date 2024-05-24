using Core.Models;
using Core.RepostoryAbstract;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepostoryConcretes
{
    public class ExploreRepostory : GenericRepostory<Explore>, IExploreRepostory
    {
        public ExploreRepostory(AppDbContext context) : base(context)
        {
        }
    }
}
