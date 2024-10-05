using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Store.Service.CashingService;

namespace Store.WebAPI.Helpers
{
    public class CashAttribute : Attribute, IAsyncActionFilter
	{
		public CashAttribute(int timeToLiveInSecounds)
		{
			_timeToLiveInSecounds = timeToLiveInSecounds;
		}

		public readonly int _timeToLiveInSecounds;

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var _cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
			var cashKey = GenerateKeyForCash(context.HttpContext.Request);
			var cashedResponse = await _cashService.GetCashResponseAsync(cashKey);
			if (!string.IsNullOrEmpty(cashedResponse))
			{
				var contentResult = new ContentResult
				{
					Content = cashedResponse,
					ContentType = "application/json",
					StatusCode = 200
				};
				context.Result = contentResult;
				return;
			}
			var executeResult = await next();
			if (executeResult.Result is OkObjectResult response)
				await _cashService.SetCashResponseAsync(cashKey, response.Value, TimeSpan.FromSeconds(_timeToLiveInSecounds));
		}
		private string GenerateKeyForCash(HttpRequest request)
		{
			StringBuilder cashKey = new StringBuilder();
			cashKey.Append($"{request.Path}");
			foreach (var key in request.Query.OrderBy(x => x.Key))
				cashKey.Append($"|{key}");
			return cashKey.ToString();

		}
	}
}
