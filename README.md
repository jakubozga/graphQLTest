# graphQLTest
Proof of concept using GraphQL with EntityFramework
## Instalation
* `git clone <repository-url>` this repository
* Build solution and automactly install all required nuget packages
* Modify connection string named "Default" in S360.GraphGL/Web.Config
* In Package Manager Console run `update-database` with selected Default project as S360.GraphGL.Data 
## Running
* Run S360.GraphQL
* Go to http://localhost:58289/
* Use sample query:
```
{
   character
  {
    id
    name
    planet
    {
      
      name
    }

  }
  droid(id: 6)
  {
    id
    name
    episodes
    {
      title
    }
  }
  episode
  {
    id
    title
    characters
    {
      name
    }
  }
}
```
