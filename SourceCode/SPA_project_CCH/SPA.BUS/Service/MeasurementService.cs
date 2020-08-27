using API.Model;
using API.Model.Model;
using AutoMapper;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.BUS.Service
{
    public class MeasurementService: IMeasurementService
    {
        public IRepositoryHelper _repositoryHelper { get; }
        public IMapper _mapper { get; }

        public MeasurementService(IRepositoryHelper repositoryHelper, IMapper mapper)
        {
            _repositoryHelper = repositoryHelper;
            _mapper = mapper;
        }

        public async Task<LogicResult<MeasurementDto>> GetMesurementNewest(int customerID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var measurementRepo = _repositoryHelper.GetRepository<IMeasurementRepository>(unitofwork);

            var mesurement = await measurementRepo.GetFirstAsync(x=>x.AppointmentDetail.Appointment1.Customer==customerID, x => x.OrderByDescending(z => z.AppointmentDetail.Date), "AppointmentDetail");
            if (mesurement == null)
                return new LogicResult<MeasurementDto>() { IsSuccess = false, message = Validation.CustomerHaveNoAppointment };

            return new LogicResult<MeasurementDto>() { IsSuccess = true, Result = _mapper.Map<MeasurementDto>(mesurement) };
        }

        public async Task<LogicResult<IEnumerable<MeasurementDto>>> GetMeasurementHistory(int customerID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var measurementRepo = _repositoryHelper.GetRepository<IMeasurementRepository>(unitofwork);

            var mesurements = await measurementRepo.GetAsync(x => x.AppointmentDetail.Appointment1.Customer == customerID,x => x.OrderByDescending(z => z.AppointmentDetail.Date), "AppointmentDetail");
            if (!mesurements.Any())
                return new LogicResult<IEnumerable<MeasurementDto>>() { IsSuccess = false, message = Validation.CustomerHaveNoAppointment };

            return new LogicResult<IEnumerable<MeasurementDto>>() { IsSuccess = true, Result = _mapper.Map<IEnumerable<MeasurementDto>>(mesurements) };
        }
    }
}
