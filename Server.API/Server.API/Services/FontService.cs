using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Services;
using Server.API.Utils;

public class FontService : IFontService
{
    private readonly GamesContext context;

    public FontService(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<Font>> GetFontsAsync()
    {
        return await context.Fonts.ToListAsync();
    }

    public async Task<Font> GetFontByIdAsync(Guid id)
    {
        var font = await context.Fonts.FindAsync(id);

        if (font == null)
        {
            throw new KeyNotFoundException("Font not found");
        }

        return font;
    }
    public async Task AddFontAsync(Font font)
    {
        context.Fonts.Add(font);
        await context.SaveChangesAsync();
    }
    public async Task DeleteFontAsync(Guid id)
    {
        var font = context.Fonts.Find(id);
        if (font == null)
        {
            throw new KeyNotFoundException("Font not found");
        }
        context.Fonts.Remove(font);
        await context.SaveChangesAsync();
    }
    public async Task UpdateFontAsync(Guid id, Font font)
    {
        if (context.Fonts.Find(id) == null)
        {
            throw new KeyNotFoundException("Fonts not found");
        }
        context.Fonts.Update(font);
        await context.SaveChangesAsync();
    }
}
