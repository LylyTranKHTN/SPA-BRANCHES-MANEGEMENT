
using API.Model.Model;
using SPA.Domain.Models;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.BUS.Service
{
    public interface IRoomService
    {
        IRepositoryHelper _repositoryHelper { get; }

        /// <summary>
        /// Get room profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List Room</returns>
        Task<LogicResult<RoomDetail>> GetRoomById(int id);

        /// <summary>
        /// Create and Save a new room
        /// </summary>
        /// <param name="room"></param>
        void PostRoom(Room room);

        /// <summary>
        /// Edit room profile
        /// </summary>
        /// <param name="room"></param>
        void PutRoom(RoomDetail room);
    }
}
