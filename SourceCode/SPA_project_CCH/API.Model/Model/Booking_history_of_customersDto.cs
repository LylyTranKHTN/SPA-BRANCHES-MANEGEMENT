using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Model.Model
{
    public class Booking_history_of_customersDto
    {
        public int AppointmentId { get; set; }
        public DateTime dateServe { get; set; }
        public string nameService { get; set; }
        // public string typeService { get; set; }
        public int status { get; set; }
        public string nameOutlet { get; set; }
        public string nameCustomer { get; set; }
    }
    public class Booking_history_of_customersGroupDto
    {
        public int AppointmentId { get; set; }
        public string nameCustomer { get; set; }
        public string dateServe { get; set; }
        public string timeServe { get; set; }
        public List<string> listnameService { get; set; }
        // public string typeService { get; set; }
        public string status { get; set; }
        public string nameOutlet { get; set; }
    }
}