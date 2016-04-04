using System.Collections.Generic;
using System.Linq;
using Business.Services;
using Engine.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo4jClient.Cypher;
using TechTalk.SpecFlow;

namespace Engine.Tests.Bindings.Then
{
    [Binding]
    public class ThenRelationValidation
    {
        [Then(@"I expect the following relations:")]
        public void ThenIExpectTheFollowingRelations(Table table)
        {
            var graphClient = NeoConnection.GetConnection().GraphClient;

            foreach (var relationRow in table.Rows)
            {
                var fromNode = relationRow["From Node"];
                var fromName = relationRow["From Name"];
                var toNode = relationRow["To Node"];
                var toName = relationRow["To Name"];
                var relationName = relationRow["RelationName"];
                var attributes = relationRow["Attributes"];

                var results = graphClient.Cypher.Match(
                    $"(x:{fromNode} {{Name: '{fromName}'}})-[r:{relationName}]->(y:{toNode} {{Name: '{toName}'}})")
                    .Return(() => Return.As<KnRelation>("r")).Results.ToList();
                Assert.AreEqual(1, results.Count, "Expected only 1 relation but found a different amount");

                var relation = results.First();

                Assert.AreEqual(relationName, relation.Type, "Relation name does not match");

                if (attributes.Equals("-"))
                    return;

                var attributeListSplit = attributes.Split(',').Where(t => !string.IsNullOrEmpty(t)).ToList();
                var attributeList = new Dictionary<string, double>();
                if (attributeListSplit.Count > 0)
                {
                    attributeList = attributeListSplit
                        .Select(a => a.Trim())
                        .Select(a => new KeyValuePair<string, double>(a.Split(' ')[0], double.Parse(a.Split(' ')[1])))
                        .ToDictionary(ts => ts.Key, ts => ts.Value);
                }


                Assert.AreEqual(attributeList.Count, relation.Data.Count, "Expected different amount of attributes from '{0}:{1}' :{2} '{3}:{4}'",
                    fromNode, fromName, relationName, toNode, toName);

                foreach (var attribute in attributeList)
                {
                    Assert.IsTrue(relation.Data
                        .Any(a => a.Key.Equals(attribute.Key)
                        && a.Value.Equals(attribute.Value)),
                        "Could not find attribute '{0}' with value '{1}' on relation '{2}' from node '{3}' with value '{4}' to node '{5}' with value '{6}'"
                        , attribute.Key, attribute.Value, relationName, fromNode, fromName, toNode, toName);
                }
            }
        }

        [Then(@"I expect not to see the following relations:")]
        public void ThenIExpectNotToSeeTheFollowRelations(Table table)
        {
            var graphClient = NeoConnection.GetConnection().GraphClient;

            foreach (var relationRow in table.Rows)
            {
                var fromNode = relationRow["From Node"];
                var fromName = relationRow["From Name"];
                var toNode = relationRow["To Node"];
                var toName = relationRow["To Name"];
                var relationName = relationRow["RelationName"];

                var results = graphClient.Cypher.Match(
                    $"(x:{fromNode} {{Name: '{fromName}'}})-[r:{relationName}]->(y:{toNode} {{Name: '{toName}'}})")
                    .Return(() => Return.As<KnRelation>("r")).Results.ToList();

                Assert.AreEqual(0, results.Count, "Did not expect a relation to be found from '{0}:{1}' :{2} '{3}:{4}'",
                    fromNode, fromName, relationName, toNode, toName);
            }
        }
    }
}
