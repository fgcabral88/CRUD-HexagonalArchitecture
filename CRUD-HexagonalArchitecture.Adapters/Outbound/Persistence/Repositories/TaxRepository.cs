using CRUD_HexagonalArchitecture.Application.Ports.Out;
using Microsoft.EntityFrameworkCore;
using CRUD_HexagonalArchitecture.Infrastructure.Data;
using CRUD_HexagonalArchitecture.Domain.Entities;

public class TaxRepository : ITaxRepository
{
    private readonly ApplicationDbContext _context;

    public TaxRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaxEntity> CreateAsync(TaxEntity tax)
    {
        _context.Taxes.Add(tax);

        await _context.SaveChangesAsync();

        return tax;
    }

    public async Task<TaxEntity?> GetByIdAsync(int id) => await _context.Taxes.FindAsync(id);

    public async Task<IEnumerable<TaxEntity>> GetAllAsync() => await _context.Taxes.ToListAsync();

    public async Task<TaxEntity> UpdateAsync(TaxEntity tax)
    {
        _context.Taxes.Update(tax);

        await _context.SaveChangesAsync();

        return tax;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Taxes.FindAsync(id);

        if (entity == null) 
            return false;

        _context.Taxes.Remove(entity);

        await _context.SaveChangesAsync();

        return true;
    }
}