using FoodWriting.Domain.Interfaces;

namespace FoodWriting.Infrastructure.Repository;

public class UnityOfWork : IDisposable, IUnityOfWork
{
    private readonly DataContext _context;
    private bool _disposed;
    
    public UnityOfWork(DataContext context)
    {
        _context = context;
    }
    
    public void Dispose()
    {
        Dispose(true);
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }
}