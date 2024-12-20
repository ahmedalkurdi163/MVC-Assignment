using Application_Domain.Models;
using Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Intefases
{
    public interface ITVShowRepository
    {
        Task<TVShow?> Update(int TVShowId, TVShowUpdateModel patchDocument);
        Task<TVShow?> Delete(int id);
        TVShow Get(int id, bool includeAttachment_img = false);
        List<TVShow> All();
        Task<TVShow?> Create(TVShowCreateModel newTVShow);
        Task SaveChangesAcync();
        void SaveChanges();
        List<Languages> GetLanguagesForTvShow(int? id);

    }
}
