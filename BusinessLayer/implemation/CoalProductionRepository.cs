using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class CoalProductionRepository : ICoalProduction
    {
        private readonly ApplicationDbContext _context;

        public CoalProductionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Coal Productions using SP
        public async Task<List<CoalProduction>> GetCoalProductions()
        {
            List<CoalProduction> coalProductions = await _context.CoalProductions
                .FromSqlRaw("EXEC sp_GetAllCoalProductions")
                .ToListAsync();
            if (coalProductions != null)
            {
                foreach (var coalProduction in coalProductions)
                {
                    await _context.Entry(coalProduction)
                        .Reference(c => c.CoalMine)
                        .LoadAsync();  // Explicitly load the CoalMine reference
                }
            }
            return coalProductions;

        }

        // Get Coal Production By ID using SP
        public async Task<CoalProduction?> GetCoalProduction(int productionId)
        {
            var result = await _context.CoalProductions
                .FromSqlRaw("EXEC sp_GetCoalProductionByID @p0", productionId)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        // Insert Coal Production using SP
        public async Task Insert(CoalProduction coalProduction)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_InsertCoalProduction @p0, @p1, @p2",
                coalProduction.MineID, coalProduction.ProductionYear, coalProduction.TotalProductionInTons
            );
            }catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            
        }

        // Update Coal Production using SP
        public async Task Update(CoalProduction updatedProduction)
        {
          await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateCoalProduction @p0, @p1, @p2, @p3",
                updatedProduction.ProductionID,
                updatedProduction.MineID,
                updatedProduction.ProductionYear,
                updatedProduction.TotalProductionInTons
            );

           
        }

        // Delete Coal Production using SP
        public async Task Delete(int productionId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeleteCoalProduction @p0", productionId
            );

        }
    }
}
