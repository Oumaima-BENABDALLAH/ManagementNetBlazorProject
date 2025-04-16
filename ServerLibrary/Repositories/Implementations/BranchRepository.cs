using BasicLibrary.Entites;
using BasicLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class BranchRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Branch>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {

            var dep = await appDbContext.Branch.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Branch.Remove(dep);
            await Commit();
            return Success();


        }

        public async Task<List<Branch>> GetAll() => await appDbContext.Branch.ToListAsync();


        public async Task<Branch> GetById(int id) => await appDbContext.Branch.FindAsync(id);


        public async Task<GeneralResponse> Insert(Branch item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Branch already added");
            appDbContext.Branch.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var dep = await appDbContext.Branch.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();



        }
        private static GeneralResponse NotFound() => new(false, "Sorry department not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Branch.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

        public Task<GeneralResponse> Insert(Branch item, string baseUrl)
        {
            throw new NotImplementedException();
        }
    }
}
