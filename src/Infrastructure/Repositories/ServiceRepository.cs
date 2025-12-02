using Application.Interfaces;
using Infrastructure.Data;
using ManoaAmigas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _db;

    public ServiceRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(Service service)
    {
        service.service_status = (char)Domain.Enums.ServiceStatus.Creado;
        service.request_date = DateTime.UtcNow; 

        _db.service.Add(service);
        await _db.SaveChangesAsync();
    }

    public async Task<Service?> GetByIdAsync(long serviceId)
    {
        return await _db.service.FirstOrDefaultAsync(s => s.service_id == serviceId);
    }

    public async Task UpdateAsync(Service service)
    {
        _db.service.Update(service);
        await _db.SaveChangesAsync();
    }
}