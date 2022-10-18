using Library.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infra.Repository;

public abstract class RepositoryBase<TContext, T> : IRepositoryBase<T> where T : class where TContext : DbContext
{
    protected TContext _context;
    public RepositoryBase(TContext context) => _context = context;

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate) =>
        await _context.Set<T>().Where(predicate).ToListAsync();
    
    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _context.Set<T>().ToListAsync();

    public async Task<T> GetByIdAsync(Guid id) =>
         await _context.Set<T>().FindAsync(id);

    public async Task<T> FindByAsync(Expression<Func<T, bool>> predicate) =>
         await _context.Set<T>().FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<T>> GetAllWithIncludes(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }

    public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
                _context.Set<T>()
                .AsNoTracking() :
                _context.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
    bool trackChanges) =>
        !trackChanges ?
            _context.Set<T>()
            .Where(expression)
            .AsNoTracking() :
            _context.Set<T>()
            .Where(expression);

    public async Task CreateAsync(T entity) => await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
}
