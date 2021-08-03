﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peer.Domain;
using Peer.Domain.Models;

namespace Peer.ConnectorApi
{
    public class PeerPullRequestAPI<T> where T : class
    {
        private readonly List<IPullRequestSource<T>> _sources;
        private List<PeerPullRequest> PRList;

        public PeerPullRequestAPI(IEnumerable<IPullRequestSource<T>> sources)
        {
            _sources = sources.ToList();
        }

        public async Task<List<PeerPullRequest>> GetPullRequests()
        {
            var tasks = _sources.Select(x => x.FetchPullRequests());
            var prs = await Task.WhenAll(tasks);
            PRList = prs.SelectMany(x => x).ToList();
            return PRList;
        }
    }
}