using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Interfaces;
using WebShop.Instrastructure.Data;

namespace WebShop.Instrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly WebShopDbContext _context;
	protected readonly DbSet<T> _dbSet;

	public Repository(WebShopDbContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public async Task Delete(int id)
	{
		var entity = await _dbSet.FindAsync(id);
		if (entity != null)
		{
			_dbSet.Remove(entity);
		}
	}

	public virtual async Task<IEnumerable<T>>? GetAll()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task Add(T entity)
	{
		await _dbSet.AddAsync(entity);
	}

	public async Task<T>? GetById(int id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task Update(T entity)
	{
		_dbSet.Update(entity);
	}
}