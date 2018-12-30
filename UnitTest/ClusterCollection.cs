using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest
{
    [CollectionDefinition(ClusterCollection.Name)]
    public class ClusterCollection: ICollectionFixture<ClusterFixture>
    {
        public const string Name = "ClusterCollection";
    }
}
