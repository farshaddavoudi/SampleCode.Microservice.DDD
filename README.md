
# Basic:
 - All basic descriptions you can also see in this [repo](https://github.com/farshaddavoudi/SampleMicroserviceApp.Identity/blob/main/README.md). 
# Domain-Driven Design:
 - Pure rich domain models disregarding persistence concerns
 - Using advanced LINQ queries to join between tables as a result of not having navigation properties
 - Pushing logic down to the domain project by using patterns such as Double Dispatch pattern
 - The Application project is merely a request orchestrator containing use cases and routes the requests to the Domain layer. Not much logic there anymore as most logic is pushing down to the Domain layer.
