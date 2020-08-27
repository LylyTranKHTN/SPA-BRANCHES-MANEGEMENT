using AutoMapper;

using SPA.Domain.Models;
using API.Model.Model;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model;

namespace SPA.BUS.Service
{
    public class RoomService : IRoomService
    {
        public IRepositoryHelper _repositoryHelper { get; }
        public IMapper _mapper { get; }

        public RoomService(IRepositoryHelper repositoryHelper, IMapper mapper)
        {
            this._mapper = mapper ;
            this._repositoryHelper = repositoryHelper ;
        }

        public async Task<LogicResult<RoomDetail>> GetRoomById(int id)
        {
            if (id <= 0)
                return new LogicResult<RoomDetail>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null};
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<IRoomRepository>(unitofwork);

                var room = await repo.GetByIdAsync(id);
                var result = _mapper.Map<RoomDetail>(room);

                return new LogicResult<RoomDetail>() { IsSuccess = true, Result = result };
            }
        }

        public void PostRoom(Room room)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IRoomRepository>(unitofwork);

            repo.Create(room);
            unitofwork.SaveChangesAsync();
        }

        public void PutRoom(RoomDetail room)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IRoomRepository>(unitofwork);

            var _room = _mapper.Map<Room>(room);
            repo.Update(_room);
            unitofwork.SaveChangesAsync();
        }
    }
}
