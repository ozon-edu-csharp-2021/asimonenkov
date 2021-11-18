using System.Threading;
using System.Threading.Tasks;

namespace Route256.MerchandiseService.Domain.Contracts
{
    /// <summary>
    /// Базовый интерфейс репозитория
    /// </summary>
    /// <typeparam name="TAggregationRoot">Объект сущности для управления</typeparam>
    public interface IRepository<TAggregationRoot>
    {

        /// <summary>
        /// Создать новую сущность
        /// </summary>
        /// <param name="itemToCreate">Объект для создания</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Созданная сущность</returns>
        Task<TAggregationRoot> CreateAsync(TAggregationRoot itemToCreate, CancellationToken cancellationToken = default);
    }
}