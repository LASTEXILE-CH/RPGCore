using Newtonsoft.Json;
using RPGCore.Packages;
using System.IO;

namespace RPGCore.Behaviour
{
	public class BhvrExporter : ResourceExporter
	{
		public override bool CanExport(IResource resource)
		{
			return resource.Extension == ".bhvr";
		}

		public override void BuildResource(IResource resource, Stream writer)
		{
			var serializer = new JsonSerializer();
			SerializedGraph serializedGraph;

			using (var sr = new StreamReader(resource.LoadStream()))
			using (var reader = new JsonTextReader(sr))
			{
				serializedGraph = serializer.Deserialize<SerializedGraph>(reader);
			}

			foreach (var node in serializedGraph.Nodes)
			{
				node.Value.Editor = default;
			}

			using var streamWriter = new StreamWriter(writer);
			serializer.Serialize(streamWriter, serializedGraph);
		}
	}
}
