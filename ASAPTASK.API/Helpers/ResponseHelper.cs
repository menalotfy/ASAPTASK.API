
using ASAPTASK.Core.DTOs;

namespace ASAPTASK.API.Helpers
{
    public class ResponseHelper
    {
      
        public static ResponseModel Success(dynamic data = null, string messageEN = "Operation is done successfully.", string messageAR = "تمت العملية بنجاح", string Message = "", string language = "en")
        {
            switch (language)
            {
                case "en":
                    Message = messageEN; break;
                case "ar":
                    Message = messageAR; break;
            }

            return new ResponseModel { Success = true, StatusCode = 200, Message = Message, Data = data };
        }

        public static ResponseModel Fail(string messageEN = "Something went wrong, try again.", string messageAR = "حدث خطأ حاول مره اخرى", string Message = "", string language = "en")
        {
            switch (language)
            {
                case "en":
                    Message = messageEN; break;
                case "ar":
                    Message = messageAR; break;
            }
            return new ResponseModel { Success = false, StatusCode = 403, Message = Message };
        }
    }
}
