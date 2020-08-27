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
using System.Security.Cryptography;

namespace SPA.BUS.Service
{
    public class TherapistService : AuthorizedService, ITherapistService
    {
        public IRepositoryHelper _repositoryHelper { get; }
        public IMapper _mapper { get; }

        public TherapistService(IRepositoryHelper repositoryHelper, IMapper mapper)
        {
            this._mapper = mapper ;
            this._repositoryHelper = repositoryHelper ;
        }

        public async Task<LogicResult<TherapistProfile>> GetTherapistById(int id)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            var result = await repo.GetByIdAsync(id);

            return new LogicResult<TherapistProfile>() { IsSuccess = true, Result = _mapper.Map<TherapistProfile>(result) };
        }

        public async Task<LogicResult<TherapistDetail>> GetTherapistbyUsernamePassword(string username, string password)
        {
            if (username == "")
                return new LogicResult<TherapistDetail>() { IsSuccess = false, message = Validation.InvalidParameters };
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);
            //string hashpass = CreateMD5(password);
            string hashpass = password;
            var result = await repo.GetFirstAsync(x => (x.Phone == username || x.Email == username) && x.passWord == hashpass);

            if (result == null)
            {
                return new LogicResult<TherapistDetail>() { IsSuccess = false, message = Validation.CustomerIsNotInOutlet };
            }
            var Res = _mapper.Map<TherapistDetail>(result);
            return new LogicResult<TherapistDetail>() { IsSuccess = true, Result = Res };
        }


        public async Task<LogicResult<IEnumerable<TherapistProfile>>> GetTherapistByName(string name)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            var therapistByName = await repo.GetAsync(x => x.Name.ToLower().Contains(name.ToLower()));

            var therapists = therapistByName.Where(x => x.Possition == (int)Position.therapist);

            var therapistsMap = _mapper.Map<IEnumerable<TherapistProfile>>(therapists);

            return new LogicResult<IEnumerable<TherapistProfile>>() { IsSuccess = true, Result = therapistsMap };
        }

        public async Task<LogicResult<IEnumerable<TherapistProfile>>> GetTherapistByOutlet(int outletID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            var therapists = await repo.GetAsync(x => x.Possition == (int)Position.therapist && x.Outlet_Staff.Any(y => y.Outlet == outletID));

            var therapistsProfile = _mapper.Map<IEnumerable<TherapistProfile>>(therapists);

            return new LogicResult<IEnumerable<TherapistProfile>>() { IsSuccess = true, Result = therapistsProfile };
        }

        public async Task<LogicResult<IEnumerable<TherapistProfile>>> GetTherapistByOutletAndTherapistName(int outletID, string TherapistName)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            var therapistsByOutlet = await GetTherapistByOutlet(outletID);
            var therapistsByName = therapistsByOutlet.Result.Where(x => x.Name.ToLower().Contains(TherapistName.ToLower()));

            return new LogicResult<IEnumerable<TherapistProfile>>() { IsSuccess = true, Result = therapistsByName };
        }

        public async Task<LogicResult<IEnumerable<TherapistProfile>>> GetTherapistByService(int serviceID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            var therapists = await repo.GetAsync(x => x.Possition == (int)Position.therapist);

            var therapistBySerVice = therapists.Where(x => x.Staff_Services.Any(y => y.Service == serviceID));

            return new LogicResult<IEnumerable<TherapistProfile>>()
            {
                IsSuccess = true,
                Result = _mapper.Map<IEnumerable<TherapistProfile>>(therapistBySerVice)
            };
        }

        public async Task<LogicResult> PostTherapist(TherapistDetail therapist)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            try
            {
                //var md5Pass = CreateMD5(therapist.passWord);

                var staff = new Staff();

                //{
                //    passWord=md5Pass,
                //    Gender=therapist.Gender,
                //    Phone=therapist.Phone,
                //    Email=therapist.Email,
                //    userName=therapist.userName,
                //    Name=therapist.Name,
                //    DoB=therapist.DoB,
                //    Possition=therapist.Possition,
                //};
                //
                //therapist.passWord = md5Pass;
                staff = _mapper.Map<Staff>(therapist);

                repo.Create(staff);

                var result = await unitofwork.SaveChangesAsync();
                if (!result)
                {
                    return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
                }

                return new LogicResult() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }
        }

        public async Task<LogicResult> PutTherapist(TherapistDetail therapsit)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);
            try
            {
                var _therapsit = _mapper.Map<Staff>(therapsit);

                var fieldsToUpdate = new string[]
                {
               
                    nameof(TherapistDetail.Name),
                    nameof(TherapistDetail.Phone),
                    nameof(TherapistDetail.passWord),
                    nameof(TherapistDetail.Gender),
                    nameof(TherapistDetail.DoB),
                    nameof(TherapistDetail.Decription),
                    nameof(TherapistDetail.Avatar),
                    nameof(TherapistDetail.Experience),
                    nameof(TherapistDetail.userName)
                };

                repo.Update(_therapsit, fieldsToUpdate);
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

        public async Task<LogicResult<IEnumerable<TherapistListDTO>>> GetTherapistByOutletAndService(int outletID, int ServiceID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            var therapists = await repo.GetAsync(x => x.Possition == (int)Position.therapist);
            var therapistBySerViceandOutlet = therapists.Where(x => x.Staff_Services.Any(y => y.Service == ServiceID && x.Outlet_Staff.Any(z => z.Outlet == outletID)));

            return new LogicResult<IEnumerable<TherapistListDTO>>()
            {
                IsSuccess = true,
                Result = _mapper.Map<IEnumerable<TherapistListDTO>>(therapistBySerViceandOutlet)
            };

        }

        public async Task<int> CheckTherapistExisByEmailOrPhone(string info)
        {
            if (info == "" || info == "*")
                return 0;
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

                var result = await repo.GetAsync(x => x.Email == info || x.Phone == info);

                if (result == null || !result.Any())
                {
                    return 0;
                }
                return 1;
            }
        }
    }

}
