using API.Model;
using API.Model.Enum;
using API.Model.Model;
using AutoMapper;

using SPA.Domain.Models;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace SPA.BUS.Service
{
    public class OutletService : IOutletService
    {
        public IRepositoryHelper _repositoryHelper { get; }
        public IMapper _mapper { get; }

        public OutletService(IRepositoryHelper repositoryHelper, IMapper mapper)
        {
            this._mapper = mapper;
            this._repositoryHelper = repositoryHelper ;
        }

        public async Task<LogicResult<OutletDetail>> GetOutletById(int id)
        {
            if (id <= 0)
                return new LogicResult<OutletDetail>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);

                var outlet = await repo.GetByIdAsync(id);
                var result = _mapper.Map<OutletDetail>(outlet);
                return new LogicResult<OutletDetail>() { IsSuccess = true, Result = result };
            }
        }

        public async Task<LogicResult<IEnumerable<ReviewOutletDto>>> GetOutletReview(int id)
        {
            if (id <= 0)
                return new LogicResult<IEnumerable<ReviewOutletDto>>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<IReviewOutletRepository>(unitofwork);
                
                var review = await repo.GetAsync(x => x.Outlet == id, x=> x.OrderByDescending(y=>y.Date));

                var result = _mapper.Map <IEnumerable<ReviewOutletDto>>(review);
                return new LogicResult<IEnumerable<ReviewOutletDto>>() { IsSuccess = true, Result = result };
            }
        }

        public async Task<LogicResult<IEnumerable<OutletDetail>>> GetOutletByName(string name)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);
            if(name == null)
            {
                var outlets = await repo.GetAllAsync();
                var outletsmap = _mapper.Map<IEnumerable<OutletDetail>>(outlets);
                return new LogicResult<IEnumerable<OutletDetail>>() { IsSuccess = true, Result = outletsmap };
            }

            var result = await repo.GetAsync(x => x.Name.ToLower().Contains(name.ToLower()) );

            var outletsMap = _mapper.Map<IEnumerable<OutletDetail>>(result);

            return new LogicResult<IEnumerable<OutletDetail>>() { IsSuccess = true, Result = outletsMap };
        }

        public async Task<LogicResult<IEnumerable<OutletDetailCA>>> GetOutletByNameCA(string outletName)
        {
            try
            {
                // Store
                var store = await GetAllOutletCA();
                if (outletName == null)
                {
                    return new LogicResult<IEnumerable<OutletDetailCA>>()
                    {
                        IsSuccess = true,
                        Result = store.Result,
                        message = "Null Name"
                    };
                }

                // First Filter: name (outlet name) -- also only filter

                return new LogicResult<IEnumerable<OutletDetailCA>>()
                {
                    IsSuccess = true,
                    Result = store.Result.Where(x => x.Name.ToLower().Contains(outletName.ToLower())),
                    message = "Success"
                };
                
            }catch
            {
                return new LogicResult<IEnumerable<OutletDetailCA>>()
                {
                    IsSuccess = false,
                    Result=null,
                    message=Validation.ServerError
                };
            }
        }

        public async Task<LogicResult<IEnumerable<OutletDetail>>> GetAllOutlet()
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);
           
            var outlets = await repo.GetAllAsync();
            var outletsmap = _mapper.Map<IEnumerable<OutletDetail>>(outlets);
            return new LogicResult<IEnumerable<OutletDetail>>() { IsSuccess = true, Result = outletsmap };
        }

        public async Task<LogicResult<IEnumerable<OutletDetailCA>>> GetAllOutletCA()
        {
            try
            {
                // Get Data
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo_Outlet = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);

                // Store
                List<OutletDetailCA> store = new List<OutletDetailCA>();

                // Get Target Data
                var outletList = await repo_Outlet.GetAllAsync();
                foreach (var outlet in outletList)
                {
                    // Get Count Of Review (Contain not Null)
                    int reviewCount = 0;
                    var outlet_review_content = outlet.ReviewOutlets.Where(x => x.Content.Length > 1);
                    reviewCount = outlet_review_content.Count();

                    // Get Average Of Star
                    float starAverage = 0;
                    var outlet_reviews = outlet.ReviewOutlets;
                    foreach (var outlet_review in outlet_reviews)
                    {
                        starAverage += (float)outlet_review.Star;
                    }
                    if (outlet_reviews.Count() == 0)
                        starAverage = 5;
                    else
                        starAverage /= outlet_reviews.Count();

                    // Mapped
                    store.Add(new OutletDetailCA()
                    {
                        ID = outlet.ID,
                        address = outlet.address,
                        Image = outlet.Image,
                        Name = outlet.Name,
                        Phone = outlet.Phone,
                        Star = starAverage,
                        Review = reviewCount
                    });
                }

                return new LogicResult<IEnumerable<OutletDetailCA>>()
                {
                    IsSuccess = true,
                    Result = store
                };
            }
            catch
            {
                return new LogicResult<IEnumerable<OutletDetailCA>>()
                {
                    IsSuccess = false,
                    Result = null,
                    message = Validation.ServerError
                };
            }


        }

        public LogicResult CreateReviewOutlet(int outletId, int customerid, int star, string content)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IReviewOutletRepository>(unitofwork);
            var customerRepo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);
            var outletRepo = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);


            var customercheck = customerRepo.GetExists(x => x.ID == customerid);
            if (!customercheck)
                return new LogicResult() { IsSuccess = false, message = Validation.CustomerIsNotInOutlet };

            var outletcheck = outletRepo.GetExists(x => x.ID == outletId);
            if (!outletcheck)
                return new LogicResult() { IsSuccess = false, message = Validation.OutletIsNotExists};

            var review = new ReviewOutlet()
            {
                Star = star,
                Content = content,
                Outlet = outletId,
                Customer = customerid,
                Date=System.DateTime.Now
            };

            repo.Create(review);
            unitofwork.SaveChanges();

            return new LogicResult() { IsSuccess = true };
        }


        //public async  Task<LogicResult> PutOutlet(OutletDetail customer)
        //{
        //    var unitofwork = _repositoryHelper.GetUnitOfWork();
        //    var repo = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);

        //    var _customer = _mapper.Map<Outlet>(customer);
        //    try
        //    {
        //        repo.Update(_customer);
        //        await unitofwork.SaveChangesAsync();

        //        return new LogicResult() { IsSuccess = true };
        //    }
        //    catch
        //    {
        //        return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
        //    }
        //}
        public async Task<LogicResult> PutOutlet(OutletDetail customer)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);
            try
            {
                var _customer = _mapper.Map<Outlet>(customer);

                var fieldsToUpdate = new string[]
                {
                    nameof(OutletDetail.ID),
                    nameof(OutletDetail.Name),
                    nameof(OutletDetail.Phone),
                    nameof(OutletDetail.Image),
                    nameof(OutletDetail.Start),
                    nameof(OutletDetail.address)
                    
                };

                repo.Update(_customer, fieldsToUpdate);
                var result = await unitofwork.SaveChangesAsync();

                if (!result)
                {
                    return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
                }
                return new LogicResult() { IsSuccess = true };
            }
            catch
            {
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }
        }

        public async Task<LogicResult> PostOutlet(Outlet outlet)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);

            try
            {
                repo.Create(outlet);

                await unitofwork.SaveChangesAsync();
                return new LogicResult() { IsSuccess = true };
            }
            catch
            {
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }
        }

        public async Task<LogicResult<BedAndTherapistNum>> GetNumOfBedAndTherapistByOutlet(int outletID)
        {
            try
            {
                // Get Data
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo_outlet_staff = _repositoryHelper.GetRepository<IOutlet_StaffRepository>(unitofwork);
                var repo_room = _repositoryHelper.GetRepository<IRoomRepository>(unitofwork);
                
                // Get Target Data
                var listOutletStaff = await repo_outlet_staff.GetAsync(x => x.Outlet.Equals(outletID));
                var listRoom = await repo_room.GetAsync(x => x.Outlet==outletID);
                
                // Get Num Therapist
                int numTherapist = 0;
                foreach (var outlet_staff in listOutletStaff)
                {
                    if (outlet_staff.Staff1.Possition.Equals((int)Position.therapist))
                    {
                        numTherapist++;
                    }
                }

                // Get Num Bed
                int numBed = 0;
                foreach (var room in listRoom)
                {
                    numBed += (int)room.numOfBed;
                }

                return new LogicResult<BedAndTherapistNum>()
                {
                    IsSuccess = true,
                    Result = new BedAndTherapistNum(numBed, numTherapist)
                };
            }
            catch
            {
                return new LogicResult<BedAndTherapistNum>()
                {
                    IsSuccess = false,
                    Result = null,
                    message = Validation.ServerError
                };
            }

        }

    }
}
