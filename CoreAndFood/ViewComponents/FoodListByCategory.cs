using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.ViewComponents
{
	public class FoodListByCategory : ViewComponent
	{
		public IViewComponentResult Invoke(int id)
		{
			FoodRepository repository = new FoodRepository();
			var foodList = repository.TList(x => x.CategoryID ==  id);
			return View(foodList);
		}
	}
}
