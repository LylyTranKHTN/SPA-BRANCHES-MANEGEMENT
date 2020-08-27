using AutoMapper;

using SPA.Domain.Models;
using API.Model;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model.Model;
using API.Model.Enum;
using CustomerType = API.Model.Enum.CustomerType;


// <NEW>
namespace SPA.BUS.Service
{
    public class ServiceService:IServiceService
    {
        public IRepositoryHelper _repositoryHelper { get; }
        public IMapper _mapper { get; }

        public ServiceService(IRepositoryHelper repositoryHelper, IMapper mapper)
        {
            this._mapper = mapper;
            this._repositoryHelper = repositoryHelper;
        }

        public async Task<LogicResult<IEnumerable<ServiceDetail>>> GetServiceByName(string name)
        {
            if (name == "")
            {
                return null;
            }

            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);

            var result = await repo.GetAsync(x => x.Name.Contains(name));

            var services = new List<Domain.Models.Service>();

            foreach (var sv in result)
            {

                services.Add(sv);
                
            }

            var serviceMap = _mapper.Map<IEnumerable<ServiceDetail>>(services);

            return new LogicResult<IEnumerable<ServiceDetail>>() { IsSuccess = true, Result = serviceMap };
        }

        public async Task<LogicResult<IEnumerable<ServiceDetail>>> GetServiceByOutlet(int outletID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo_room = _repositoryHelper.GetRepository<IRoomRepository>(unitofwork);
            var repo_bed = _repositoryHelper.GetRepository<IBedRepository>(unitofwork);
            var repo_ser_bed = _repositoryHelper.GetRepository<IService_BedRepository>(unitofwork);
            var repo_ser = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
    
            var room_result = await repo_room.GetAsync(x => x.Outlet==outletID, null, null, null);

            var beds = new List<Domain.Models.Bed>();
            foreach(var room in room_result)
            {
                var bed = await repo_bed.GetAsync(x => x.Room==room.ID);
                foreach(var b in bed)
                {
                    beds.Add(b);
                }
            }

            var ser_beds = new List<Domain.Models.Service_Bed>();
            foreach(var bed in beds)
            {
                var temp = bed.ID;
                var ser_bed = await repo_ser_bed.GetAsync(x => x.Bed==bed.ID);
                foreach(var sb in ser_bed)
                {
                    ser_beds.Add(sb);
                }
            }

            var services = new List<Domain.Models.Service>();
            foreach (var s_b in ser_beds)
            {
                var service = await repo_ser.GetAsync(x => x.ID==s_b.Service);
                foreach(var sv in service)
                {
                    services.Add(sv);
                }

            }

            var servicesMap = _mapper.Map<IEnumerable<ServiceDetail>>(services);

            return new LogicResult<IEnumerable<ServiceDetail>>() { IsSuccess = true, Result = servicesMap };
        }

        public async Task<LogicResult<IEnumerable<ServiceTypeDto>>> GetAllServiceType()
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IServiceTypeRepository>(unitofwork);

            var servicetype = await repo.GetAllAsync();
            var typesmap = _mapper.Map<IEnumerable<ServiceTypeDto>>(servicetype);
            return new LogicResult<IEnumerable<ServiceTypeDto>>() { IsSuccess = true, Result = typesmap };
        }

        public async Task<LogicResult<IEnumerable<ReviewServiceDto>>> GetServiceReview(int id)
        {
            if (id <= 0)
                return new LogicResult<IEnumerable<ReviewServiceDto>>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<IReviewServiceRepository>(unitofwork);

                var review = await repo.GetAsync(x => x.Service == id, x => x.OrderByDescending(y => y.Date));

                var result = _mapper.Map<IEnumerable<ReviewServiceDto>>(review);
                return new LogicResult<IEnumerable<ReviewServiceDto>>() { IsSuccess = true, Result = result };
            }
        }

        public async Task<LogicResult> CreateReviewService(int serviceId, int customerid, int star, string content)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IReviewServiceRepository>(unitofwork);
            var customerRepo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);
            var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);


            var customercheck = await customerRepo.GetExistsAsync(x => x.ID == customerid);
            if (!customercheck)
                return new LogicResult() { IsSuccess = false, message = Validation.CustomerIsNotInOutlet };

            var servicecheck = await serviceRepo.GetExistsAsync(x => x.ID == serviceId);
            if (!servicecheck)
                return new LogicResult() { IsSuccess = false, message = Validation.OutletIsNotExists };

            var review = new ReviewService()
            {
                Star = star,
                Content = content,
                Service = serviceId,
                Customer = customerid,
                Date = System.DateTime.Now
            };

            repo.Create(review);

            var dbResult = await unitofwork.SaveChangesAsync();

            if (!dbResult)
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };

            return new LogicResult() { IsSuccess = true };
        }

        public async Task<LogicResult<IEnumerable<ServiceDetail>>> GetServiceBy5Filter(_5FilterServiceList _5Filter)
        {
            int outletID = _5Filter.outletID;
            string serviceName = _5Filter.serivceName;

            // First Filter: OutletID
            var result1 = await this.GetServiceByOutlet(outletID);

            // Second Filter: serviceName
            var result2 = result1.Result.AsEnumerable();
            if (serviceName != null)
            {
                result2 = result1.Result.Where(x => x.Name.ToLower().Contains(serviceName.ToLower()));
            }

            // Third Filter: serviceType
            if (_5Filter.serviceType.HasValue)
                result2 = result2.Where(x => x.serviceType.Equals(_5Filter.serviceType.Value));

            // Fourth Filter: servicePrice
            if (_5Filter.servicePrice.HasValue)
            {
                switch (_5Filter.servicePrice.Value)
                {
                    case 1:
                        result2 = result2.Where(x => x.Price <= (double)Price.money1);
                        break;
                    case 2:
                        result2 = result2.Where(x => x.Price <= (double)Price.money2 &&
                            x.Price > (double)Price.money1);
                        break;
                    case 3:
                        result2 = result2.Where(x => x.Price <= (double)Price.money3 &&
                            x.Price > (double)Price.money2);
                        break;
                    case 4:
                        result2 = result2.Where(x => x.Price > (double)Price.money3);
                        break;
                }
            }

            // Fifth Filter: orderBy
            if (_5Filter.orderBy.HasValue)
            switch (_5Filter.orderBy.Value)
            {
                case (int)OrderBy.moneyDecrease:
                    result2.OrderByDescending(x => x.Price);
                    break;
                case (int)OrderBy.moneyIncrese:
                    result2.OrderBy(x => x.Price);
                    break;
            }

            return new LogicResult<IEnumerable<ServiceDetail>>()
            {
                IsSuccess = true,
                Result = result2
            };
        }

        public async Task<LogicResult<IEnumerable<BufferTimeDetail>>> getBufferTime(int therapist)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo_buff = _repositoryHelper.GetRepository<IBufferTimeRepository>(unitofwork);
            var repo_ser = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
            var therapistRepo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);
            var buffer_repo = _repositoryHelper.GetRepository<IBufferTimeRepository>(unitofwork);


            // check current therapist
            var currentTherapist = await therapistRepo.GetByIdAsync(therapist);
            if (currentTherapist == null)
                return new LogicResult<IEnumerable<BufferTimeDetail>>() { IsSuccess = false, message = Validation.TherepistIsNotInOutlet };

            // get services by therapist
            var services = await repo_ser.GetAsync(x => x.Staff_Services.Any(y => y.Staff == therapist));

            // get buffer time by service
            var bufferTimes = new List<BufferTimeDetail>();
            if (services.Any())
            {
                foreach (var service in services)
                {
                    foreach(CustomerType type in Enum.GetValues(typeof(CustomerType)))
                    {
                        var bufferTime = await buffer_repo.GetFirstAsync(x => x.Service == service.ID && x.customerType == (int)type);
                        if (bufferTime != null)
                        {
                            var buffer = new BufferTimeDetail
                            {
                                customerType =bufferTime.CustomerType1.Name,
                                ServiceName = service.Name,
                                bufferTime = bufferTime.bufferTime1
                            };
                            bufferTimes.Add(buffer);
                        }
                    }
                }
            };
            return new LogicResult<IEnumerable<BufferTimeDetail>>() { IsSuccess = true, Result = bufferTimes };
	}

        public async Task<LogicResult<ServiceDTO>> GetServiceByID(int ServiceID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
            var result = repo.GetById(ServiceID);
            var Result = new ServiceDTO()
            {
                ID = result.ID,
                Image = result.Image,
                Name = result.Name,
                Star = result.Star.GetValueOrDefault(),
                Price = result.Price,
                NumOfTimeSlot = result.numOfTimeSlot*15

            };
            return new LogicResult<ServiceDTO>() { IsSuccess = true, Result = Result };


        }

      
    }
    
}
