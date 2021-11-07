using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel
{
    public sealed class Position : Enumeration
    {
        public static Position InternProgrammer = new(1, nameof(InternProgrammer));
        public static Position MiddleProgrammer = new(2, nameof(MiddleProgrammer));
        public static Position JuniorProgrammer = new(3, nameof(JuniorProgrammer));
        public static Position SeniorProgrammer = new(4, nameof(SeniorProgrammer));
        public static Position WarehouseWorker = new(5, nameof(WarehouseWorker));
        public static Position PickupPointWorker = new(6, nameof(PickupPointWorker));
        public Position(int id, string name) : base(id, name)
        {
        }
    }
}