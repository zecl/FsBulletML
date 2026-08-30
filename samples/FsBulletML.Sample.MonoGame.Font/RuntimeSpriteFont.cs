using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StbTrueTypeSharp;

namespace FsBulletML.Sample.MonoGame.Font;

/// <summary>
/// Bakes a TTF into a MonoGame SpriteFont at runtime so samples still run
/// when old XNB SpriteFonts fail on DesktopGL 3.8.
/// </summary>
public static unsafe class RuntimeSpriteFont
{
    public static SpriteFont Bake(GraphicsDevice device, float pixelHeight = 14f)
    {
        var ttf = FindSystemTtf();
        if (ttf is null)
            throw new FileNotFoundException("No system TTF found for SpriteFont fallback.");

        var font = StbTrueType.CreateFont(ttf, 0)
            ?? throw new InvalidOperationException("stbtt_CreateFont failed.");

        float scale = StbTrueType.stbtt_ScaleForPixelHeight(font, pixelHeight);
        int ascent, descent, lineGap;
        StbTrueType.stbtt_GetFontVMetrics(font, &ascent, &descent, &lineGap);
        int iAscent = (int)Math.Round(ascent * scale);
        int iDescent = (int)Math.Round(descent * scale);
        int lineSpacing = Math.Max(1, iAscent - iDescent + (int)Math.Round(lineGap * scale));

        var chars = new List<char>();
        for (int c = 32; c <= 126; c++) chars.Add((char)c);
        chars.Add('▲'); chars.Add('▼'); chars.Add('…');

        var raw = new List<(char ch, int w, int h, int xoff, int yoff, int adv, byte[] bmp)>();
        foreach (var ch in chars)
        {
            int w, h, xoff, yoff;
            byte* bmp = StbTrueType.stbtt_GetCodepointBitmap(font, scale, scale, ch, &w, &h, &xoff, &yoff);
            int adv, lsb;
            StbTrueType.stbtt_GetCodepointHMetrics(font, ch, &adv, &lsb);
            int iAdv = Math.Max(1, (int)Math.Round(adv * scale));
            byte[] data = Array.Empty<byte>();
            if (bmp != null && w > 0 && h > 0)
            {
                data = new byte[w * h];
                Marshal.Copy((IntPtr)bmp, data, 0, data.Length);
                StbTrueType.stbtt_FreeBitmap(bmp, null);
            }
            raw.Add((ch, Math.Max(w, 0), Math.Max(h, 0), xoff, yoff, iAdv, data));
        }

        const int pad = 1;
        int atlasW = 512;
        int x = pad, y = pad, rowH = 0, atlasH = 64;
        var placed = new List<(char ch, Rectangle glyph, Rectangle crop, Vector3 kern, int adv)>();
        foreach (var g in raw)
        {
            int gw = Math.Max(g.w, 1), gh = Math.Max(g.h, 1);
            if (x + gw + pad > atlasW)
            {
                x = pad;
                y += rowH + pad;
                rowH = 0;
            }
            rowH = Math.Max(rowH, gh);
            placed.Add((g.ch, new Rectangle(x, y, gw, gh), new Rectangle(g.xoff, g.yoff, g.w, g.h),
                new Vector3(0, g.adv, 0), g.adv));
            x += gw + pad;
            atlasH = Math.Max(atlasH, y + rowH + pad);
        }
        atlasH = (atlasH + 3) & ~3;

        var rgba = new byte[atlasW * atlasH * 4];
        for (int i = 0; i < raw.Count; i++)
        {
            var g = raw[i];
            var loc = placed[i].glyph;
            for (int py = 0; py < g.h; py++)
            for (int px = 0; px < g.w; px++)
            {
                byte a = g.bmp[py * g.w + px];
                int idx = ((loc.Y + py) * atlasW + (loc.X + px)) * 4;
                rgba[idx] = 255;
                rgba[idx + 1] = 255;
                rgba[idx + 2] = 255;
                rgba[idx + 3] = a;
            }
        }

        var tex = new Texture2D(device, atlasW, atlasH, false, SurfaceFormat.Color);
        tex.SetData(rgba);

        var glyphs = placed.Select(p => p.glyph).ToList();
        var cropping = placed.Select(p => p.crop).ToList();
        var charMap = placed.Select(p => p.ch).ToList();
        var kerning = placed.Select(p => p.kern).ToList();
        return new SpriteFont(tex, glyphs, cropping, charMap, lineSpacing, 0f, kerning, ' ');
    }

    static byte[]? FindSystemTtf()
    {
        string[] candidates =
        {
            "/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf",
            "/usr/share/fonts/truetype/liberation/LiberationSans-Regular.ttf",
            "/usr/share/fonts/truetype/freefont/FreeSans.ttf",
            "/usr/share/fonts/opentype/noto/NotoSansCJK-Regular.ttc",
        };
        foreach (var c in candidates)
            if (File.Exists(c)) return File.ReadAllBytes(c);
        foreach (var root in new[] { "/usr/share/fonts/truetype", "/usr/share/fonts" })
        {
            if (!Directory.Exists(root)) continue;
            var hit = Directory.EnumerateFiles(root, "*.ttf", SearchOption.AllDirectories).FirstOrDefault();
            if (hit != null) return File.ReadAllBytes(hit);
        }
        return null;
    }
}
