using Common.Models.Error;
using System;
using System.Text;

namespace EmployeeArrivalTrackerDomain.Adapter
{
    public static class ErrorAdapret
    {
        public static ErrorViewModel Transform(byte[] error)
        {
            string errorMsg = Encoding.ASCII.GetString(error);
            return new ErrorViewModel
            {
                ErrorMsg = errorMsg,
                Time = DateTime.Now.ToString("yyyy-MM-dd")
            };
        }
    }
}
