using System.Text.Json;
using RecipeSPA.Models;
namespace RecipeSPA.Data;
public class RecipeRepository : IRecipeRepository
{
    private readonly string _filePath;
    private List<Recipe> _recipes;
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    public RecipeRepository(IWebHostEnvironment env)
    {
        var folder = Path.Combine(env.ContentRootPath, "App_Data");
        Directory.CreateDirectory(folder);
        _filePath = Path.Combine(folder, "recipes.json");
        _recipes = Load();
    }
    public List<Recipe> GetAll() => _recipes;
    public Recipe? GetById(Guid id) => _recipes.FirstOrDefault(r => r.Id == id);
    public void Add(Recipe recipe)
    {
        recipe.Id = Guid.NewGuid();
        _recipes.Add(recipe);
        Save();
    }
    public void Update(Recipe recipe)
    {
        var idx = _recipes.FindIndex(r => r.Id == recipe.Id);
        if (idx == -1) return;
        _recipes[idx] = recipe;
        Save();
    }
    public void Delete(Guid id)
    {
        _recipes.RemoveAll(r => r.Id == id);
        Save();
    }
    private List<Recipe> Load()
    {
        if (!File.Exists(_filePath)) return new List<Recipe>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Recipe>>(json, JsonOpts) ?? new List<Recipe>();
    }
    private void Save()
    {
        var json = JsonSerializer.Serialize(_recipes, JsonOpts);
        File.WriteAllText(_filePath, json);
    }
}
