# This is the funda project test for lesley simons

## Running

- Run the solution on debug mode and interact via swagger.


## Project Breakdown

- Funda Api is the entry point through the MakelaarHousesController
- Used the onion architect and therefore core is strictly business logic. Therefore most of the files are in application. I tried to keep core only for logic but there was no complex logic and moving the fundaService into the core without creating models that replicate the entities for the external call. I would have external entity models knowledge within my core domain project.
- Application contains funda application service which is the main coordinator for the funda call.
- Infrastructure.Api.Funda - contains the rest client used for interacting with the Funda service

## Notes
- I noticed on the website that there is an additional flag called status, I used beschikbaar,
- I further noticed tuin is a complex object and began modelling it but realised this was not in the requirements
- MediatR is used to separate the caller from the handler in this case the controller and the Application Project
- Memory Cache is used to limit the number of calls but also Polly is used in case of failures with network calls
- KeyService is abstract to represent the fetching of the key, my preference would be key vault but I did not set that up and demonstrated the abstractions.
- I did use the app settings to fetch the base URL but did not do it with location as to me that will either be provided through the API as a request parameter, however, I would prefer methods that do GetFromAmsterdam as opposed to GetFromLocation(Amsterdam), this is not feasible in a searching system but I coded it that way for this example
- The Base URL is retrieved using Micosroft Configuration Extensions but this could be changed to anything else
- Global Exception handler handled by middleware with the use of custom exceptions
- Rest Client Used but since it's an instance, I wrapped it in a class for better unit tests
- Unit tests are done with fluent assertions, Nunit And Moq.
- Integration tests are done with an in-memory web server
- I wanted to read every page from the api but when I modified the page the HuidigePagina didn't change however if changed the url from &page=2 to p2 it would work. I left the implementation in the code but it's not used, to show I considered it but it may not have been what you wanted? 

## Improvements
- Add A Distributed cache for scale
- Create a domain model, right now it feels very procedural because no business logic can be applied
- Create projection of report. If this is a common report send an event that can calculate the report in the back around that way we can cache these searches assuming that houses aren't updated regularly
- Noticed on the site you have a makerlaar's page, That would probably be more efficient to get these types of results. 
- Pull in all the data and use it in some sort of warehouse structure like SQL cubes
- If this data is huge pulling it into memory is going to cost a significant amount of resources. Perhaps a suggestion would be to ask the dev team to create common queries as a report.
- The data seems unstructured a NO Sql database would be better but at the very least remove the in-memory SQL implementation.
- The Sql implementation needs support for transient failures, retry logic and stale data, potentially unit of work.
- logging to be written to a cloud provider such as application insights right now it's being written to a file.