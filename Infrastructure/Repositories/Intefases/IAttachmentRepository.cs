using Application_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Intefases
{
    public interface IAttachmentRepository
    {
        Task<Attachment> Add(Attachment attachment);
        Task<Attachment> Update(Attachment attachment);
        Task<Attachment> Delete(int id);
        Task<Attachment> Get(int id);
        Task<List<Attachment>> All();
    }
}