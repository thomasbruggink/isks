using System.Linq;
using Business.Services;
using Engine.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient.Cypher;
using TechTalk.SpecFlow;

namespace Engine.Tests.Bindings.Then
{
    [Binding]
    public class ThenNodeValidation
    {
        [Then(@"I expect the following nodes:")]
        public void ThenIExpectTheFollowingNodes(Table table)
        {
            var graphClient = NeoConnection.GetConnection().GraphClient;

            foreach (var nodeRow in table.Rows)
            {
                var nodeName = nodeRow["Node"];
                var nodeContentName = nodeRow["Name"];

                var names = graphClient.Cypher.Match($"(x:{nodeName} {{Name: '{nodeContentName}'}})")
                    .Return(() => Return.As<KnNode>("x"));
                Assert.AreEqual(1, names.Results.Count(),
                    "Different amount of nodes returned than expected for node '{0}' and value '{1}'",
                    nodeName, nodeContentName);
            }
        }
    }
}
