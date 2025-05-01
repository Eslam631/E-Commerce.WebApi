namespace E_Commerce.Web.ErroeModels
{
    public class ErrorToReturn
    {
      public  int StatCode { get; set; }
        public string ErrorMessage { get; set; } = default!;

        public List<string>? Errors {  get; set; }

    }
}
