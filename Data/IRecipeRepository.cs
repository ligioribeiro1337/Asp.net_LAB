using RecipeSPA.Models;
namespace RecipeSPA.Data;
public interface IRecipeRepository
{
    List<Recipe> GetAll();
    Recipe? GetById(Guid id);
    void Add(Recipe recipe);
    void Update(Recipe recipe);
    void Delete(Guid id);
}
