﻿using MyProject.Core.Entities.Content;
using MyProject.Core.Repositories;
using MyProject.Data.Identity;
using MyProject.Services.Configuration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services.Content
{
    public class NodeService : INodeService
    {
        private readonly INodeRepository _nodeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ContentAppSettings _contentAppSettings;

        public NodeService(
            INodeRepository nodeRepository,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _nodeRepository = nodeRepository;
            _contentAppSettings = new ContentAppSettings();
            configuration.Bind(nameof(ContentAppSettings), _contentAppSettings);
            _userManager = userManager;
        }

        public async Task<Core.Entities.Content.Node> GetAsync(string id)
        {
            return await _nodeRepository.GetAsync(id);
        }

        // https://www.mikesdotnetting.com/article/328/simple-paging-in-asp-net-core-razor-pages
        public async Task<List<Node>> GetPaginatedResultAsync(NodeSearch nodeSearch, int currentPage, int pageSize = 10)
        {
            var data = _nodeRepository.Get(nodeSearch);
            var output = await data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            return output;
        }

        public async Task<int> GetCountAsync(NodeSearch nodeSearch)
        {
            var data = _nodeRepository.Get(nodeSearch);
            return await data.CountAsync();
        }

        public async Task<string> AddAsync(Core.Entities.Content.Node node)
        {
            node.Id = Guid.NewGuid().ToString();
            node.CreatedDate = DateTimeOffset.UtcNow.ToString("s");
            if (node.CustomFields != null)
            {
                node.CustomFields.Id = Guid.NewGuid().ToString();
                node.CustomFields.EntityId = node.Id;
            }
            _nodeRepository.Add(node);

            var rootId = node.ParentId;
            if (!string.IsNullOrEmpty(node.ParentId))
            {
                var parent = await _nodeRepository.GetAsync(node.ParentId);
                parent.ChildCount += 1;
                _nodeRepository.Update(parent);
                if (!string.IsNullOrEmpty(parent.RootId))
                    rootId = parent.RootId;
            }
            node.RootId = rootId;
            if (!string.IsNullOrEmpty(node.RootId))
            {
                var root = await _nodeRepository.GetAsync(node.RootId);
                root.DescendantCount += 1;
                _nodeRepository.Update(root);
            }

            await _nodeRepository.SaveChangesAsync();
            return node.Id;
        }

        public async Task UpdateAsync(Node node)
        {
            _nodeRepository.Update(node);
            await _nodeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            _nodeRepository.Delete(id);

            var node = await _nodeRepository.GetAsync(id);
            if (!string.IsNullOrEmpty(node.ParentId))
            {
                var parent = await _nodeRepository.GetAsync(node.ParentId);
                parent.ChildCount -= 1;
                _nodeRepository.Update(parent);
            }
            if (!string.IsNullOrEmpty(node.RootId))
            {
                var root = await _nodeRepository.GetAsync(node.RootId);
                root.DescendantCount -= 1;
                _nodeRepository.Update(root);
            }

            await _nodeRepository.SaveChangesAsync();
        }

        public async Task<int> VoteAsync(string userId, string nodeId, bool isUpVote)
        {
            var vote = await GetVoteAsync(userId, nodeId);

            if (vote == null)
            {
                return await AddVoteAsync(userId, nodeId, isUpVote);
            }
            else
            {
                var votes = await DeleteVoteAsync(userId, nodeId);
                var isExistingUpVote = vote.Score > 0;
                if (isUpVote != isExistingUpVote)
                    return await AddVoteAsync(userId, nodeId, isUpVote);
                return votes;
            }
        }

        public async Task<NodeVote> GetVoteAsync(string userId, string nodeId)
        {
            return await _nodeRepository.GetVoteAsync(userId, nodeId);
        }

        private async Task<int> AddVoteAsync(string userId, string nodeId, bool isUpVote)
        {
            var weight = await GetWeightAsync(userId);
            var score = (short)(isUpVote ? weight : -weight);
            var vote = new NodeVote()
            {
                Id = Guid.NewGuid().ToString(),
                NodeId = nodeId,
                UserId = userId,
                Score = score
            };

            _nodeRepository.AddVote(vote);
            return await UpdateHotAsync(vote, false);
        }

        private async Task<int> DeleteVoteAsync(string userId, string nodeId)
        {
            var vote = await GetVoteAsync(userId, nodeId);
            _nodeRepository.RemoveVote(vote);
            return await UpdateHotAsync(vote, true);
        }

        private async Task<int> UpdateHotAsync(NodeVote vote, bool isUndo)
        {
            var node = await _nodeRepository.GetAsync(vote.NodeId);
            if (vote.Score > 0) node.UpVotes = node.UpVotes + (isUndo? -1 : 1) * vote.Score;
            if (vote.Score < 0) node.DownVotes = node.DownVotes + (isUndo ? 1 : -1) * vote.Score;
            var hot = Hot(node.UpVotes, node.DownVotes, node.CreatedDate);
            node.Hot = hot;

            await _nodeRepository.SaveChangesAsync();

            var diff = node.UpVotes - node.DownVotes;
            return diff > 0 ? diff : 0;
        }

        private async Task<short> GetWeightAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            short maxWeight = 1;
            foreach (var role in roles)
            {
                var roleWeightSetting = _contentAppSettings.RoleWeightSettings.First(rws => rws.Role == role);
                if (roleWeightSetting != null && roleWeightSetting.Weight > maxWeight)
                    maxWeight = roleWeightSetting.Weight;
            }

            return maxWeight;
        }

        private double Hot(int upVotes, int downVotes, string createdDate)
        {
            var score = upVotes - downVotes;
            var order = Math.Log10(Math.Max(Math.Abs(score), 1));
            var sign = 0;
            if (score > 0) sign = 1;
            if (score < 0) sign = -1;

            TimeSpan t = DateTimeOffset.Parse(createdDate) - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            var hot = Math.Round(order + (sign * secondsSinceEpoch) / 45000, 7);
            return hot;
        }
    }
}
