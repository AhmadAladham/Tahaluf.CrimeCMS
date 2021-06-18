using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrimeFile.Core.Common
{
    public class ServiceResult
    {
        public ServiceResult()
        {
        }

        public ServiceResult(ResultCode status)
        {
            Status = status;
        }

        public ResultCode Status { get; set; }

        public List<Error> Errors { get; set; }

        public bool IsSucceed
        {
            get
            {
                return Status == ResultCode.Ok || Status == ResultCode.NoContent || Status == ResultCode.Created;
            }
        }

        public bool HasErrors
        {
            get
            {
                return Errors != null && Errors.Count > 0;
            }
        }

        public void AddErrors(params string[] codes)
        {
            if (Errors == null)
            {
                Errors = new List<Error>();
            }
            List<Error> errors = codes.Select(c => new Error(c)).ToList();

            Errors.AddRange(errors);
        }

        public static List<string> CreateErrors(params string[] codes)
        {
            return codes.Select(c => c).ToList();
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() : base()
        {
        }

        public ServiceResult(ResultCode status) : base(status)
        {
        }

        public T Data { get; set; }
    }
}
