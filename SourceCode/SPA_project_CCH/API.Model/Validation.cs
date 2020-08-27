using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public static class Validation
    {
        public const string FileNotFound = "File not found.";
        public const string MustNotBeNull = "Must not be null";

        public const string EmailInvalid = "Email is invalid..";
        public const string MobileNumberInvalid = "Mobile number is invalid.";

        public const string InvalidParameters = "Invalid parameters.";
        public const string DateTimeIsInPast = "Datetime is in the past. Datetime have to in a future";

        public const string TotalRecordInvalid = "Total record is invalid.";
        public const string PageSizeInvalid = "Pagesize is invalid.";

        public const string ServerError = "Server error. Please contact with admin to have support.";
        public const string TherepistIsNotInOutlet = "Therapist id is not staff of spa";

        public const string CustomerIsNotInOutlet = "Customer doesn't register yet.";
        public const string OutletIsNotExists = "Outlet id is not exists ";

        public const string PhoneCanNotChange = "Can not change phone number. Please contact with therapist/outlet manager to have support";
        public const string EmailCanNotChange = "Can not change email. Please contact with therapist/outlet manager to have support";
        public const string NRICCanNotChange = "Can not change NRIC. Please contact with therapist/outlet manager to have support";

        //booking
        public const string ServiceNotInOutlet = "Service do not in your outlet";
        public const string AllServiceSoldOut = "All service in your choice date sold out! Please choose another date.";
        public const string OuletNotInSystem = "Cannot find outlet in system";
        public const string BookingFail = "Booking fail. Please contact with admin to have support.";
        public const string BookingSoldOut = "All your booking sold out!";
        public const string SomeBookingSoldOut = "Some booking sold out, but we have already book some your visible booking";
        public const string DayCancelInValid = "You only cancel or edit booking before 2 days booking occus.";
        public const string CustomerNotExit = "Customer does not exit";
        public const string NoOutletWithID = "No outlet with ID";
        public const string InvalidPageIndex = "PageSize and PageNumber out of number records";
        public const string AppointmentNotExists = "Customer does not exists";

        public const string ServiceDoesnotexist = "Service does not exist";

        public const string ConfirmPassWordNotCorrect = "Confirm password don't like password";
        public const string NRICExisted = "NRIC was registered before.";
        public const string CustomerHaveNoAppointment = "Customer not booking yet.";
    }
}
