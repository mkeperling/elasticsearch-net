﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Snapshots
{
	[TestFixture]
	public class GetSnapshotRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public GetSnapshotRequestTests()
		{
			var request = new GetSnapshotRequest("repos", "snap");
			var response = this._client.GetSnapshot(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_snapshot/repos/snap");
			this._status.RequestMethod.Should().Be("GET");
		}
	}
}
