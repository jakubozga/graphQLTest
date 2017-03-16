using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;
using S360.GraphQL.Data;
using S360.GraphQL.Query;

namespace S360.GraphQL.Controllers
{
    public class GraphQLQuery
    {
        public string Query { get; set; }
    }

    public class GraphQLController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody] GraphQLQuery query)
        {
            var schema = new Schema
            {
                Query = new StarWarsQuery(new StarWarsContext())
            };

            var result = new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;

            }).Result;

            if (result.Errors?.Count > 0)
            {
                var errors = String.Join(Environment.NewLine, result.Errors.Select(x => x.Message));
                return BadRequest(errors);
            }

            return Ok(result);
        }
    }
}
