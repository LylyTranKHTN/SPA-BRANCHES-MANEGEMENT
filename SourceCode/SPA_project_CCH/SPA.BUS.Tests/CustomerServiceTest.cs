// <copyright file="CustomerServiceTest.cs">Copyright ©  2018</copyright>
using System;
using System.Threading.Tasks;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPA.BUS.Service;
using SPA.Domain.Models;

namespace SPA.BUS.Service.Tests
{
    /// <summary>This class contains parameterized unit tests for CustomerService</summary>
    [PexClass(typeof(CustomerService))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CustomerServiceTest
    {
        /// <summary>Test stub for GetCustomerById(Int32)</summary>
        [PexMethod]
        public Task<global::LogicResult<Customer>> GetCustomerByIdTest([PexAssumeUnderTest]CustomerService target, int id)
        {
            Task<global::LogicResult<Customer>> result = target.GetCustomerById(id);
            return result;
            // TODO: add assertions to method CustomerServiceTest.GetCustomerByIdTest(CustomerService, Int32)
        }
    }
}
