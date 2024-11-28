using WebShop.Core.Interfaces;

namespace WebShop.Application.Services;

public class GenericService<T> : IGenericService<T> where T : class
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IRepository<T> _repository;

	public GenericService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_repository = unitOfWork.Repository<T>();
	}

	public virtual async Task<IEnumerable<T>> GetAll()
	{
		return await _repository.GetAll();
	}

	public async Task<T>? GetById(int id)
	{
		return await _repository.GetById(id);
	}

	public async Task Add(T entity)
	{
		await _repository.Add(entity);
		await _unitOfWork.Complete();
	}

	public async Task Update(T entity)
	{
		await _repository.Update(entity);
		await _unitOfWork.Complete();
	}

	public async Task Delete(int id)
	{
		await _repository.Delete(id);
		await _unitOfWork.Complete();
	}
}