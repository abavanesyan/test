using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UFAR.Classroom;
using UFAR.Classroom.Entities;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Services;
using UFAR.Classroom;

[Route("api/[controller]")]
[ApiController]
public class SubmissionController : ControllerBase
{
    private readonly ISubmissionService _submissionService;
    private readonly ApplicationDbContext _context;

    public SubmissionController(ISubmissionService submissionService, ApplicationDbContext context)
    {
        _submissionService = submissionService;
        _context = context;
    }

    // Endpoint to upload a file
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var stream = file.OpenReadStream();
        var fileName = Path.GetFileName(file.FileName);

        // Upload file to blob storage
        string fileUrl = await _submissionService.UploadFileToBlobAsync(stream, fileName);

        // Save file information to the database
        var fileRecord = new FileRecords
        {
            FileName = fileName,
            FileUrl = fileUrl,
            UploadDate = DateTime.UtcNow
        };

        await _context.FileRecords.AddAsync(fileRecord);
        await _context.SaveChangesAsync();

        return Ok(new { FileUrl = fileUrl });
    }

    // Endpoint to download a file based on the fileId
    [HttpGet("download/{fileId}")]
    public async Task<IActionResult> DownloadFile(int fileId)
    {
        // Retrieve file record from the database
        var fileRecord = await _context.FileRecords
            .FirstOrDefaultAsync(fr => fr.Id == fileId);

        if (fileRecord == null)
            return NotFound("File not found.");

        // Retrieve the file from Azure Blob Storage using the fileUrl
        var blobUri = fileRecord.FileUrl;
        var blobClient = new BlobClient(new Uri(blobUri));

        // Download the file from Blob Storage
        var blobDownloadInfo = await blobClient.DownloadAsync();

        // Return the file content to the user
        return File(blobDownloadInfo.Value.Content, "application/octet-stream", fileRecord.FileName);
    }

    // Endpoint to retrieve file metadata based on the fileId
    [HttpGet("details/{fileId}")]
    public async Task<IActionResult> GetFileDetails(int fileId)
    {
        // Retrieve file record from the database
        var fileRecord = await _context.FileRecords
            .FirstOrDefaultAsync(fr => fr.Id == fileId);

        if (fileRecord == null)
            return NotFound("File not found.");

        // Return the file metadata (filename, URL, upload date)
        return Ok(new
        {
            fileRecord.FileName,
            fileRecord.FileUrl,
            fileRecord.UploadDate
        });
    }

    // Endpoint to retrieve all file details
    [HttpGet("all")]
    public async Task<IActionResult> GetAllFileDetails()
    {
        // Retrieve all file records from the database
        var fileRecords = await _context.FileRecords
            .OrderByDescending(fr => fr.UploadDate)  // Optional: Order by upload date
            .ToListAsync();

        if (fileRecords == null || fileRecords.Count == 0)
            return NotFound("No files found.");

        // Return the list of file records
        return Ok(fileRecords);
    }
    [HttpDelete("delete/{fileId}")]
    public async Task<IActionResult> DeleteFile(int fileId)
    {
        // Retrieve the file record from the database
        var fileRecord = await _context.FileRecords.FirstOrDefaultAsync(fr => fr.Id == fileId);

        if (fileRecord == null)
            return NotFound("File not found.");

        // Delete the file from Azure Blob Storage
        await _submissionService.DeleteFileFromBlobAsync(fileRecord.FileUrl);

        // Remove the file record from the database
        _context.FileRecords.Remove(fileRecord);
        await _context.SaveChangesAsync();

        return Ok("File deleted successfully.");
    }



}