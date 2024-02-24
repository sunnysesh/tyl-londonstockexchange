
# Evaluation of London Stock Broker API

## Benefits
- **Clear separation of logic between trade & price model**:
    - The codebase demonstrates a clear separation of concerns between trade and price models, enhancing code readability and maintainability.
- **Application of SOLID principles**:
    - Trade & price value responsibilities are split between two services, both of which have dependency inversion implemented for them.
    - Interface segregation is observed with services & generic repository pattern.
- **Commits showcase Test-Driven Development (TDD)**
    - Commit history reflects the use of TDD.

## Drawbacks
- **Limited to adding one trade at a time**:
  - Current implementation supports only adding one trade at a time without idempotency, potentially limiting scalability and reliability.
- **Lack of logging**:
  - Incorporating logging functionality would enhance observability, helping in debugging and monitoring.
- **Missing custom exceptions**:#
  - Implementing custom exceptions would also complement logging efforts and improve observability.
- **Additional unit tests for the controller**:
  - This would strengthen test coverage for the controller and would enhance overall reliability.
- **Functional tests for the entire process**:
  - Itroducing functional tests for end-to-end scenarios would provide a more comprehensive validation of the API.

## Enhancements & Improvements
- **Implemtation of data store**:
    - A data store and some form of caching for each transaction made would greatly improve the APIs reliability & consitency.
    - As the current data models are of small volumes and high throughput would be needed for a stock exchange, I would recommend going for some form of a NoSQL database.
- **External validation for tickers & broker IDs**:
    - Incorporating external validation mechanisms for tickers and broker IDs would enhance data integrity and security.
    - This could be through a secrets.json file, an external database or an entireley new microservice
- **Transition to event-sourcing architecture**:
    - Given the immutability of trades in stock market contexts, I believe that some form of event sourcing would align well with this domain.
    - An example event could be 'TransactionReceived/ShareExchanged', where we could maintain a history of each event/trade. and implement a source table for tickers to update values.
    - This would also offer scalability advantages for higher traffic scenarios, as the broker would not have to wait for a response from our API and could continue processing trades asynchronously.
    - This could be split into several microservices for futher decoupling of the system & improved scalability.