﻿using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TendaGo.Web.Services
{
    public interface IEmbedService
    {
        EmbedConfig EmbedConfig { get; }
        TileEmbedConfig TileEmbedConfig { get; }

        Task<bool> EmbedReport(string userName, string roles, params string[] filters);
        Task<bool> EmbedDashboard();
        Task<bool> EmbedTile();
    }
}
