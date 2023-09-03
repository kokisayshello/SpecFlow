Feature: Library Manager API tests

@Passed
Scenario: 01 Library is empty
    Given URL address http://localhost:9000/api/books/
    When User sends GET request to http://localhost:9000/api/books/
    Then The response is 200 OK and empty body []
@Failed
Scenario:02 Add First book to empty Library
    Given URL address http://localhost:9000/api/books/
    When User sends POST request with JSON:
        |  Key          |  Value              |
        | --------------|---------------------| 
        |Id             |  1                  |
        |Title          |  Lolita             |
        |Description    |  1955               |
        |Author         |  VladimirNabokov    |
     Then The response is 200 OK and JSON:
	|  Key          |  Value              |
        | --------------|---------------------| 
        |Id             |  1                  |
        |Title          |  Lolita             |
        |Description    |  1955               |
        |Author         |  VladimirNabokov    |
@Failed
Scenario: 03 Add Second book to Library
    Given URL address http://localhost:9000/api/books/
    When User sends POST request with JSON:
	|  Key          |  Value              | 
        | --------------|---------------------| 
        |Id             |  2                  |
        |Title          |  LaRouteBleue       |
        |Description    |  1983               |
        |Author         |  KennethWhite       |
    Then The response is 200 OK and JSON:
        |  Key          |  Value              | 
        | --------------|---------------------| 
        |Id             |  2                  |
        |Title          |  LaRouteBleue       |
        |Description    |  1983               |
        |Author         |  KennethWhite       |
@Passed
Scenario: 04 Update an existing book from Library by Id
    Given URL address http://localhost:9000/api/books/1
    When User sends PUT request with JSON:
	|  Key          |  Value              | 
        | --------------|---------------------| 
        |Id             |  1                  |
        |Title          |  ForWhomtheBellTolls|
        |Description    |  1940               |
        |Author         |  ErnestHemingway    |
    Then The response is 200 OK and JSON:
        |  Key          |  Value              | 
        | --------------|---------------------| 
        |Id             |  1                  |
        |Title          |  ForWhomtheBellTolls|
        |Description    |  1940               |
        |Author         |  ErnestHemingway    |
@Failed
Scenario: 05 Select a book from Library by Id after Update
    Given URL address http://localhost:9000/api/books/1
    When User sends GET request to http://localhost:9000/api/books/1
    Then The response is 200 OK and JSON body:
        |  Key          |  Value              | 
        | --------------|---------------------| 
        |Id             |  1                  |
        |Title          |  ForWhomtheBellTolls|
        |Description    |  1940               |
        |Author         |  ErnestHemingway    |
@Failed
Scenario: 06 Select a book from Library by Title
    Given I have made a GET request to "http://localhost:9000/api/books?Title=Bleue"
    When I receive a response
    Then the response should contain a book with the following details:
      | Id | Title          | Description | Author       |
      | 2  | LaRouteBleue   | 1983        | KennethWhite |
@Passed
Scenario: 07 Delete First book from Library
    Given URL address http://localhost:9000/api/books/1
    When User sends DELETE request to http://localhost:9000/api/books/1
    Then The response is 204 No Content and empty body
@Passed
Scenario: 08 Delete Second book from Library
    Given URL address http://localhost:9000/api/books/2
    When User sends DELETE request to http://localhost:9000/api/books/2
    Then The response is 204 No Content and empty body
