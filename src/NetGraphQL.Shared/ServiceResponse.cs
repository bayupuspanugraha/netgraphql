using System.Collections.Generic;
using System.Linq;

namespace NetGraphQL.Shared
{
    public interface IServiceResponse
    {
        bool IsError { get; }
        IEnumerable<string> ErrorMessages { get; }

        void AddError(string errorMessage);
    }

    public interface IServiceResponse<TResult> : IServiceResponse
    {
        TResult Result { get; set; }
    }

    public class ServiceResponse : IServiceResponse
    {
        public bool IsError => ErrorMessages.Any();

        private IList<string> _errorMessages = new List<string>();
        public IEnumerable<string> ErrorMessages => _errorMessages;

        public void AddError(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage)) return;
            _errorMessages.Add(errorMessage);
        }
    }

    public class ServiceResponse<TResult> : ServiceResponse, IServiceResponse<TResult>
    {
        public TResult Result { get; set; }

        public IList<string> Messages { get; set; } = new List<string>();
    }

    public class EventResponse<TResult> : ServiceResponse, IServiceResponse<TResult>
    {
        public TResult Result { get; set; }
    }
}
