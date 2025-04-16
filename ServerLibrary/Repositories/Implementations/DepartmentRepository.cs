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
    public class DepartmentRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Department>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {

            var dep = await appDbContext.Department.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Department.Remove(dep);
            await Commit();
            return Success();


        }

        public async Task<List<Department>> GetAll() => await appDbContext.Department.ToListAsync();


        public async Task<Department> GetById(int id) => await appDbContext.Department.FindAsync(id);


        public async Task<GeneralResponse> Insert(Department item)
        {
            var checkIfNull = await CheckName(item.Name);
            if (!checkIfNull)
                return new GeneralResponse(false, "Department already added");
            appDbContext.Department.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department item)
        {
            var dep = await appDbContext.Department.FindAsync(item.Id);
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
            var item = await appDbContext.Department.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

        public Task<GeneralResponse> Insert(Department item, string baseUrl)
        {
            throw new NotImplementedException();
        }
    }
}
