using System;

namespace HayTeknoloji.BigBlueButton.Models
{
    public class ResultModel<T>
    {
        public bool IsSuccess { get; set; }
        public bool InternalError { get; set; }
        public Exception Exception { get; set; }

        public T BbbResponse { get; set; }
    }
}