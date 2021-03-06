﻿using MyProject.Core.Constants;
using MyProject.Core.Entities.Content;
using MyProject.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Data.Repositories
{
    public class NodeRepository : Repository, INodeRepository
    {
        public NodeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Node> GetAsync(string id)
        {
            return await _dbContext.Nodes.FindAsync(id);
        }

        public IQueryable<Node> Get(NodeSearch search)
        {
            var nodes = from n in _dbContext.Nodes
                            .Include(n => n.CustomFields)
                        where (
                            (string.IsNullOrEmpty(search.Module) || n.Module == search.Module) &&
                            (string.IsNullOrEmpty(search.Type) || n.Type == search.Type) &&
                            (string.IsNullOrEmpty(search.Slug) || n.Slug == search.Slug) &&
                            (string.IsNullOrEmpty(search.ParentId) || n.ParentId == search.ParentId) &&
                            (string.IsNullOrEmpty(search.CategoryId) || n.CategoryId == search.CategoryId) &&
                            (string.IsNullOrEmpty(search.Path) || n.Path.StartsWith(search.Path)) &&
                            (string.IsNullOrEmpty(search.GroupId) || n.GroupId == search.GroupId)
                        )
                        select n;

            if (search.OrderBy != null && search.OrderBy.Length > 0)
            {
                IOrderedQueryable<Core.Entities.Content.Node> sortedNodes = null;
                switch (search.OrderBy[0])
                {
                    case (OrderBy.Title):
                        sortedNodes = nodes.OrderBy(i => i.Title);
                        break;
                    case (OrderBy.Weight):
                        sortedNodes = nodes.OrderByDescending(i => i.Weight);
                        break;
                    case (OrderBy.Earliest):
                        sortedNodes = nodes.OrderBy(i => i.CreatedDate);
                        break;
                    case (OrderBy.Latest):
                        sortedNodes = nodes.OrderByDescending(i => i.CreatedDate);
                        break;
                    case (OrderBy.ChildCount):
                        sortedNodes = nodes.OrderByDescending(i => i.ChildCount);
                        break;
                    case (OrderBy.DescendantCount):
                        sortedNodes = nodes.OrderByDescending(i => i.DescendantCount);
                        break;
                    case (OrderBy.Hot):
                        sortedNodes = nodes.OrderByDescending(i => i.Hot);
                        break;
                }

                if (search.OrderBy.Length > 1)
                {
                    for (int i = 1; i < search.OrderBy.Length; i++)
                    {
                        switch (search.OrderBy[i])
                        {
                            case (OrderBy.Title):
                                sortedNodes = sortedNodes.ThenBy(i => i.Title);
                                break;
                            case (OrderBy.Weight):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.Weight);
                                break;
                            case (OrderBy.Earliest):
                                sortedNodes = sortedNodes.ThenBy(i => i.CreatedDate);
                                break;
                            case (OrderBy.Latest):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.CreatedDate);
                                break;
                            case (OrderBy.ChildCount):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.ChildCount);
                                break;
                            case (OrderBy.DescendantCount):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.DescendantCount);
                                break;
                            case (OrderBy.Hot):
                                sortedNodes = sortedNodes.ThenByDescending(i => i.Hot);
                                break;
                        }
                    }

                    return sortedNodes;
                }
            }

            return nodes;
        }

        public void Add(Node node)
        {
            _dbContext.Nodes.Add(node);
        }

        public void Update(Node node)
        {
            _dbContext.Nodes.Update(node);
        }

        public void Delete(string id)
        {
            _dbContext.Remove(_dbContext.Nodes.Single(i => i.Id == id));
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<NodeVote> GetVoteAsync(string userId, string nodeId)
        {
            var vote = from v in _dbContext.NodeVotes
                       where v.NodeId == nodeId && v.UserId == userId
                       select v;
            return await vote.FirstOrDefaultAsync();
        }

        public void AddVote(NodeVote vote)
        {
            _dbContext.NodeVotes.Add(vote);
        }

        public void RemoveVote(NodeVote vote)
        {
            _dbContext.NodeVotes.Remove(vote);
        }
    }
}
