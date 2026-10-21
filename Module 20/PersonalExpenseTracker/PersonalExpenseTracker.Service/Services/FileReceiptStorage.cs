using Microsoft.Extensions.Logging;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

// This service owns receipt validation and disk operations; MVC never saves files.
public class FileReceiptStorage(string receiptDirectory, ILogger<FileReceiptStorage> logger) : IReceiptStorage
{
    public const int MaxBytes = 2 * 1024 * 1024;

    public async Task<string> SaveAsync(ReceiptUpload upload, CancellationToken ct = default)
    {
        var extension = Path.GetExtension(upload.FileName);
        if (!extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
            && !extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
            throw new ReceiptValidationException("Only JPG and PNG receipt images are allowed.");
        if (upload.Length <= 0 || upload.Length > MaxBytes)
            throw new ReceiptValidationException("The receipt must be a non-empty image no larger than 2 MB.");

        // Count actual bytes too: metadata alone is not a reliable size limit.
        using var buffer = new MemoryStream();
        var chunk = new byte[81920];
        int read;
        while ((read = await upload.Content.ReadAsync(chunk, ct)) != 0)
        {
            if (buffer.Length + read > MaxBytes)
                throw new ReceiptValidationException("The receipt must be no larger than 2 MB.");
            await buffer.WriteAsync(chunk.AsMemory(0, read), ct);
        }

        var bytes = buffer.ToArray();
        var isPng = bytes.AsSpan().StartsWith(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 });
        var isJpg = bytes.AsSpan().StartsWith(new byte[] { 255, 216, 255 });
        if ((extension.Equals(".png", StringComparison.OrdinalIgnoreCase) && !isPng)
            || (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) && !isJpg))
            throw new ReceiptValidationException("The receipt content must match its JPG or PNG extension.");

        Directory.CreateDirectory(receiptDirectory);
        var fileName = $"{Guid.NewGuid():N}{extension}";
        var path = Path.Combine(receiptDirectory, fileName);
        try
        {
            await using var output = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            await output.WriteAsync(bytes, ct);
        }
        catch
        {
            Delete(fileName);
            throw;
        }
        return fileName;
    }

    public bool Delete(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) return true;
        // Only application-generated filenames can ever be removed.
        if (Path.GetFileName(fileName) != fileName
            || !Guid.TryParseExact(Path.GetFileNameWithoutExtension(fileName), "N", out _)
            || !(Path.GetExtension(fileName).Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                || Path.GetExtension(fileName).Equals(".png", StringComparison.OrdinalIgnoreCase)))
        {
            logger.LogWarning("Refused to delete an invalid receipt filename.");
            return false;
        }
        try
        {
            File.Delete(Path.Combine(receiptDirectory, fileName));
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            logger.LogError(ex, "Could not delete receipt {ReceiptFileName}.", fileName);
            return false;
        }
    }
}
