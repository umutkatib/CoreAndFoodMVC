using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.ViewComponents
{
	public class CategoryGetList : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			CategoryRepository repository = new CategoryRepository();
			var categorytList = repository.TList(x => x.Status == true);
			return View(categorytList);
		}
	}
}
