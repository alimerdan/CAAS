# CAAS
## Cryptography As a Service

- This project is the implementation of the proposed solution in the published paper [Design and Implementation of a Dockerized, Cross Platform, Multi-Purpose Cryptography as a Service Framework Featuring Scalability, Extendibility and Ease of Integration](https://ieeexplore.ieee.org/document/10009317) using .NET CORE 3.1
- It is hosted live and can be accessed [here](https://caas.alimerdan.xyz/) 
- It aims to provide cryptographic operations via APIs using JSON
- It also contains a docker file for easier deployment, along with kubernetes ingress/deployment YAML files
- Swagger documentaation is maintaned for better understanding of each endpoint parameters and expected response
- The project is covered 100% via unit tests (using Xunit), and the report could be generated by running script "/tests/CAAS.Tests/ExecCodeCoverage.ps1"
