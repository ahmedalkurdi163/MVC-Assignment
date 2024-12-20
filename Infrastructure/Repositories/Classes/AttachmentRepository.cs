using Application_Domain.Models;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Intefases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Classes
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AttachmentRepository> _logger;

        public AttachmentRepository(AppDbContext context, ILogger<AttachmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Attachment> Add(Attachment attachment)
        {
            if (attachment == null)
            {
                _logger.LogWarning("Attempted to add a null attachment.");
                return null;
            }

            try
            {
                await _context.Attachments.AddAsync(attachment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Attachment with ID {AttachmentId} added successfully.", attachment.AttachmentId);
                return attachment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an attachment.");
                return null;
            }
        }

        public async Task<List<Attachment>> All()
        {
            try
            {
                var attachments = await _context.Attachments.ToListAsync();
                _logger.LogInformation("Retrieved {Count} attachments successfully.", attachments.Count);
                return attachments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all attachments.");
                return new List<Attachment>();
            }
        }

        public async Task<Attachment> Delete(int id)
        {
            try
            {
                var attachment = await _context.Attachments.FindAsync(id);
                if (attachment == null)
                {
                    _logger.LogWarning("Attempted to delete a non-existent attachment with ID {AttachmentId}.", id);
                    return null;
                }

                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Attachment with ID {AttachmentId} deleted successfully.", id);
                return attachment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the attachment with ID {AttachmentId}.", id);
                return null;
            }
        }

        public async Task<Attachment> Get(int id)
        {
            try
            {
                var attachment = await _context.Attachments.FirstOrDefaultAsync(t => t.AttachmentId == id);
                if (attachment == null)
                {
                    _logger.LogWarning("No attachment found with ID {AttachmentId}.", id);
                }
                else
                {
                    _logger.LogInformation("Attachment with ID {AttachmentId} retrieved successfully.", id);
                }
                return attachment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the attachment with ID {AttachmentId}.", id);
                return null;
            }
        }

        public async Task<Attachment> Update(Attachment attachment)
        {
            if (attachment == null)
            {
                _logger.LogWarning("Attempted to update a null attachment.");
                return null;
            }

            try
            {
                _context.Attachments.Update(attachment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Attachment with ID {AttachmentId} updated successfully.", attachment.AttachmentId);
                return attachment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the attachment with ID {AttachmentId}.", attachment.AttachmentId);
                return null;
            }
        }
    }
}
