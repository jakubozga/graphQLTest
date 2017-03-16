function graphQLFetcher(graphQLParams) {
	return fetch(window.location.origin + '/graphql', {
		method: 'post',
		headers: { 'Content-Type': 'application/json' },
		body: JSON.stringify(graphQLParams)
	}).then(response => response.json());
}

ReactDOM.render(
        React.createElement(GraphiQL, {
        	fetcher: graphQLFetcher,
        	query: parameters.query,
        	variables: parameters.variables,
        	operationName: parameters.operationName,
        	onEditQuery: onEditQuery,
        	onEditVariables: onEditVariables,
        	onEditOperationName: onEditOperationName
        }),
        document.getElementById('graphiql')
      );