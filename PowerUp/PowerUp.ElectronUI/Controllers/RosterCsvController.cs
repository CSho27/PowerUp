﻿using Microsoft.AspNetCore.Mvc;
using PowerUp.CSV;
using PowerUp.Databases;
using PowerUp.Entities.Rosters;
using System.Net.Mime;

namespace PowerUp.ElectronUI.Controllers
{
  [Route("csv")]
  public class RosterCsvController : Controller
  {
    private const string ExportUrl = "export";

    private readonly IPlayerCsvService _csvService;

    public RosterCsvController(IPlayerCsvService csvService)
    {
      _csvService = csvService;
    }

    [Route(ExportUrl), HttpGet]
    public async Task<ActionResult> Export(int rosterId)
    {
      var csvStream = new MemoryStream();
      var roster = DatabaseConfig.Database.Load<Roster>(rosterId);
      if (roster is null)
        return NotFound();
      await _csvService.ExportRoster(csvStream, roster);
      return File(csvStream, MediaTypeNames.Text.Csv, $"{roster.Name}.csv");
    }
  }
}
