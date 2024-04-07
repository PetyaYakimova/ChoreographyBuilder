namespace ChoreographyBuilder.Infrastructure.Data.Common
{
    public interface IRepository
    {
        /// <summary>
        /// Gets all entities from the DB. Changes on them will affect the records in the DB if they are saved.
        /// </summary>
        /// <typeparam name="T">DB Enity type</typeparam>
        /// <returns></returns>
        IQueryable<T> All<T>() where T : class;

		/// <summary>
		/// Gets all entities from the DB as readonly. No changes on them will affect the records in the DB.
		/// </summary>
		/// <typeparam name="T">DB Enity type</typeparam>
		/// <returns></returns>
		IQueryable<T> AllAsReadOnly<T>() where T : class;

		/// <summary>
		/// Adds a new record.
		/// </summary>
		/// <typeparam name="T">DB Enity type</typeparam>
		/// <returns></returns>
		Task AddAsync<T>(T entity) where T : class;

		/// <summary>
		/// Saves all the changes in the DB.
		/// </summary>
		/// <returns></returns>
        Task<int> SaveChangesAsync();

		/// <summary>
		/// Gets a record by id if it exists. If not - returns null.
		/// </summary>
		/// <typeparam name="T">DB Enity type</typeparam>
		/// <param name="id">Record id</param>
		/// <returns></returns>
		Task<T?> GetByIdAsync<T>(object id) where T : class;

		/// <summary>
		/// Deletes a record by its id if it exists.
		/// </summary>
		/// <typeparam name="T">DB Enity type</typeparam>
		/// <param name="id">Record id</param>
		/// <returns></returns>
		Task DeleteAsync<T>(object id) where T : class;
    }
}
