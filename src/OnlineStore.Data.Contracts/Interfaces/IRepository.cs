﻿namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class { }
}
