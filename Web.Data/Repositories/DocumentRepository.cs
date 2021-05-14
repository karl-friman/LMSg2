using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Data;


namespace Web.Data.Repositories
{
    class DocumentRepository : IAsyncRepository<Document>
    {
        private readonly ApplicationDbContext db;
        public DocumentRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Document>> GetAllWithCourseAndModule(bool includeAll)
        {
            if (includeAll)
            {
                List<Document> documentList = await db.Documents
                                              .Include(lms => lms.LMSUser)
                                              .OrderBy(x => x.Name)
                                              .ToListAsync();
                return documentList;

            }
            else
            {
                List<Document> documentList = await db.Documents.ToListAsync();
                return documentList;
            }
        }
        public async Task<Document> GetOne(int? id, bool includeAll)
        {
            if (includeAll)
            {
                return await db.Documents
                            .Include(lms => lms.LMSUser)
                            .OrderBy(x => x.Name)
                            .FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                return await db.Documents
                            .FirstOrDefaultAsync(m => m.Id == id);
            }
        }
        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
        public async Task AddAsync(Document document)
        {
            await db.AddAsync(document);
        }
        public void Update(Document document)
        {
            db.Update(document);
        }
        public void Remove(Document document)
        {
            db.Remove(document);
        }
        public bool Any(int? Id)
        {
            return db.Documents.Any(m => m.Id == Id);
        }

        public Task<Document> GetOne(string Id, bool includeAll)
        {
            throw new NotImplementedException();
        }

        public bool Any(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LMSUser>> GetAllWithCourseAndModule()
        {
            throw new NotImplementedException();
        }
    }
}
