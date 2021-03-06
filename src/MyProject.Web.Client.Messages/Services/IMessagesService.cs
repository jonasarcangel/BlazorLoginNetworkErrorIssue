﻿using MyProject.Core.Entities.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Messages.Services
{
    public interface IMessageService
    {
        Task<Message[]> GetAsync(string groupId, int currentPage);
        Task<int> GetCountAsync(string groupId);
        Task<int> GetPageSizeAsync(string module);
    }
}
