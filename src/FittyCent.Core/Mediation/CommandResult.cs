using System.Collections.Generic;
using System.Linq;

namespace FittyCent.Core.Mediation {
    public sealed class CommandResult : CommandResult<UnitType> {
        private CommandResult(params string[] errors)
            : base(UnitType.Default, errors) { }

        public static CommandResult Success() {
            return new CommandResult();
        }

        public static CommandResult Fail(IEnumerable<string> errors) {
            return Fail(errors.ToArray());
        }

        public static CommandResult Fail(params string[] errors) {
            if ( !errors.Any() ) {
                errors = new[] { "Unspecified Error." };
            }

            return new CommandResult(errors);
        }
    }

    public class CommandResult<TResponse> {
        private readonly string[] _errors;
        private readonly TResponse _responseData;

        protected CommandResult(TResponse responseData, params string[] errors) {
            _responseData = responseData;
            _errors = errors;
        }


        public IEnumerable<string> Errors {
            get { return _errors; }
        }

        public bool HasErrors {
            get { return _errors.Any(); }
        }

        public TResponse Data { get { return _responseData; } }


        public static CommandResult<T> Success<T>(T data) {
            return new CommandResult<T>(data);
        }
        
        public static CommandResult<T> Fail<T>(T data, IEnumerable<string> errors) {
            return Fail(data, errors.ToArray());
        }

        public static CommandResult<T> Fail<T>(T data, params string[] errors) {
            if ( !errors.Any() ) {
                errors = new[] { "Unspecified Error." };
            }

            return new CommandResult<T>(data, errors);
        }
    }
}
