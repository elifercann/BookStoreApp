using BookStoreApi.Utilities.Formatter;

namespace BookStoreApi.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder mvc)=>
       
            mvc.AddMvcOptions(options =>
            {
                options.OutputFormatters.Add(new CsvOutputFormatter());
            });
       
    }
}
