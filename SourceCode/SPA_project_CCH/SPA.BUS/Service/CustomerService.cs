using API.Model;
using AutoMapper;
using API.Model.Model;
using SPA.BUS.Service;
using SPA.Domain.Models;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SPA.BUS.Service
{
    public class CustomerService: AuthorizedService, ICustomerService
    {
        public IRepositoryHelper _repositoryHelper { get; }
        public IMapper _mapper { get; }

        public CustomerService(IRepositoryHelper repositoryHelper,IMapper mapper)
        {
            this._mapper = mapper ;
            this._repositoryHelper = repositoryHelper ;
        }

        public async Task<LogicResult<CustomerDetail>> GetCustomerById(int id)
        {
            if (id <= 0)
                return new LogicResult<CustomerDetail>() { IsSuccess = false, message = Validation.InvalidParameters };
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);

                var result = await repo.GetByIdAsync(id);

                if (result == null)
                {
                    return new LogicResult<CustomerDetail>() { IsSuccess = false, message = Validation.CustomerIsNotInOutlet };
                }
                return new LogicResult<CustomerDetail>() { IsSuccess = true, Result = _mapper.Map<CustomerDetail>(result) };
            }
        }
        
        public async Task<LogicResult<CustomerDetail>> GetCustomerbyUsernamePassword(string username, string password)
        {
            if (username =="")
                return new LogicResult<CustomerDetail>() { IsSuccess = false, message = Validation.InvalidParameters };
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);
                string hashpass= CreateMD5(password);
            //string hashpass = password;
            var result = await repo.GetFirstAsync(x => (x.Phone == username || x.Email == username) && x.passWord == hashpass);

                if (result == null)
                {
                    return new LogicResult<CustomerDetail>() { IsSuccess = false, message = Validation.CustomerIsNotInOutlet };
                }
                var Res = _mapper.Map<CustomerDetail>(result);
                return new LogicResult<CustomerDetail>() { IsSuccess = true, Result =Res };
        }

        public async Task<LogicResult<CustomerProfileDetail>> GetCustomerProfile(int id)
        {
            if (id <= 0)
                return new LogicResult<CustomerProfileDetail>() { IsSuccess = false, message = Validation.InvalidParameters };
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);
                var custommer = await repo.GetByIdAsync(id);
                var result = new CustomerProfileDetail()
                {
                    ID = custommer.ID,
                    Gender = custommer.Gender,
                    DoB = custommer.DoB,
                    //Age = DateTime.Now.Year - custommer.DoB.Value.Year,
                    Profession = custommer.Profression,
                    PassWord = custommer.passWord,
                    Phone = custommer.Phone,
                    Email = custommer.Email,
                    NRIC = custommer.NRIC,
                    Avatar = custommer.Avatar,
                    Name = custommer.Name,
                    blackList=custommer.blackList.GetValueOrDefault(0),
                    //Measurement = custommer.Measurement
                    
                };
                return new LogicResult<CustomerProfileDetail>() { IsSuccess = true, Result = result };
            }
        }

        public async Task<LogicResult<IEnumerable<CustomerUpComming>>> GetCustomerByOutlet_Therapist_Date(int outlet, int therapist, DateTime date)
        {
            var unitOfWork = _repositoryHelper.GetUnitOfWork();
            var therapistRepo = _repositoryHelper.GetRepository<ITherapistRepository>(unitOfWork);
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitOfWork);
            var outletRepo = _repositoryHelper.GetRepository<IOutletRepository>(unitOfWork);

            // get current therapist
            var currentTherapist = await therapistRepo.GetByIdAsync(therapist);
            if (currentTherapist == null)
                return new LogicResult<IEnumerable<CustomerUpComming>>() { IsSuccess = false, message = Validation.TherepistIsNotInOutlet };

            var currentOutlet = await outletRepo.GetByIdAsync(outlet);
            if(currentOutlet == null)
            {
                return new LogicResult<IEnumerable<CustomerUpComming>>() { IsSuccess = false, message = Validation.OuletNotInSystem };
            }

            // get all appointment by outlet
            
            var appointments = await appointmentDetailRepo.GetAppointmentDetailIncludeCustomerAppointmentService(outlet, therapist, date);

            var customerdto = new List<CustomerUpComming>();

            foreach( var item in appointments)
            {
                var customer = new CustomerUpComming()
                {
                    AppointmentID = item.Appointment,
                    Name = item.Appointment1.Customer1.Name,
                    Phone = item.Appointment1.Customer1.Phone,
                    Avatar = item.Appointment1.Customer1.Avatar,
                    Bed = item.Bed.HasValue? item.Bed.Value : 0,
                    Room = item.Bed1.Room.Value,
                    Services = item.Service1.Name
                };

                customerdto.Add(customer);
            };

            return new LogicResult<IEnumerable<CustomerUpComming>>() { IsSuccess = true, Result = customerdto };
        }

         //post customer register
        public LogicResult PostCustomer(CustomerRegiter customer)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);

            if (customer.PassWord != customer.ConfirmPassWord)
                return new LogicResult() { IsSuccess = false, message = Validation.ConfirmPassWordNotCorrect };

            //check NRIC is unique
            var CheckCustomerNRIC = repo.GetExists(x => x.NRIC == customer.NRIC);
            if (CheckCustomerNRIC)
                return new LogicResult() { IsSuccess = false, message = Validation.NRICExisted };

            //hash password
            var md5Pass = CreateMD5(customer.PassWord);

            var customerToSave = new Customer()
            {
                Name = customer.Name,
                DoB = customer.DOB,
                Gender = customer.Gender,
                passWord = md5Pass,
                NRIC = customer.NRIC,
                Phone = customer.Phone,
                Email=customer.Email,
            };
            repo.Create(customerToSave);
            unitofwork.SaveChanges();

            return new LogicResult() { IsSuccess = true };
        }

        public async Task<LogicResult> EditCustomer(CustomerProfileDetail customer)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);

            var currentCustomer = repo.GetById(customer.ID);

            //hash password
            //var md5Pass = CreateMD5(customer.PassWord);

            currentCustomer.Name = customer.Name;
            currentCustomer.DoB = customer.DoB;
            currentCustomer.Gender = customer.Gender;
            currentCustomer.Profression = customer.Profession;
            //currentCustomer.passWord = md5Pass;


            if (currentCustomer.Email == null)
                currentCustomer.Email = customer.Email;
            else if(currentCustomer.Email != customer.Email)
                return new LogicResult<IEnumerable<CustomerProfileDetail>>() { IsSuccess = false, message = Validation.EmailCanNotChange };
            if (currentCustomer.Phone == null)
                currentCustomer.Phone = customer.Phone;
            else if(currentCustomer.Phone != customer.Phone)
                return new LogicResult<IEnumerable<CustomerProfileDetail>>() { IsSuccess = false, message = Validation.PhoneCanNotChange };
            if (currentCustomer.NRIC == null)
                currentCustomer.NRIC = customer.NRIC;
            else if (currentCustomer.NRIC != customer.NRIC)
                return new LogicResult<IEnumerable<CustomerProfileDetail>>() { IsSuccess = false, message = Validation.NRICCanNotChange };

            var updateField = new string[]
            {
                nameof(Customer.Email),
                nameof(Customer.Phone),
                nameof(Customer.Name),
                nameof(Customer.DoB),
                nameof(Customer.Gender),
                nameof(Customer.Profression),
               // nameof(Customer.passWord),
            };

            repo.Update(currentCustomer, updateField);
            var result = await unitofwork.SaveChangesAsync();

            if (!result)
            {
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }
            return new LogicResult() { IsSuccess = true };
        }
        public async Task<LogicResult<IEnumerable<CustomerListDTO>>> GetCustomerByOutletAndText(int idoutlet, string txt)
        {
            var unitOfWork = _repositoryHelper.GetUnitOfWork();
            var appointmentdtRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitOfWork);
            var outletRepo = _repositoryHelper.GetRepository<IOutletRepository>(unitOfWork);
            //var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitOfWork);


            var currentOutlet = await outletRepo.GetByIdAsync(idoutlet);
            if (currentOutlet == null)
            {
                return new LogicResult<IEnumerable<CustomerListDTO>>() { IsSuccess = false, message = Validation.OuletNotInSystem };
            }

            var app = await appointmentdtRepo.GetCustomerByOutletAndText(idoutlet, txt);

            var customerlistdto = new List<CustomerListDTO>();

            foreach (var item in app)
            {
                var customer = new CustomerListDTO()
                {
                    name = item.Appointment1.Customer1.Name,
                    phonenumber = item.Appointment1.Customer1.Phone,
                    avt = item.Appointment1.Customer1.Avatar,
                    email = item.Appointment1.Customer1.Email,
                    nric = item.Appointment1.Customer1.NRIC.ToString()
                };

                    customerlistdto.Add(customer);

            };

            return new LogicResult<IEnumerable<CustomerListDTO>>() { IsSuccess = true, Result = customerlistdto };
        }

        public async Task<LogicResult<IEnumerable<CustomerListDTO>>> GetAllCustomerByOutlet(int idoutlet)
        {
            var unitOfWork = _repositoryHelper.GetUnitOfWork();
            var appointmentdtRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitOfWork);
            var outletRepo = _repositoryHelper.GetRepository<IOutletRepository>(unitOfWork);
            //var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitOfWork);


            var currentOutlet = await outletRepo.GetByIdAsync(idoutlet);
            if (currentOutlet == null)
            {
                return new LogicResult<IEnumerable<CustomerListDTO>>() { IsSuccess = false, message = Validation.OuletNotInSystem };
            }

            var app = await appointmentdtRepo.GetAllCustomerByOutlet(idoutlet);

            var customerlistdto = new List<CustomerListDTO>();

            foreach (var item in app)
            {
                var customer = new CustomerListDTO()
                {
                    name = item.Appointment1.Customer1.Name,
                    phonenumber = item.Appointment1.Customer1.Phone,
                    avt = item.Appointment1.Customer1.Avatar,
                    email = item.Appointment1.Customer1.Email,
                    nric = item.Appointment1.Customer1.NRIC.ToString()
                };

                customerlistdto.Add(customer);

            };

            return new LogicResult<IEnumerable<CustomerListDTO>>() { IsSuccess = true, Result = customerlistdto };
        }

        public async Task<int> CheckCustomerExisByEmailOrPhoneOrNRIC(string info)
        {
            if (info == "" || info == "*")
                return 0;
            else
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);

                var result = await repo.GetAsync(x => x.Email == info||x.Phone==info||x.NRIC.ToString()==info);

                if (!result.Any())
                {
                    return 0;
                }
                return 1;
            }
        }

    }

}
