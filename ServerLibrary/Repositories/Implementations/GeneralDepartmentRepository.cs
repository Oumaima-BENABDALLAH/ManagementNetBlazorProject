using BasicLibrary.Entites;
using BasicLibrary.Responses;
using Microsoft.AspNetCore.Http.Timeouts;
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
    public class GeneralDepartmentRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<GeneralDepartment>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.GeneralDepartment.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.GeneralDepartment.Remove(dep);
            await Commit();
            return Success();


        }

        public async Task<List<GeneralDepartment>> GetAll() => await appDbContext.GeneralDepartment.ToListAsync();


        public async Task<GeneralDepartment> GetById(int id) => await appDbContext.GeneralDepartment.FindAsync(id);
      

        public async Task<GeneralResponse> Insert(GeneralDepartment item)
        {
            var checkIfNull = await CheckName(item.Name);
            if(!checkIfNull)
                return new GeneralResponse(false, "General department already added");
            appDbContext.GeneralDepartment.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(GeneralDepartment item)
        {
            var dep = await appDbContext.GeneralDepartment.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();



        }

        private static GeneralResponse NotFound() => new(false, "Sorry department not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool>  CheckName(string name)
        {
            var item = await appDbContext.GeneralDepartment.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null ;
        }

        public Task<GeneralResponse> Insert(GeneralDepartment item, string baseUrl)
        {
            throw new NotImplementedException();
        }
    }
}
