using Microsoft.AspNetCore.Mvc;

namespace XlibraryPro.API.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public UploadController(IWebHostEnvironment env)
    {
        _env = env;
    }

    // POST api/upload/image?folder=covers
    [HttpPost("image")]
    public async Task<IActionResult> UploadImage(
        IFormFile file,
        [FromQuery] string folder = "images",
        [FromQuery] string? filename = null)
    {
        if (file is null || file.Length == 0)
            return BadRequest(new { error = "No file provided." });

        var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp", "image/gif" };
        if (!allowedTypes.Contains(file.ContentType.ToLower()))
            return BadRequest(new { error = "Only image files are allowed (jpg, png, webp, gif)." });

        if (file.Length > 5 * 1024 * 1024)
            return BadRequest(new { error = "File size must not exceed 5 MB." });

        // Build save path: wwwroot/images/{folder}/
        var wwwroot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var folderPath = Path.Combine(wwwroot, "images", folder);
        Directory.CreateDirectory(folderPath);

        // Use provided filename or generate unique one
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var savedName = filename != null
            ? $"{filename}{ext}"
            : $"{Guid.NewGuid()}{ext}";

        var fullPath = Path.Combine(folderPath, savedName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
            await file.CopyToAsync(stream);

        // Return public URL
        var url = $"/images/{folder}/{savedName}";

        return Ok(new
        {
            url,
            filename = savedName,
            folder,
            size = file.Length,
            mimeType = file.ContentType,
        });
    }

    // POST api/upload/file?folder=documents
    [HttpPost("file")]
    public async Task<IActionResult> UploadFile(
        IFormFile file,
        [FromQuery] string folder = "documents",
        [FromQuery] string? filename = null)
    {
        if (file is null || file.Length == 0)
            return BadRequest(new { error = "No file provided." });

        if (file.Length > 20 * 1024 * 1024)
            return BadRequest(new { error = "File size must not exceed 20 MB." });

        var wwwroot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var folderPath = Path.Combine(wwwroot, "files", folder);
        Directory.CreateDirectory(folderPath);

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var savedName = filename != null
            ? $"{filename}{ext}"
            : $"{Guid.NewGuid()}{ext}";

        var fullPath = Path.Combine(folderPath, savedName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
            await file.CopyToAsync(stream);

        var url = $"/files/{folder}/{savedName}";

        return Ok(new
        {
            url,
            filename = savedName,
            folder,
            size = file.Length,
            mimeType = file.ContentType,
        });
    }
}