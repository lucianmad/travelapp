﻿namespace TravelApp.DataAccess.Repositories.Abstractions;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(int id, T entity);
    Task DeleteAsync(int id);
}