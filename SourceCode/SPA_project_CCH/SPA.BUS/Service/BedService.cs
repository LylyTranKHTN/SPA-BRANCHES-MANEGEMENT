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
using SPA.BUS.Service;

namespace SPA.BUS.Service
{
    public class BedService : IBedService
    {
        public IRepositoryHelper _repositoryHelper { get; }
        public IMapper _mapper { get; }

        public BedService(IRepositoryHelper repositoryHelper, IMapper mapper)
        {
            this._mapper = mapper ;
            this._repositoryHelper = repositoryHelper ;
        }

        public async Task<LogicResult<BedServiceDto>> GetBedById(int id)
        {
            if (id <= 0)
                return new LogicResult<BedServiceDto>() { IsSuccess = false, message = Validation.InvalidParameters, Result= null };
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<IBedRepository>(unitofwork);

                var bed = await repo.GetByIdAsync(id);
                var result = new BedServiceDto()
                {
                    ID = bed.ID,
                    Room = bed.Room.Value,
                    services = _mapper.Map<List<ServicesIdNameDto>>(bed.Service_Bed.Select(x => x.Service1))
                };

                return new LogicResult<BedServiceDto>() { IsSuccess = true, Result = result };
            }
        }

        public async Task<LogicResult<IEnumerable<ManageBedDto>>> GetBedByOutlet(int outletID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IBedRepository>(unitofwork);

            var beds = await repo.GetBedIncludeRoomByOutletID(outletID);

            var manageBed = _mapper.Map<IEnumerable<ManageBedDto>>(beds);

            return new LogicResult<IEnumerable<ManageBedDto>>() { IsSuccess = true, Result = manageBed };
        }


        public void PostBed(Bed bed)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IBedRepository>(unitofwork);

            repo.Create(bed);
            unitofwork.SaveChangesAsync();
        }

        public void PutBed(Bed bed)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IBedRepository>(unitofwork);

            repo.Update(bed);
            unitofwork.SaveChangesAsync();
        }
    }
}
