using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeSPA.Data;
using RecipeSPA.Models;
namespace RecipeSPA.Pages;
public class IndexModel : PageModel
{
    private readonly IRecipeRepository _repo;
    public IndexModel(IRecipeRepository repo) => _repo = repo;
    // Начальная загрузка страницы
    public void OnGet() { }
    // API: получить все рецепты
    public IActionResult OnGetRecipes()
    {
        return new JsonResult(_repo.GetAll());
    }
    // API: получить один рецепт
    public IActionResult OnGetRecipe(Guid id)
    {
        var r = _repo.GetById(id);
        if (r == null) return new NotFoundResult();
        return new JsonResult(r);
    }
    // API: создать рецепт
    public IActionResult OnPostCreate([FromBody] Recipe recipe)
    {
        if (string.IsNullOrWhiteSpace(recipe.Name))
            return new BadRequestResult();
        _repo.Add(recipe);
        return new JsonResult(new { success = true });
    }
    // API: обновить рецепт
    public IActionResult OnPostUpdate([FromBody] Recipe recipe)
    {
        if (recipe.Id == Guid.Empty)
            return new BadRequestResult();
        _repo.Update(recipe);
        return new JsonResult(new { success = true });
    }
    // API: удалить рецепт
    public IActionResult OnPostDeleteItem([FromBody] DeleteRequest req)
    {
        _repo.Delete(req.Id);
        return new JsonResult(new { success = true });
    }
    public class DeleteRequest
    {
        public Guid Id { get; set; }
    }
}
