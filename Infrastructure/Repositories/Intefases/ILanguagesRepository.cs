using Application_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Intefases
{
    public interface ILanguagesRepository
    {
        Languages Add(Languages item);
        Languages Update(Languages item);
        Languages Delete(int id);
        Languages Get(int id);
        List<Languages> All();
    }
}
