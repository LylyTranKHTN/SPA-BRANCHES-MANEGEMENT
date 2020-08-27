using SPA.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.UnitOfWork
{
    public partial class RepositoryHelper : IRepositoryHelper
    {
        public IUnitOfWork GetUnitOfWork()
        {
            var unitOfWork = new UnitOfWork();
            return unitOfWork;
        }

        public TRepository GetRepository<TRepository>(IUnitOfWork unitOfWork)
            where TRepository : class
        {
            if (typeof(TRepository) == typeof(IBedRepository))
            {
                dynamic repo = new BedRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }
            if (typeof(TRepository) == typeof(IRoomRepository))
            {
                dynamic repo = new RoomRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }
            if (typeof(TRepository) == typeof(IOutletRepository))
            {
                dynamic repo = new OutletRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }
            if (typeof(TRepository) == typeof(ICustomerRepository))
            {
                dynamic repo = new CustomerRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }
            if (typeof(TRepository) == typeof(ITherapistRepository))
            {
                dynamic repo = new TherapistRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if (typeof(TRepository) == typeof(IServiceRepository))
            {
                dynamic repo = new ServiceRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if (typeof(TRepository) == typeof(IService_BedRepository))
            {
                dynamic repo = new Service_BedRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }
            if (typeof(TRepository) == typeof(IAppoitmentDetailRepository))
            {
                dynamic repo = new AppoitmentDetailRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }
            if (typeof(TRepository) == typeof(IReviewOutletRepository))
            {
                dynamic repo = new ReviewOutletRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if (typeof(TRepository) == typeof(ITimeSlotRepository))
            {
                dynamic repo = new TimeSlotRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if (typeof(TRepository) == typeof(IReviewOutletRepository))
            {
                dynamic repo = new ReviewOutletRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if (typeof(TRepository) == typeof(IBufferTimeRepository))
            {
                dynamic repo = new BufferTimeRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if (typeof(TRepository) == typeof(IAppoitmentRepository))
            {
                dynamic repo = new AppoitmentRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if(typeof(TRepository)==typeof(IOutlet_StaffRepository))
            {
                dynamic repo = new Outlet_StaffRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if(typeof(TRepository)==typeof(IMeasurementRepository))
            {
                dynamic repo = new MeasurementRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if (typeof(TRepository) == typeof(IServiceTypeRepository))
            {
                dynamic repo = new ServiceTypeRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            if (typeof(TRepository) == typeof(IReviewServiceRepository))
            {
                dynamic repo = new ReviewServiceRepository();
                repo.UnitOfWork = unitOfWork;
                return (TRepository)repo;
            }

            TRepository repository = null;
            return repository;
        }
    }
}
